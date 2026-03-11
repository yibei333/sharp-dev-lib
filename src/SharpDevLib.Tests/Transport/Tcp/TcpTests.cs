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
        var client1 = TcpHelper.CreateClient(IPAddress.Loopback, port);
        var client2 = TcpHelper.CreateClient(IPAddress.Loopback, port);
        StartClientTask(client1);
        StartClientTask(client2);
        await Task.Delay(TimeSpan.FromMilliseconds(100), CancellationToken.None);
        Console.WriteLine("准备发送");
        client1.Send("登录:1".Utf8Decode());
        client2.Send("登录:2".Utf8Decode());
        client1.Send("发送:2;你叫什么名字".Utf8Decode());
        client2.Send("发送:1;我叫bar".Utf8Decode());
        await Task.Delay(TimeSpan.FromMilliseconds(100), CancellationToken.None);

        static void StartListen(int port)
        {
            var listener = TcpHelper.CreateListener<UserInfo>(IPAddress.Loopback, port);
            listener.StateChanged += (s, e) =>
            {
                Console.WriteLine($"服务端状态变化:{e.Before}->{e.Current}");
            };
            listener.SessionAdded += (s, e) =>
            {
                Console.WriteLine("会话连接");
                e.Session.Received += (ss, ee) =>
                {
                    var message = ee.Bytes.Utf8Encode();
                    Console.WriteLine($"服务器收到消息:{message}");

                    //1.登录
                    if (message.StartsWith("登录:"))
                    {
                        var id = message.TrimStart("登录:").ToInt();
                        var user = UserRepository.FindUser(id);
                        ee.Session.Metadata = user;
                        ee.Session.Send($"{user.Name}登录成功".Utf8Decode());
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
                        toSession.Send($"{toSession.Metadata!.Name}收到来自{ee.Session.Metadata.Name}的消息:{text}".Utf8Decode());
                        return;
                    }
                };
            };
            listener.StartListen();
        }

        static void StartClientTask(TcpClient client)
        {
            client.Received += (s, e) => Console.WriteLine(e.Bytes.Utf8Encode());
            client.StateChanged += (s, e) => Console.WriteLine($"客户端状态变化:{e.Before}->{e.Current}");
            client.StartConnectAndReceive();
        }
    }
}

class UserInfo
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public IPAddress? Address { get; set; }
    public int Port { get; set; }
}

class UserRepository
{
    readonly static List<UserInfo> _users =
    [
        new UserInfo{Id=1,Name="foo"},
        new UserInfo{Id=2,Name="bar"},
    ];

    public static UserInfo FindUser(int id)
    {
        return _users.FirstOrDefault(x => x.Id == id) ?? throw new Exception($"找不到Id为'{id}'的用户");
    }
}
