# TCP

提供`TCP`网络通信功能。

##### 模拟通讯示例

```csharp
using System.Net;
using SharpDevLib;

var serverPort = TcpHelper.GetAvailableTcpPort(8000, 9000);
var server = new Server(serverPort);
server.StartListen();

var client1 = new Client("客户端1", serverPort);
client1.StartConnectAndReceive();
client1.Login(1);

var client2 = new Client("客户端2", serverPort);
client2.StartConnectAndReceive();
client2.Login(2);

await Task.Delay(TimeSpan.FromMilliseconds(100), CancellationToken.None);

client1.SendMessage(2, "你叫什么名字?");
await Task.Delay(TimeSpan.FromMilliseconds(100), CancellationToken.None);

client2.SendMessage(1, "我叫bar");
await Task.Delay(TimeSpan.FromMilliseconds(100), CancellationToken.None);

//客户端1收到信息->登录成功,id:1,name:foo
//客户端2收到信息->登录成功,id:2,name:bar
//客户端2收到信息->你叫什么名字?(from:foo)
//客户端1收到信息->我叫bar(from:bar)

class Server
{
    public Server(int port)
    {
        Listener = TcpHelper.CreateListener<IdNameDto<int>>(IPAddress.Loopback, port, adapter: TcpAdapters.FixedHeader);
        Listener.SessionAdded += SessionAdded;
    }

    TcpListener<IdNameDto<int>> Listener { get; }

    readonly List<IdNameDto<int>> _users =
    [
        new IdNameDto<int>(1,"foo"),
        new IdNameDto<int>(2,"bar"),
    ];

    IdNameDto<int> FindUser(int id)
    {
        return _users.FirstOrDefault(x => x.Id == id) ?? throw new Exception($"找不到Id为'{id}'的用户");
    }

    public void StartListen()
    {
        Listener.StartListen();
    }

    void SessionAdded(object? sender, TcpSessionEventArgs<IdNameDto<int>> args)
    {
        args.Session.Received += Received;
    }

    void Received(object? sender, TcpSessionDataEventArgs<IdNameDto<int>> args)
    {
        var message = args.Bytes.Utf8Encode();
        if (message.StartsWith("登录:")) HandleLogin(args.Session, message.TrimStart("登录:"));
        else if (message.StartsWith("发送:")) HandleSend(args.Session, message.TrimStart("发送:"));
        else Console.WriteLine($"未定义的消息:{message}");
    }

    void HandleLogin(TcpSession<IdNameDto<int>> session, string message)
    {
        var id = message.ToInt();
        var user = FindUser(id);
        session.Metadata = user;
        session.Send($"登录成功,id:{id},name:{user.Name}".Utf8Decode());
    }

    void HandleSend(TcpSession<IdNameDto<int>> session, string message)
    {
        if (session.Metadata is null || session.Metadata.Id <= 0)
        {
            session.Send("请先登录".Utf8Decode());
            return;
        }
        var array = message.SplitToList(';');
        var uid = array[0].ToInt();
        var text = array[1];
        var toSession = Listener.Sessions.FirstOrDefault(x => x.Metadata is not null && x.Metadata.Id == uid);
        if (toSession is null)
        {
            session.Send("发送失败,对方不在线".Utf8Decode());
            return;
        }
        toSession.Send($"{text}(from:{session.Metadata.Name})".Utf8Decode());
    }
}

class Client
{
    public Client(string name, int serverPort)
    {
        Name = name;
        TcpClient = TcpHelper.CreateClient(IPAddress.Loopback, serverPort, adapter: TcpAdapters.FixedHeader);
        TcpClient.Received += Received;
    }

    string Name { get; }

    public TcpClient TcpClient { get; }

    void Received(object? sender, TcpClientDataEventArgs args)
    {
        Console.WriteLine($"{Name}收到信息->{args.Bytes.Utf8Encode()}");
    }

    public void StartConnectAndReceive()
    {
        TcpClient.StartConnectAndReceive();
    }

    public void Login(int id)
    {
        TcpClient.Send($"登录:{id}".Utf8Decode());
    }

    public void SendMessage(int toId, string message)
    {
        TcpClient.Send($"发送:{toId};{message}".Utf8Decode());
    }
}
```

## 相关文档
- [UDP](udp.md)
- [网络传输](../README.md#网络传输)
