using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net;
using System.Threading.Tasks;

namespace SharpDevLib.Tests.Transport.Tcp;

[TestClass]
public class TcpTests
{
    [TestMethod]
    public void GetAvailbelTcpTest()
    {
        var port = TcpHelper.GetAvailableTcpPort(50, 100);
        Assert.IsGreaterThanOrEqualTo(50, port);
        Assert.IsLessThanOrEqualTo(100, port);
    }

    [TestMethod]
    public async Task Test()
    {
        var listener = TcpHelper.CreateListener(IPAddress.Any, 4098);
        StartListener(listener);

        var client = TcpHelper.CreateClient(IPAddress.Loopback, 4098);
        ClientStartConnectAndReceive(client);
        await Task.Delay(500, TestContext.CancellationToken);

        client.Send("hello,world".Utf8Decode());
        await Task.Delay(500, TestContext.CancellationToken);

        listener.Dispose();
        client.Dispose();
    }

    static async void StartListener(TcpListener listener)
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
                Console.WriteLine($"server received:{ee.Bytes.Utf8Encode()}");
                ee.Session.Send("server reply".Utf8Decode());
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

    static async void ClientStartConnectAndReceive(TcpClient client)
    {
        client.StateChanged += (s, e) =>
        {
            Console.WriteLine($"client state changed:{e.Before}->{e.Current}");
        };
        client.Received += (s, e) =>
        {
            Console.WriteLine($"client received:{e.Bytes.Utf8Encode()}");
        };
        client.Error += (s, e) =>
        {
            Console.WriteLine($"client error:{e.Exception.Message}");
        };
        await client.ConnectAndReceiveAsync();
    }

    public TestContext TestContext { get; set; }
}


