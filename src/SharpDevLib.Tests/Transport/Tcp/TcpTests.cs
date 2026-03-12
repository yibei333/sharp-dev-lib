using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
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
    public async Task ChatAsync()
    {
        var port = TcpHelper.GetAvailableTcpPort(8000, 9000);
        StartListen(port);
        var client1 = TcpHelper.CreateClient(IPAddress.Loopback, port, adapter: TcpAdapters.FixedHeader);
        var client2 = TcpHelper.CreateClient(IPAddress.Loopback, port, adapter: TcpAdapters.FixedHeader);
        StartClient(client1, nameof(client1));
        StartClient(client2, nameof(client2));

        client1.Send("登录:1".Utf8Decode());
        client2.Send("登录:2".Utf8Decode());
        await Task.Delay(TimeSpan.FromMilliseconds(100), CancellationToken.None);

        client1.Send("发送:2;你叫什么名字".Utf8Decode());
        await Task.Delay(TimeSpan.FromMilliseconds(100), CancellationToken.None);

        client2.Send("发送:1;我叫bar".Utf8Decode());
        await Task.Delay(TimeSpan.FromMilliseconds(100), CancellationToken.None);

        static void StartListen(int port)
        {
            var listener = TcpHelper.CreateListener<IdNameDto<int>>(IPAddress.Loopback, port, adapter: TcpAdapters.FixedHeader);
            listener.SessionAdded += (s, e) =>
            {
                e.Session.Received += (ss, ee) =>
                {
                    var message = ee.Bytes.Utf8Encode();

                    //1.登录
                    if (message.StartsWith("登录:"))
                    {
                        var id = message.TrimStart("登录:").ToInt();
                        var user = UserRepository.FindUser(id);
                        ee.Session.Metadata = user;
                        ee.Session.Send($"登录成功,id:{id},name:{user.Name}".Utf8Decode());
                        return;
                    }

                    //2.转发消息
                    if (message.StartsWith("发送:"))
                    {
                        if (ee.Session.Metadata is null || ee.Session.Metadata.Id <= 0)
                        {
                            ee.Session.Send("请先登录".Utf8Decode());
                            return;
                        }
                        var array = message.TrimStart("发送:").SplitToList(';');
                        var uid = array[0].ToInt();
                        var text = array[1];
                        var toSession = listener.Sessions.FirstOrDefault(x => x.Metadata is not null && x.Metadata.Id == uid);
                        if (toSession is null)
                        {
                            ee.Session.Send("发送失败,对方不在线".Utf8Decode());
                            return;
                        }
                        toSession.Send($"{text}(from:{ee.Session.Metadata.Name})".Utf8Decode());
                        return;
                    }
                };
            };
            listener.StartListen();
        }

        static void StartClient(TcpClient client, string clientName)
        {
            client.Received += (s, e) => Console.WriteLine($"{clientName}收到信息->{e.Bytes.Utf8Encode()}");
            client.StartConnectAndReceive();
        }
    }

    class UserRepository
    {
        readonly static List<IdNameDto<int>> _users =
        [
            new IdNameDto<int>(1,"foo"),
            new IdNameDto<int>(2,"bar"),
        ];

        public static IdNameDto<int> FindUser(int id)
        {
            return _users.FirstOrDefault(x => x.Id == id) ?? throw new Exception($"找不到Id为'{id}'的用户");
        }
    }
}

