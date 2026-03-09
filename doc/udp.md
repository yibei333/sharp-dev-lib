# UDP

提供`UDP`网络通信功能。

##### 实例

```csharp
using SharpDevLib;
using System.Net;

//获取可用UDP端口
var port = UdpHelper.GetAvailableUdpPort(8000, 9000);
Console.WriteLine(port);
//8000

//检查指定范围内是否有可用端口
var port = UdpHelper.GetAvailableUdpPort(1024, 65535);
if (port == -1)
{
    Console.WriteLine("无可用端口");
}
else
{
    Console.WriteLine($"可用端口: {port}");
}

//创建UDP客户端
var client = UdpHelper.CreateClient();
client.Received += (sender, e) =>
{
    var message = e.Bytes.Utf8Encode();
    Console.WriteLine($"收到来自 {e.RemoteEndPoint} 的消息: {message}");
};
client.Error += (sender, e) =>
{
    Console.WriteLine($"错误: {e.Exception.Message}");
    if (e.RemoteEndPoint is not null)
    {
        Console.WriteLine($"远程端点: {e.RemoteEndPoint}");
    }
};
await client.ReceiveAsync();

//创建绑定到本地地址的UDP客户端
var client = UdpHelper.CreateClient(IPAddress.Parse("192.168.1.100"), 12345);
await client.ReceiveAsync();

//发送数据到指定远程端点
client.Send(IPAddress.Parse("192.168.1.1"), 8080, "Hello UDP".Utf8Decode());

//发送数据到多个目标
var targets = new[]
{
    (IPAddress.Parse("192.168.1.1"), 8080),
    (IPAddress.Parse("192.168.1.2"), 8080),
    (IPAddress.Parse("192.168.1.3"), 8080)
};
foreach (var (ip, port) in targets)
{
    client.Send(ip, port, "Broadcast Message".Utf8Decode());
}

//处理数据发送完成事件
client.Sended += (sender, e) =>
{
    Console.WriteLine($"已发送: {e.Bytes.Utf8Encode()}");
};
client.Send(IPAddress.Parse("192.168.1.1"), 8080, "test".Utf8Decode());

//UDP服务端示例
var server = UdpHelper.CreateClient(IPAddress.Parse("127.0.0.1"), 8080);
var clients = new Dictionary<EndPoint, (string Name, DateTime LastActive)>();
server.Received += (sender, e) =>
{
    var message = e.Bytes.Utf8Encode();
    Console.WriteLine($"收到来自 {e.RemoteEndPoint} 的消息: {message}");

    if (message.StartsWith("REGISTER:"))
    {
        var name = message.Substring(9);
        clients[e.RemoteEndPoint] = (name, DateTime.Now);
        server.Send(((IPEndPoint)e.RemoteEndPoint).Address, ((IPEndPoint)e.RemoteEndPoint).Port,
            $"注册成功, {name}".Utf8Decode());
    }
    else if (message == "LIST")
    {
        var clientList = string.Join(", ", clients.Values.Select(x => x.Name));
        server.Send(((IPEndPoint)e.RemoteEndPoint).Address, ((IPEndPoint)e.RemoteEndPoint).Port,
            $"在线客户端: {clientList}".Utf8Decode());
    }
    else if (message.StartsWith("SEND:"))
    {
        var parts = message.Substring(5).Split(':');
        if (parts.Length == 2)
        {
            var targetName = parts[0];
            var content = parts[1];
            var targetEndpoint = clients.FirstOrDefault(x => x.Value.Name == targetName).Key;
            if (targetEndpoint is not null)
            {
                server.Send(((IPEndPoint)targetEndpoint).Address, ((IPEndPoint)targetEndpoint).Port,
                    content.Utf8Decode());
            }
        }
    }
    else
    {
        server.Send(((IPEndPoint)e.RemoteEndPoint).Address, ((IPEndPoint)e.RemoteEndPoint).Port,
            "未知命令".Utf8Decode());
    }

    //更新最后活跃时间
    if (clients.ContainsKey(e.RemoteEndPoint))
    {
        clients[e.RemoteEndPoint] = (clients[e.RemoteEndPoint].Name, DateTime.Now);
    }
};
await server.ReceiveAsync();

//UDP客户端示例
var client = UdpHelper.CreateClient();
client.Received += (sender, e) =>
{
    var message = e.Bytes.Utf8Encode();
    Console.WriteLine($"收到: {message}");
};
client.Error += (sender, e) =>
{
    Console.WriteLine($"错误: {e.Exception.Message}");
};
await client.ReceiveAsync();

client.Send(IPAddress.Parse("127.0.0.1"), 8080, "REGISTER:Alice".Utf8Decode());
client.Send(IPAddress.Parse("127.0.0.1"), 8080, "LIST".Utf8Decode());
client.Send(IPAddress.Parse("127.0.0.1"), 8080, "SEND:Bob:Hello Bob".Utf8Decode());

//心跳检测（服务端）
var server = UdpHelper.CreateClient(IPAddress.Parse("127.0.0.1"), 8080);
var clients = new Dictionary<EndPoint, DateTime>();
server.Received += (sender, e) =>
{
    if (e.Bytes.Utf8Encode() == "HEARTBEAT")
    {
        clients[e.RemoteEndPoint] = DateTime.Now;
        server.Send(((IPEndPoint)e.RemoteEndPoint).Address, ((IPEndPoint)e.RemoteEndPoint).Port,
            "PONG".Utf8Decode());
    }
};
await server.ReceiveAsync();

//定时清理超时客户端
var cleanupTask = Task.Run(async () =>
{
    while (true)
    {
        await Task.Delay(30000); //每30秒检查一次
        var timeout = DateTime.Now.AddSeconds(60);
        var timedOut = clients.Where(x => x.Value < timeout).Select(x => x.Key).ToList();
        foreach (var endpoint in timedOut)
        {
            clients.Remove(endpoint);
            Console.WriteLine($"移除超时客户端: {endpoint}");
        }
    }
});

//使用自定义适配器（需要实现ITransportSendAdapter和ITransportReceiveAdapter）
var customSendAdapter = new CustomSendAdapter();
var customReceiveAdapter = new CustomReceiveAdapter();
var client = UdpHelper.CreateClient(TransportAdapterType.Custom)
{
    SendAdapter = customSendAdapter,
    ReceiveAdapter = customReceiveAdapter
};
await client.ReceiveAsync();

//UDP广播
var broadcastAddress = IPAddress.Parse("192.168.1.255");
client.Send(broadcastAddress, 8080, "Broadcast Message".Utf8Decode());

//多播示例
var multicastAddress = IPAddress.Parse("239.0.0.1");
//注意：多播需要额外的Socket选项设置，此处仅展示发送接口
//client.Send(multicastAddress, 8080, "Multicast Message".Utf8Decode());

//释放资源
client.Dispose();
```

## 相关文档
- [TCP](tcp.md)
- [网络传输](../README.md#网络传输)
