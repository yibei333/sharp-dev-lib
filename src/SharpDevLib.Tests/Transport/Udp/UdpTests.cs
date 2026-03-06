using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net;
using System.Threading.Tasks;

namespace SharpDevLib.Tests.Transport.Udp;

[TestClass]
public class UdpTests
{
    [TestMethod]
    public async Task Test()
    {
        var client1 = UdpHelper.CreateClient(IPAddress.Loopback, 6000);
        ClientStartReceive(client1);
        var client2 = UdpHelper.CreateClient(IPAddress.Loopback, 6001);
        ClientStartReceive(client2);
        await Task.Delay(500, TestContext.CancellationTokenSource.Token);

        client1.Send(IPAddress.Loopback, 6001, "i am client1".Utf8Decode());
        client2.Send(IPAddress.Loopback, 6000, "i am client2".Utf8Decode());
        await Task.Delay(1000, TestContext.CancellationTokenSource.Token);

        client1.Dispose();
        client2.Dispose();
    }

    static async void ClientStartReceive(UdpClient client)
    {
        client.Received += (s, e) =>
        {
            var content = e.Bytes.Utf8Encode();
            var remoteEndPoint = e.RemoteEndPoint as IPEndPoint;
            Console.WriteLine($"client received:{content}");
            if (!content.StartsWith("ok")) e.Client.Send(remoteEndPoint!.Address, remoteEndPoint.Port, $"ok,{e.Bytes.Utf8Encode()}".Utf8Decode());
        };
        client.Error += (s, e) =>
        {
            Console.WriteLine($"client error:{e.Exception.Message}");
        };
        await client.ReceiveAsync();
    }

    public TestContext TestContext { get; set; }
}


