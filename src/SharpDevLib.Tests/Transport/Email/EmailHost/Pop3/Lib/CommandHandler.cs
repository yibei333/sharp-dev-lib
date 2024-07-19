using System;
using System.Collections.Generic;
using System.Linq;

namespace SharpDevLib.Tests.Transport.Email.EmailHost.Pop3.Lib;

internal class CommandHandler(POP3ServerSession activeConnection, POP3Listener service) : IPOP3ConnectionInfo
{
    private readonly POP3Listener service = service;

    private string unauthUserName = null!;
    private string userNameAtLogin = null!;
    private string authMailboxID = null!;
    private bool mailboxIsReadOnly = true;
    private bool Authenticated => authMailboxID != null;
    private IList<string> uniqueIDs = [];
    private readonly List<string> deletedUniqueIDs = [];
    private readonly POP3ServerSession activeConnection = activeConnection;
    private bool isSleeping = false;

    System.Net.IPAddress IPOP3ConnectionInfo.ClientIP => activeConnection.ClientIP;

    long IPOP3ConnectionInfo.ConnectionID => activeConnection.ConnectionID;

    string IPOP3ConnectionInfo.AuthMailboxID => authMailboxID;

    string IPOP3ConnectionInfo.UserNameAtLogin => Authenticated ? userNameAtLogin : null!;

    bool IPOP3ConnectionInfo.IsSecure => activeConnection.IsSecure;

    private bool IsUserLoginAllowed => service.RequireSecureLogin == false || activeConnection.IsSecure || activeConnection.IsLocalHost;

    static readonly List<string> capabilities =
    [
        "TOP", "RESP-CODES", "PIPELINING", "UIDL", "AUTH-RESP-CODE",
        "UID-PARAM", "MULTI-LINE-IND", "DELI", "SLEE-WAKE", "QAUT"
    ];

    static readonly List<string> allowedUnauth =
    [
        "NOOP", "CAPA", "USER", "PASS", "XLOG", "STLS", "QUIT"
    ];

    static readonly List<string> allowedSleeping =
    [
        "NOOP", "WAKE", "QUIT"
    ];

    static PopResponse BadCommandSyntaxResponse => PopResponse.ERR("Bad command syntax.");

    private const string UidParamPrefix = "UID:";

    internal PopResponse Connect() => PopResponse.OKSingle(service.ServiceName);

    internal PopResponse Command(string command, string pars)
    {
        if (Authenticated == false && allowedUnauth.Contains(command) == false) return PopResponse.ERR("Authenticate first.");

        if (isSleeping && allowedSleeping.Contains(command) == false) return PopResponse.ERR("This command is not allowed in a sleeping state. (Only NOOP, QUIT or WAKE.)");

        return command switch
        {
            "NOOP" => NOOP(),
            "CAPA" => CAPA(),
            "USER" => USER(pars),
            "PASS" => PASS(pars),
            "XLOG" => XLOG(pars),
            "STAT" => STAT(),
            "LIST" => LIST(pars),
            "UIDL" => UIDL(pars),
            "RETR" => RETR(pars),
            "TOP" => TOP(pars),
            "DELE" => DELE(pars),
            "DELI" => DELI(pars),
            "QUIT" => QUIT(),
            "QAUT" => QAUT(),
            "RSET" => RSET(),
            "SLEE" => SLEE(),
            "WAKE" => WAKE(),
            _ => PopResponse.ERR("Unknown command: " + command),
        };
    }

    private PopResponse CAPA()
    {
        var resp = new List<string>(capabilities);

        resp.Insert(0, "IMPLEMENTATION " + service.ServiceName);

        if (service.ServiceName.Contains("/billpg.com/") == false) resp.Add("X-ENGINE http://billpg.com/POP3/");
        if (activeConnection.IsSecure == false && service.SecureCertificate != null) resp.Add("STLS");
        if (IsUserLoginAllowed) resp.Add("USER");

        resp.Add($"X-TLS {activeConnection.IsSecure}");
        resp.Sort();
        return PopResponse.OKMulti("Capabilities are...", resp);
    }

