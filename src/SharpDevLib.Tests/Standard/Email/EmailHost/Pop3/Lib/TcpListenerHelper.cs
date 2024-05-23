
using System;
using System.Net.Sockets;

namespace SharpDevLib.Tests.Standard.Email.EmailHost.Pop3.Lib;

public static class TcpListenerHelper
{
    public delegate void OnNewConnectionDelegate(TcpClient tcp);

    public static void StartListen(this TcpListener listen, OnNewConnectionDelegate onNew)
    {
        listen.Start();

        BeginListen();
        void BeginListen()
        {
            Helpers.TryCallCatch(BeginListenInternal);
            void BeginListenInternal()
            {
                listen.BeginAcceptTcpClient(OnConnectInternal, null);
            }
        }

        void OnConnectInternal(IAsyncResult iar)
        {
            if (listen.Server == null || listen.Server.IsBound == false) return;
            BeginListen();

            TcpClient? tcp = null;
            Helpers.TryCallCatch(EndListenInternal);
            void EndListenInternal()
            {
                tcp = listen.EndAcceptTcpClient(iar);
            }

            if (tcp != null)
            {
                onNew(tcp);
            }
        }
    }
}
