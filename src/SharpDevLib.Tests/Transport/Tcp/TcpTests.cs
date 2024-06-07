using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpDevLib.Transport;
using System;
using System.Net;
using System.Threading.Tasks;

namespace SharpDevLib.Tests.Transport.Tcp;

[TestClass]
public class TcpTests
{
    [TestMethod]
    public async Task Test()
    {
        var listenerFactory = new TcpListenerFactory();
        var listener = listenerFactory.Create(IPAddress.Any, 4098);
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

    [TestMethod]
    public async Task ServiceTest()
    {
        IServiceCollection services = new ServiceCollection();
        services.AddTcp();
        var serviceProvider = services.BuildServiceProvider();
        var listenerFactory = serviceProvider.GetRequiredService<ITcpListenerFactory>();
        var clientFactory = serviceProvider.GetRequiredService<ITcpClientFactory>();

        var listener = listenerFactory.Create(IPAddress.Any, 4099);
        StartListener(listener);

        var client = clientFactory.Create(IPAddress.Loopback, 4099);
        ClientStartConnectAndReceive(client);
        await Task.Delay(500);

        client.Send("hello,world".ToUtf8Bytes());
        await Task.Delay(500);

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

    static async void ClientStartConnectAndReceive(TcpClient client)
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


