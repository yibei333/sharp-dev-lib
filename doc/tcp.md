# TCP

提供`TCP`网络通信功能。

##### 实例

```csharp
using SharpDevLib;
using System.Net;

//获取可用TCP端口
var port = TcpHelper.GetAvailableTcpPort(8000, 9000);
Console.WriteLine(port);
//8000

//检查指定范围内是否有可用端口
var port = TcpHelper.GetAvailableTcpPort(1024, 65535);
if (port == -1)
{
    Console.WriteLine("无可用端口");
}
else
{
    Console.WriteLine($"可用端口: {port}");
}

//创建TCP客户端
var client = TcpHelper.CreateClient(IPAddress.Parse("127.0.0.1"), 8080);
client.Received += (sender, e) =>
{
    var message = e.Bytes.Utf8Encode();
    Console.WriteLine($"收到: {message}");
};
client.Error += (sender, e) =>
{
    Console.WriteLine($"错误: {e.Exception.Message}");
};
await client.ConnectAndReceiveAsync();

//使用指定本地地址创建TCP客户端
var client = TcpHelper.CreateClient(
    IPAddress.Parse("192.168.1.100"),
    12345,
    IPAddress.Parse("192.168.1.1"),
    8080
);
await client.ConnectAndReceiveAsync();

//发送数据
client.Send("Hello Server".Utf8Decode());

//监听客户端状态变化
client.StateChanged += (sender, e) =>
{
    Console.WriteLine($"状态变化: {e.Before} -> {e.After}");
};
await client.ConnectAndReceiveAsync();

//处理数据发送完成事件
client.Sended += (sender, e) =>
{
    Console.WriteLine($"已发送: {e.Bytes.Utf8Encode()}");
};
client.Send("test".Utf8Decode());

//创建TCP监听器
var listener = TcpHelper.CreateListener(IPAddress.Parse("127.0.0.1"), 8080);
listener.SessionAdded += (sender, e) =>
{
    Console.WriteLine($"新会话: {e.Session}");
};
listener.SessionRemoved += (sender, e) =>
{
    Console.WriteLine($"会话移除: {e.Session}");
};
await listener.ListenAsync();

//创建带元数据的TCP监听器
var listener = TcpHelper.CreateListener(
    IPAddress.Parse("127.0.0.1"),
    8080,
    () => new { Id = Guid.NewGuid(), ConnectTime = DateTime.Now }
);
listener.SessionAdded += (sender, e) =>
{
    Console.WriteLine($"新会话ID: {e.Session.Metadata.Id}");
    Console.WriteLine($"连接时间: {e.Session.Metadata.ConnectTime}");
};
await listener.ListenAsync();

//处理会话数据
listener.SessionAdded += (sender, e) =>
{
    e.Session.Received += (s, args) =>
    {
        var message = args.Bytes.Utf8Encode();
        Console.WriteLine($"来自 {e.Session} 的消息: {message}");
    };
    e.Session.Sended += (s, args) =>
    {
        Console.WriteLine($"发送至 {e.Session}: {args.Bytes.Utf8Encode()}");
    };
};
await listener.ListenAsync();

//发送数据到所有会话
foreach (var session in listener.Sessions)
{
    session.Send("Broadcast".Utf8Decode());
}

//关闭特定会话
if (listener.Sessions.Any())
{
    var session = listener.Sessions.First();
    session.Close();
}

//关闭监听器
listener.Close();

//完整的TCP服务端示例
var listener = TcpHelper.CreateListener(
    IPAddress.Parse("127.0.0.1"),
    8080,
    () => new { Username = string.Empty }
);
listener.SessionAdded += (sender, e) =>
{
    Console.WriteLine($"客户端连接: {e.Session.RemoteEndPoint}");
    e.Session.Received += (s, args) =>
    {
        var message = args.Bytes.Utf8Encode();
        Console.WriteLine($"收到: {message}");

        if (message.StartsWith("LOGIN:"))
        {
            e.Session.Metadata.Username = message.Substring(6);
            e.Session.Send($"欢迎 {e.Session.Metadata.Username}".Utf8Decode());
        }
        else if (message == "BROADCAST")
        {
            foreach (var session in listener.Sessions.Where(x => x != e.Session))
            {
                session.Send($"来自 {e.Session.Metadata.Username}: {message}".Utf8Decode());
            }
        }
        else if (message == "LIST")
        {
            var users = string.Join(", ", listener.Sessions.Select(x => x.Metadata.Username));
            e.Session.Send($"在线用户: {users}".Utf8Decode());
        }
        else
        {
            e.Session.Send("未知命令".Utf8Decode());
        }
    };
    e.Session.StateChanged += (s, args) =>
    {
        Console.WriteLine($"会话 {e.Session.Metadata.Username} 状态: {args.After}");
    };
};
listener.SessionRemoved += (sender, e) =>
{
    Console.WriteLine($"客户端断开: {e.Session.Metadata.Username}");
};
await listener.ListenAsync();

//完整的TCP客户端示例
var client = TcpHelper.CreateClient(IPAddress.Parse("127.0.0.1"), 8080);
client.Received += (sender, e) =>
{
    var message = e.Bytes.Utf8Encode();
    Console.WriteLine($"收到: {message}");
};
client.Error += (sender, e) =>
{
    Console.WriteLine($"错误: {e.Exception.Message}");
};
client.StateChanged += (sender, e) =>
{
    Console.WriteLine($"状态: {e.After}");
};
await client.ConnectAndReceiveAsync();

client.Send("LOGIN:Alice".Utf8Decode());
client.Send("BROADCAST".Utf8Decode());
client.Send("LIST".Utf8Decode());

//使用自定义适配器（需要实现ITransportSendAdapter和ITransportReceiveAdapter）
var customSendAdapter = new CustomSendAdapter();
var customReceiveAdapter = new CustomReceiveAdapter();
var client = TcpHelper.CreateClient(IPAddress.Parse("127.0.0.1"), 8080, TransportAdapterType.Custom)
{
    SendAdapter = customSendAdapter,
    ReceiveAdapter = customReceiveAdapter
};
await client.ConnectAndReceiveAsync();
```

## 相关文档
- [UDP](udp.md)
- [网络传输](../README.md#网络传输)
