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
        using var listener = listenerFactory.Create<int>(IPAddress.Any, 4098);
        listener.StateChanged += (s, e) =>
        {
            Console.WriteLine($"server state changed:{e.Before}->{e.Current}");
        };
        listener.SessionAdded += (s, e) =>
        {
            Console.WriteLine("session added");
            e.Session.Received += (ss, ee) =>
            {
                Console.WriteLine($"server received:{(string.Join(",", ee.Bytes))}");
                ee.Session.Send("server reply".ToUtf8Bytes());
            };
            e.Session.Error += (ss, ee) =>
            {
                Console.WriteLine($"server error:{ee.Exception.Message}");
            };
        };
        StartListener(listener);
        await Task.Delay(1000);

        var clientFactory = new TcpClientFactory();
        using var client = clientFactory.Create(IPAddress.Loopback, 4098);
        client.StateChanged += (s, e) =>
        {
            Console.WriteLine($"client state changed:{e.Before}->{e.Current}");
        };
        client.Received += (s, e) =>
        {
            Console.WriteLine($"client received:{(string.Join(",", e.Bytes))}");
        };
        client.Error += (s, e) =>
        {
            Console.WriteLine($"client error:{e.Exception.Message}");
        };
        ClientStartConnectAndReceive(client);
        await Task.Delay(1000);

        client.Send([1, 2, 3, 4, 5, 6, 7, 8]);
        await Task.Delay(1000);

        listener.Dispose();
        client.Dispose();
    }

    async void StartListener(TcpListener<int> listener)
    {
        await listener.ListenAsync();
    }

    async void ClientStartConnectAndReceive(SharpDevLib.Standard.TcpClient client)
    {
        await client.ConnectAndReceiveAsync();
    }
}


