using System;
using System.IO;
using System.Net.Security;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace SharpDevLib.Tests.Transport.Email.EmailHost.Pop3.Lib
{
    internal class POP3ServerSession
    {
        private readonly TcpClient tcp;
        private readonly bool immediateTls;
        private readonly POP3Listener service;
        private readonly long connectionID;

        private readonly object mutex;
        private readonly CommandHandler handler;
        private readonly LineBuffer buffer;
        private NetworkStream tcpstr;
        private SslStream tls;
        private Stream CurrStream => tls as Stream ?? tcpstr;
        private PopResponse currResp;

        internal POP3ServerSession(TcpClient tcp, bool immediateTls, POP3Listener service, long connectionID)
        {
            this.tcp = tcp;
            this.immediateTls = immediateTls;
            this.service = service;
            this.connectionID = connectionID;

            mutex = new object();
            handler = new CommandHandler(this, service);
            buffer = new LineBuffer(1024 * 64);
            tcpstr = null!;
            tls = null!;
            currResp = null!;
        }

        internal System.Net.IPAddress ClientIP => ((System.Net.IPEndPoint)tcp.Client.RemoteEndPoint!).Address;

        internal long ConnectionID => connectionID;

        internal bool IsLocalHost
        {
            get
            {
                var client = ClientIP.ToString();
                return client == "127.0.0.1" || client == "::1";
            }
        }

        internal bool IsSecure => tls != null;

        internal bool IsActive => tcp.Connected;

        internal void Start()
        {
            tcpstr = tcp.GetStream();

            if (immediateTls) HandshakeTLS();
            else SendConnectBanner();
        }

        internal void CloseConnection()
        {
            lock (mutex) tcp.Close();
        }

        private void HandshakeTLS()
        {
            if (tls != null) return;

            tls = new SslStream(tcpstr, false);
            tls.AuthenticateAsServerAsync(service.SecureCertificate).LockContinueWith(mutex, OnEndTLS);
        }

        void OnEndTLS(Task task)
        {
            if (task.Status == TaskStatus.RanToCompletion)
            {
                buffer.Clear();

                if (tls.SslProtocol != System.Security.Authentication.SslProtocols.Tls12) StartSendResponse(PopResponse.Critical("SYS/PERM", "Only TLS 1.2 is supported by this server."));

                if (immediateTls) SendConnectBanner();
                else InterpretCommand();
            }
        }


        private void SendConnectBanner()
        {
            StartSendResponse(handler.Connect());
        }

        private void StartSendResponse(PopResponse resp)
        {
            if (currResp != null) throw new ApplicationException("StartSendResponse called before conclusion of response.");
            currResp = resp;

            var task = CurrStream.WriteLineAsync(currResp.FirstLine);

            if (resp.IsMultiLine) task.LockContinueWith(mutex, ContinueSendResponse);
            else if (resp.IsQuit) task.LockContinueWith(mutex, CompleteSendResponse(CloseConnection));
            else task.LockContinueWith(mutex, CompleteSendResponse(InterpretCommand));
        }

        private void ContinueSendResponse(Task task)
        {
            if (task.Status == TaskStatus.RanToCompletion)
            {
                var nextLine = currResp.NextLine();
                if (nextLine != null)
                {
                    var quotedNextLine = Helpers.AsDotQuoted(nextLine);
                    CurrStream.WriteLineAsync(quotedNextLine).LockContinueWith(mutex, ContinueSendResponse);
                }
                else CurrStream.WriteLineAsync(".").LockContinueWith(mutex, CompleteSendResponse(InterpretCommand));
            }
            else if (task.Status == TaskStatus.Faulted)
            {
                service.Events.OnError(handler, task.Exception!);
                CloseConnection();
            }
            else throw new ApplicationException("Unknown task status: " + task.Status);
        }

        private Action<Task> CompleteSendResponse(Action onRanToComplete)
        {
            return Internal;
            void Internal(Task task)
            {
                if (task.Status == TaskStatus.RanToCompletion)
                {
                    currResp = null!;
                    onRanToComplete();
                }
                else if (task.Status == TaskStatus.Faulted)
                {
                    service.Events.OnError(handler, task.Exception!);
                    CloseConnection();
                }
                else throw new ApplicationException("Unknown task status: " + task.Status);
            }
        }

        private void InterpretCommand()
        {
            var linePacket = buffer.GetLine();

            if (linePacket == null)
            {
                if (tcp.Connected == false) return;
                CurrStream.ReadAsync(buffer.Buffer, buffer.VacantStart, buffer.VacantLength).LockContinueWith(mutex, CompleteRead);
            }
            else if (linePacket.AsASCII == "STLS")
            {
                if (IsSecure) StartSendResponse(PopResponse.ERR("Already secure."));
                else CurrStream.WriteLineAsync("+OK Send TLS ClientHello when ready.").LockContinueWith(mutex, CompleteSendResponse(HandshakeTLS));
            }
            else
            {
                var line = linePacket.AsASCII;
                string command, pars;
                var spaceIndex = line.IndexOf(' ');
                if (spaceIndex < 0)
                {
                    command = line;
                    pars = null!;
                }
                else
                {
                    command = line[..spaceIndex];
                    pars = line[(spaceIndex + 1)..];
                }
                OnCommand(command.ToUpperInvariant(), pars);
            }
        }

        void CompleteRead(Task<int> task)
        {
            if (task.IsConnectionClosed())
            {
                /* Nothing to do. Don't lauch any new async tasks and leave this session to expire. */
            }
            else if (task.Status == TaskStatus.RanToCompletion)
            {
                buffer.UpdateUsedBytes(task.Result);
                InterpretCommand();
            }
            else if (task.Status == TaskStatus.Faulted)
            {
                service.Events.OnError(handler, task.Exception!);
                CloseConnection();
            }
            else throw new ApplicationException("Unexpected task.Status: " + task.Status);
        }

        void OnCommand(string command, string pars)
        {
            PopResponse resp;
            try
            {
                resp = handler.Command(command, pars);
            }
            catch (Exception ex)
            {
                service.Events.OnError(handler, ex);
                if (ex is POP3ResponseException rex) resp = rex.AsResponse();
                else if (ex is NotImplementedException) resp = PopResponse.ERR("Command not available.");
                else resp = PopResponse.Critical("SYS/TEMP", "System error. Administrators should check logs.");
            }

            StartSendResponse(resp);
        }
    }
}