using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpDevLib.Standard;
using System;
using System.Net;
using System.Threading.Tasks;

namespace SharpDevLib.Tests.Standard.Transport.Udp;

[TestClass]
public class UdpTests
{
    [TestMethod]
    public async Task Test()
    {
        var clientFactory = new UdpClientFactory();
        var client1 = clientFactory.Create(IPAddress.Loopback, 6000);
        ClientStartReceive(client1);
        var client2 = clientFactory.Create(IPAddress.Loopback, 6001);
        ClientStartReceive(client2);
        await Task.Delay(500);

        client1.Send(IPAddress.Loopback, 6001, "i am client1".ToUtf8Bytes());
        client2.Send(IPAddress.Loopback, 6000, "i am client2".ToUtf8Bytes());
        await Task.Delay(1000);

        client1.Dispose();
        client2.Dispose();
    }

    [TestMethod]
    public async Task ServiceTest()
    {
        IServiceCollection services = new ServiceCollection();
        services.AddUdp();
        var serviceProvider = services.BuildServiceProvider();
        var clientFactory = serviceProvider.GetRequiredService<IUdpClientFactory>();

        var client1 = clientFactory.Create(IPAddress.Loopback, 6002);
        ClientStartReceive(client1);
        var client2 = clientFactory.Create(IPAddress.Loopback, 6003);
        ClientStartReceive(client2);
        await Task.Delay(500);

        client1.Send(IPAddress.Loopback, 6003, "i am client1".ToUtf8Bytes());
        client2.Send(IPAddress.Loopback, 6002, "i am client2".ToUtf8Bytes());
        await Task.Delay(1000);

        client1.Dispose();
        client2.Dispose();
    }

    static async void ClientStartReceive(UdpClient client)
    {
        client.Received += (s, e) =>
        {
            var content = e.Bytes.ToUtf8String();
            var remoteEndPoint = e.RemoteEndPoint as IPEndPoint;
            Console.WriteLine($"client received:{content}");
            if (!content.StartsWith("ok")) e.Client.Send(remoteEndPoint!.Address, remoteEndPoint.Port, $"ok,{e.Bytes.ToUtf8String()}".ToUtf8Bytes());
        };
        client.Error += (s, e) =>
        {
            Console.WriteLine($"client error:{e.Exception.Message}");
        };
        await client.ReceiveAsync();
    }
}