    private PopResponse USER(string claimedUser)
    {
        if (IsUserLoginAllowed == false) return PopResponse.ERR("Call STLS and negotiate TLS first.");

        if (Authenticated || unauthUserName != null) return PopResponse.ERR("Already called USER.");

        if (claimedUser.Length == 0) return PopResponse.ERR("Invalid user name.");

        unauthUserName = claimedUser;
        return PopResponse.OKSingle("Thank you. Send password.");
    }

    private PopResponse PASS(string claimedPassClear)
    {
        static PopResponse badPasswordResponse() => PopResponse.ERR("AUTH", "Wrong username or password.");

        var clientIP = ((IPOP3ConnectionInfo)this).ClientIP;
        if (service.IPBanEngine.IsBanned(clientIP))
        {
            service.IPBanEngine.RegisterFailedAttempt(clientIP);
            return badPasswordResponse();
        }

        if (unauthUserName == null) return PopResponse.ERR("Call USER before PASS.");
        if (Authenticated) return PopResponse.ERR("Already authenticated.");

        var authreq = new POP3AuthenticationRequest(this, unauthUserName, claimedPassClear);
        service.Events.OnAuthenticate(authreq);

        if (authreq.AuthMailboxID != null)
        {
            userNameAtLogin = unauthUserName;
            unauthUserName = null!;
            authMailboxID = authreq.AuthMailboxID;
            mailboxIsReadOnly = authreq.MailboxIsReadOnly;
            uniqueIDs = RefreshUniqueIDsFromMailbox;
            return PopResponse.OKSingle("Welcome.");
        }
        else
        {
            unauthUserName = null!;
            service.IPBanEngine.RegisterFailedAttempt(clientIP);
            return badPasswordResponse();
        }
    }

    private IList<string> RefreshUniqueIDsFromMailbox
    {
        get
        {
            var newUniqueIDs = service.Events.OnMessageList(authMailboxID).ToList().AsReadOnly();

            if (newUniqueIDs.All(IsGoodUniqueID)) return newUniqueIDs;
            else throw new POP3ResponseException("Unique ID strings must be printable ASCII only.", true);

            static bool IsGoodUniqueID(string uid)
            {
                if (string.IsNullOrEmpty(uid)) return false;
                if (uid.Length > 70) return false;
                if (uid == "_") return false;
                if (uid.Any(ch => ch < '!' || ch > '~')) return false;
                return true;
            }
        }
    }

    private PopResponse XLOG(string pars)
    {
        int spaceIndex = pars.IndexOf(' ');
        if (spaceIndex < 1) return PopResponse.ERR("Syntax: XLOG (username) (password)");

        unauthUserName = pars[..spaceIndex];
        var claimedPassword = pars[(spaceIndex + 1)..];

        return PASS(claimedPassword);
    }

    private PopResponse STAT()
    {
        var countedUniqueIDs = uniqueIDs.Except(deletedUniqueIDs).ToList();
        var totalBytes = countedUniqueIDs.Select(uniqueID => service.Events.OnMessageSize(authMailboxID, uniqueID)).Sum();
        return PopResponse.OKSingle($"{countedUniqueIDs.Count} {totalBytes}");
    }

    private PopResponse LIST(string id)
    {
        return PerMessageOrSingle(id, Translate, "Message sizes follow...");
        string Translate(int messageID, string uniqueID) => $"{messageID} {service.Events.OnMessageSize(authMailboxID, uniqueID)}";
    }

    private PopResponse UIDL(string id)
    {
        return PerMessageOrSingle(id, Translate, "Unique-IDs follow...");
        static string Translate(int messageID, string uniqueID) => $"{messageID} {uniqueID}";
    }

