using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace SharpDevLib.Tests.Transport.Udp;

[TestClass]
public class UdpTests
{
    [TestMethod]
    public void GetAvailbelUdpTest()
    {
        var port = UdpHelper.GetAvailableUdpPort(50, 100);
        Assert.IsGreaterThanOrEqualTo(50, port);
        Assert.IsLessThanOrEqualTo(100, port);
    }

    [TestMethod]
    public async Task Test()
    {
        var client1 = UdpHelper.CreateClient(IPAddress.Loopback, 6000);
        ClientStartReceive(client1);
        var client2 = UdpHelper.CreateClient(IPAddress.Loopback, 6001);
        ClientStartReceive(client2);
        await Task.Delay(500, CancellationToken.None);

        client1.Send(IPAddress.Loopback, 6001, "i am client1".Utf8Decode());
        client2.Send(IPAddress.Loopback, 6000, "i am client2".Utf8Decode());
        await Task.Delay(1000, CancellationToken.None);

        client1.Dispose();
        client2.Dispose();

        static void ClientStartReceive(UdpClient client)
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
            client.StartReceive();
        }
    }
}