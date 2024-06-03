using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpDevLib.Standard;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace SharpDevLib.Tests.Standard.Transport.Tcp;

[TestClass]
public class TcpTests
{
    [TestMethod]
    public async Task Test()
    {
        var listenerFactory = new TcpListenerFactory();
        var listener = listenerFactory.Create<int>(IPAddress.Any, 4098);
        StartListener(listener);

        var clientFactory = new TcpClientFactory();
        var client = clientFactory.Create(IPAddress.Loopback, 4098);
        ClientStartConnectAndReceive(client);
        await Task.Delay(500);

        client.Send("hello,world".ToUtf8Bytes());
        await Task.Delay(500);

        listener.Dispose();
        client.Dispose();
    }

    async void StartListener(TcpListener<int> listener)
    {
        listener.StateChanged += (s, e) =>
        {
            Console.WriteLine($"server state changed:{e.Before}->{e.Current}");
        };
        listener.SessionAdded += (s, e) =>
        {
            Console.WriteLine("session added");
            e.Session.Received += (ss, ee) =>
            {
                Console.WriteLine($"server received:{ee.Bytes.ToUtf8String()}");
                ee.Session.Send("server reply".ToUtf8Bytes());
            };
            e.Session.Error += (ss, ee) =>
            {
                Console.WriteLine($"server error:{ee.Exception.Message}");
            };
        };
        listener.SessionRemoved += (s, e) =>
        {
            Console.WriteLine($"server session removed");
        };
        await listener.ListenAsync();
    }

    async void ClientStartConnectAndReceive(SharpDevLib.Standard.TcpClient client)
    {
        client.StateChanged += (s, e) =>
        {
            Console.WriteLine($"client state changed:{e.Before}->{e.Current}");
        };
        client.Received += (s, e) =>
        {
            Console.WriteLine($"client received:{e.Bytes.ToUtf8String()}");
        };
        client.Error += (s, e) =>
        {
            Console.WriteLine($"client error:{e.Exception.Message}");
        };
        await client.ConnectAndReceiveAsync();
    }
}