    private PopResponse PerMessageOrSingle(string id, MultiLinePerMessageTranslate translateFn, string firstLineText)
    {
        if (string.IsNullOrEmpty(id)) return MultiLinePerMessage(firstLineText, translateFn);

        ParseForUniqueId(id, out int messageID, out string uniqueID);

        return PopResponse.OKSingle(translateFn(messageID, uniqueID));
    }

    private delegate string MultiLinePerMessageTranslate(int messageID, string uniqueID);
    private PopResponse MultiLinePerMessage(string introText, MultiLinePerMessageTranslate translate)
    {
        int messageID = 0;
        string NextResponseLine()
        {
            while (true)
            {
                messageID++;

                if (messageID > uniqueIDs.Count) return null!;
                var uniqueID = uniqueIDs[messageID - 1];
                if (deletedUniqueIDs.Contains(uniqueID)) continue;
                return translate(messageID, uniqueID);
            }
        }

        return PopResponse.OKMulti(introText, NextResponseLine);
    }

    private PopResponse RETR(string pars) => RetrOrTop(pars, -1);

    private PopResponse TOP(string pars)
    {
        if (string.IsNullOrEmpty(pars)) return BadCommandSyntaxResponse;

        int spaceIndex = pars.LastIndexOf(' ');
        if (spaceIndex < 0) return BadCommandSyntaxResponse;
        if (int.TryParse(pars[spaceIndex..].Trim(), out int lineCount) == false) return BadCommandSyntaxResponse;

        return RetrOrTop(pars[..spaceIndex].Trim(), lineCount);
    }

    private PopResponse RetrOrTop(string pars, int lineCountSend)
    {
        if (string.IsNullOrEmpty(pars)) return BadCommandSyntaxResponse;

        ParseForUniqueId(pars, out _, out string uniqueID);

        var request = new POP3MessageRetrievalRequest(authMailboxID, uniqueID, lineCountSend);
        service.Events.OnMessageRetrieval(request);

        if (lineCountSend < 0) return PopResponse.OKMulti("Message text follows...", ContentWrappers.WrapForRetr(request));
        else return PopResponse.OKMulti($"Header and first {lineCountSend} lines...", ContentWrappers.WrapForTop(request, lineCountSend));
    }

    private PopResponse DELE(string pars)
    {
        return DeleteWrapper(pars, Internal);
        PopResponse Internal(string uniqueID)
        {
            deletedUniqueIDs.Add(uniqueID);
            return PopResponse.OKSingle($"Message UID:{uniqueID} flagged for delete on QUIT or SLEE.");
        }
    }

    private PopResponse DELI(string pars)
    {
        return DeleteWrapper(pars, Internal);
        PopResponse Internal(string uniqueID)
        {
            service.Events.OnMessageDelete(authMailboxID, new List<string> { uniqueID }.AsReadOnly());
            deletedUniqueIDs.Add(uniqueID);
            return PopResponse.OKSingle($"Deleted message. UID:{uniqueID}");
        }
    }

    private PopResponse DeleteWrapper(string pars, Func<string, PopResponse> action)
    {
        if (string.IsNullOrEmpty(pars)) return BadCommandSyntaxResponse;

        if (mailboxIsReadOnly) return PopResponse.ERR("READ-ONLY", "This mailbox is read-only.");

        ParseForUniqueId(pars, out _, out string uniqueID);

        return action(uniqueID);
    }

    private PopResponse QUIT()
    {
        if (Authenticated == false) return PopResponse.Quit("Closing connection without authenticating.");
        DeleteFlaggedMessages(out int messageCount);
        var messagesDeletedReport = "";
        if (messageCount > 0) messagesDeletedReport = $"{messageCount} messages deleted. ";
        return PopResponse.Quit(messagesDeletedReport + "Closing connection.");
    }

