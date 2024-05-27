using System;
using System.Collections.Generic;

namespace SharpDevLib.Tests.Standard.Email.EmailHost.Pop3.Lib;

public class POP3Events
{
    public delegate void OnNewConnectionDelegate(IPOP3ConnectionInfo info);
    public OnNewConnectionDelegate OnNewConnection { get; set; }

    public delegate void OnCommandReceivedDelegate(IPOP3ConnectionInfo info, string command);
    public OnCommandReceivedDelegate OnCommandReceived { get; set; }

    public delegate void OnAuthenticateDelegate(POP3AuthenticationRequest req);
    public OnAuthenticateDelegate OnAuthenticate { set; get; }

    public delegate IEnumerable<string> OnMessageListDelegate(string mailboxID);
    public OnMessageListDelegate OnMessageList { set; get; }

    public delegate long OnMessageSizeDelegate(string mailboxID, string messageUniqueID);
    public OnMessageSizeDelegate OnMessageSize { get; set; }

    public delegate void OnMessageRetrievalDelegate(POP3MessageRetrievalRequest req);
    public OnMessageRetrievalDelegate OnMessageRetrieval { get; set; }

    public delegate void OnMessageDeleteDelegate(string mailboxID, IList<string> uniqueIDs);
    public OnMessageDeleteDelegate OnMessageDelete { get; set; }

    public delegate void OnErrorDelegate(IPOP3ConnectionInfo info, Exception ex);
    public OnErrorDelegate OnError { get; set; }

    internal POP3Events()
    {
        OnNewConnection = info => { };
        OnCommandReceived = (info, command) => { };
        OnAuthenticate = req => req.AuthMailboxID = null!;
        OnMessageList = mailboxID => [];
        OnMessageRetrieval = req => throw new POP3ResponseException("SYS/PERM", "Retrieval not configured.", true);
        OnMessageSize = (mailboxID, messageUID) => ContentWrappers.MessageSizeByRetrieval(OnMessageRetrieval, mailboxID, messageUID);
        OnMessageDelete = (mailboxID, messageUID) => throw new POP3ResponseException("SYS/PERM", "Deletes not configured.", true);
        OnError = (info, ex) => { };
    }
}