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

    static void StartListener(TcpListener listener)
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
        listener.Listen();
    }

    static void ClientStartConnectAndReceive(TcpClient client)
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
        client.ConnectAndReceive();
    }

    public TestContext TestContext { get; set; }
}

[TestClass]
public class AA
{
    [TestMethod]
    public async Task BBAsync()
    {
        var port = TcpHelper.GetAvailableTcpPort(8000, 9000);
        StartListen(port);
        var client1 = TcpHelper.CreateClient(IPAddress.Loopback, port);
        var client2 = TcpHelper.CreateClient(IPAddress.Loopback, port);
        StartClientTask(client1);
        StartClientTask(client2);
        Console.WriteLine("准备发送");
        client1.Send("登录:1".Utf8Decode());
        client2.Send("登录:2".Utf8Decode());
        client1.Send("发送:2,你叫什么名字".Utf8Decode());
        client2.Send("发送:2,我叫bar".Utf8Decode());

        await Task.Delay(TimeSpan.FromSeconds(5), CancellationToken.None);
        Console.WriteLine("complete");

        static void StartListen(int port)
        {
            var listener = TcpHelper.CreateListener<UserInfo>(IPAddress.Loopback, port);
            listener.SessionAdded += (s, e) =>
            {
                Console.WriteLine("会话连接");
                e.Session.Received += (ss, ee) =>
                {
                    var message = ee.Bytes.Utf8Encode();

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
            listener.Listen();
        }

        static void StartClientTask(TcpClient client)
        {
            client.Received += (s, e) => Console.WriteLine(e.Bytes.Utf8Encode());
            client.ConnectAndReceive();
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
        readonly static List<UserInfo> _users = [
            new UserInfo{Id=1,Name="foo"},
        new UserInfo{Id=2,Name="bar"},
    ];

        public static UserInfo FindUser(int id)
        {
            return _users.FirstOrDefault(x => x.Id == id) ?? throw new Exception($"找不到Id为'{id}'的用户");
        }
    }
}