    private void DeleteFlaggedMessages(out int messageCount)
    {
        messageCount = deletedUniqueIDs.Count;
        if (messageCount == 0) return;
        service.Events.OnMessageDelete(authMailboxID, deletedUniqueIDs.AsReadOnly());
        deletedUniqueIDs.Clear();
    }

    private PopResponse RSET()
    {
        deletedUniqueIDs.Clear();
        return PopResponse.OKSingle("Un-flagged all messages flagged for delete.");
    }

    private PopResponse QAUT()
    {
        if (Authenticated == false) return PopResponse.ERR("Not logged in.");
        DeleteFlaggedMessages(out _);
        deletedUniqueIDs.Clear();

        unauthUserName = null!;
        userNameAtLogin = null!;
        authMailboxID = null!;
        isSleeping = false;

        return PopResponse.OKSingle("Logged out. You can either reauthenticate or exit.");
    }

    private PopResponse SLEE()
    {
        if (isSleeping) return PopResponse.ERR("Already sleeping.");

        DeleteFlaggedMessages(out int messageCount);
        deletedUniqueIDs.Clear();
        isSleeping = true;

        return PopResponse.OKSingle($"Zzzzz. Deleted {messageCount} messages.");
    }

    private PopResponse WAKE()
    {
        if (isSleeping == false) return PopResponse.ERR("Not sleeping.");
        var newUniqueIDs = RefreshUniqueIDsFromMailbox;
        bool isNewMessages = newUniqueIDs.Except(uniqueIDs).Any();
        uniqueIDs = newUniqueIDs.ToList().AsReadOnly();
        isSleeping = false;
        return PopResponse.OKSingle(ActivityResponseCode(isNewMessages), "Welcome back.");
    }

    static string ActivityResponseCode(bool isNewMessages) => "ACTIVITY/" + (isNewMessages ? "NEW" : "NONE");

    static PopResponse NOOP() => PopResponse.OKSingle("There's no-one here but us POP3 services.");

    private void ParseForUniqueId(string id, out int messageID, out string uniqueID)
    {
        if (int.TryParse(id, out int parsedMessageID))
        {
            if (parsedMessageID < 1 || parsedMessageID > uniqueIDs.Count) throw new POP3ResponseException("No such message.");

            messageID = parsedMessageID;
            uniqueID = uniqueIDs[messageID - 1];

            if (string.IsNullOrEmpty(uniqueID) || deletedUniqueIDs.Contains(uniqueID)) throw new POP3ResponseException("That message has been deleted.");
            return;
        }

        if (id.StartsWith(UidParamPrefix, StringComparison.InvariantCultureIgnoreCase))
        {
            var selectedUniqueID = id[UidParamPrefix.Length..];
            if (IsValidUniqueID(selectedUniqueID) == false) throw new POP3ResponseException("UID/NOT-FOUND", "No such UID.");
            if (string.IsNullOrEmpty(selectedUniqueID) || deletedUniqueIDs.Contains(selectedUniqueID)) throw new POP3ResponseException("That message has been deleted.");

            foreach (int loopedMessageID in Enumerable.Range(1, uniqueIDs.Count))
            {
                if (selectedUniqueID == uniqueIDs[loopedMessageID - 1])
                {
                    messageID = loopedMessageID;
                    uniqueID = selectedUniqueID;
                    return;
                }
            }

            if (service.AllowUnknownIDRequests)
            {
                if (service.Events.OnMessageList(authMailboxID).Contains(selectedUniqueID))
                {
                    messageID = 0;
                    uniqueID = selectedUniqueID;
                    return;
                }
            }

            throw new POP3ResponseException("UID/NOT-FOUND", "No such UID.");
        }

        throw new POP3ResponseException("Bad parameters.");
    }

    static bool IsValidUniqueID(string uniqueID)
    {
        if (string.IsNullOrEmpty(uniqueID)) return false;

        foreach (char ch in uniqueID)
        {
            int ascii = ch;
            if (ascii < 33 || ascii > 126) return false;
        }

        return true;
    }

}

