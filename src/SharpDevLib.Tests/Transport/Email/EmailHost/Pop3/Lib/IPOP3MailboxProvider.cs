using System;
using System.Collections.Generic;
using System.IO;

namespace SharpDevLib.Tests.Transport.Email.EmailHost.Pop3.Lib
{
    public class POP3AuthenticationRequest
    {
        public IPOP3ConnectionInfo ConnectionInfo { get; }
        public string SuppliedUsername { get; }
        public string SuppliedPassword { get; }

        internal POP3AuthenticationRequest(IPOP3ConnectionInfo info, string suppliedUsername, string suppliedPassword)
        {
            ConnectionInfo = info;
            SuppliedUsername = suppliedUsername;
            SuppliedPassword = suppliedPassword;
        }

        public string AuthMailboxID { get; set; } = null!;
        public bool MailboxIsReadOnly { get; set; } = false;
    }

    public class POP3MessageRetrievalRequest
    {
        public string AuthMailboxID { get; }
        public string MessageUniqueID { get; }
        public int TopLineCount { get; }
        public bool FullMessage => TopLineCount < 0;
        public Func<string> OnNextLine { get; set; }
        public Action OnClose { get; set; }
        public bool AcceptRetrieval { get; set; }

        internal POP3MessageRetrievalRequest(string authMailboxID, string messageUniqueID, int topLineCount)
        {
            AuthMailboxID = authMailboxID;
            MessageUniqueID = messageUniqueID;
            TopLineCount = topLineCount;
            OnNextLine = () => throw new ApplicationException("OnNextLine event handler has not been set.");
            OnClose = DefaultOnClose;
            AcceptRetrieval = false;
        }

        private void DefaultOnClose()
        {
        }

        public void RejectRequest()
        {
            AcceptRetrieval = false;
        }

        public void UseEnumerableLines(IEnumerable<string> lines)
        {
            var lineEnum = lines.GetEnumerator();
            string OnNextLineInternal() => lineEnum.MoveNext() ? lineEnum.Current : null!;
            OnNextLine = OnNextLineInternal;
            OnClose = lineEnum.Dispose;
            AcceptRetrieval = true;
        }

        public void UseTextFile(string emlPath, bool deleteAfter)
        {
            var emlStream = File.OpenText(emlPath);
            OnNextLine = emlStream.ReadLine!;
            if (deleteAfter) OnClose = DisposeAndDelete;
            else OnClose = emlStream.Dispose;
            void DisposeAndDelete()
            {
                emlStream.Dispose();
                File.Delete(emlPath);
            }
        }
    }

    public delegate void RaiseNewMessageEvent(string mailboxID);

    public class POP3ResponseException(string responseCode, string message, bool isCritical) : Exception(message)
    {
        public string ResponseCode { get; } = responseCode;
        public bool IsCritical { get; } = isCritical;

        public POP3ResponseException(string responseCode, string message) : this(responseCode, message, false) { }

        public POP3ResponseException(string message) : this(null!, message, false) { }

        public POP3ResponseException(string message, bool isCritical) : this(null!, message, isCritical) { }

        internal PopResponse AsResponse()
        {
            if (IsCritical) return PopResponse.Critical(ResponseCode, Message);
            else return PopResponse.ERR(ResponseCode, Message);
        }
    }

    internal delegate string NextLineFn();

    public interface IPOP3ConnectionInfo
    {
        System.Net.IPAddress ClientIP { get; }
        long ConnectionID { get; }
        string AuthMailboxID { get; }
        string UserNameAtLogin { get; }
        bool IsSecure { get; }
    }

    public interface IMessageContent
    {
        string NextLine();
        void Close();
    }
}

