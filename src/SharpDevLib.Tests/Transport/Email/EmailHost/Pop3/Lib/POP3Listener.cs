using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

namespace SharpDevLib.Tests.Transport.Email.EmailHost.Pop3.Lib;

public class POP3Listener : IDisposable
{
    private readonly object mutex;
    private readonly List<TcpListener> listeners;
    private readonly List<POP3ServerSession> connections;
    public IIPBanEngine IPBanEngine { get; set; } = new ThreeStrikesBanEngine();
    public bool RequireSecureLogin { get; set; }
    public X509Certificate SecureCertificate { get; set; } = null!;
    internal readonly ManualResetEvent stopService;
    private static long nextConnectionID = 5000000001;
    internal static long GenConnectionID() => Interlocked.Increment(ref nextConnectionID);
    public POP3Events Events { get; } = new POP3Events();
    public bool AllowUnknownIDRequests { get; set; } = true;

    public POP3Listener()
    {
        mutex = new object();
        stopService = new ManualResetEvent(false);
        listeners = [];
        connections = [];
        RequireSecureLogin = true;
    }

    public string ServiceName { get; set; } = "POP3 service by billpg industries https://billpg.com/POP3/";

    public void ListenOnStandard(IPAddress addr)
    {
        ListenOn(addr, 110, false);
        ListenOn(addr, 995, true);
    }

    public void ListenOnHigh(IPAddress addr)
    {
        ListenOn(addr, 1100, false);
        ListenOn(addr, 9955, true);
    }

    public int ListenOnRandom(IPAddress addr, bool immediateTls)
    {
        var listen = new TcpListener(addr, 0);
        listen.StartListen(OnNewConnection(immediateTls));
        StoreNewListener(listen);
        return ((IPEndPoint)listen.LocalEndpoint).Port;
    }

    public void ListenOn(IPAddress addr, int port, bool immediateTls)
    {
        var listen = new TcpListener(addr, port);
        listen.StartListen(OnNewConnection(immediateTls));
        StoreNewListener(listen);
    }

    private void StoreNewListener(TcpListener listen)
    {
        lock (mutex) listeners.Add(listen);
    }

    private TcpListenerHelper.OnNewConnectionDelegate OnNewConnection(bool immediateTls)
    {
        return Internal;
        void Internal(TcpClient tcp)
        {
            lock (mutex)
            {
                connections.RemoveAll(con => con.IsActive == false);
                var connection = new POP3ServerSession(tcp, immediateTls, this, GenConnectionID());
                connections.Add(connection);
                connection.Start();
            }
        }
    }

    public void Stop()
    {
        lock (mutex)
        {
            stopService.Set();
            foreach (var listen in listeners) listen.Stop();
            foreach (var con in connections) con.CloseConnection();
        }
    }

#pragma warning disable CA1816 // Dispose methods should call SuppressFinalize
    void IDisposable.Dispose() => Stop();
#pragma warning restore CA1816 // Dispose methods should call SuppressFinalize
}

