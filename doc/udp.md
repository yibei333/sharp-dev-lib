# UDP - UDP 网络通信

提供 UDP 网络通信功能。

## 类

### UdpHelper

UDP 帮助类，提供 UDP 客户端功能。

### UdpClient

UDP 客户端类。

## 扩展方法

### SendAsync

发送数据。

#### 方法签名

```csharp
public static async Task SendAsync(this UdpClient client, byte[] data, string host, int port)
```

#### 参数

| 参数 | 类型 | 说明 |
| --- | --- | --- |
| client | UdpClient | UDP 客户端 |
| data | byte[] | 要发送的数据 |
| host | string | 主机地址 |
| port | int | 端口号 |

#### 返回值

表示异步发送任务的 Task。

### ReceiveAsync

接收数据。

#### 方法签名

```csharp
public static async Task<byte[]> ReceiveAsync(this UdpClient client)
```

#### 参数

| 参数 | 类型 | 说明 |
| --- | --- | --- |
| client | UdpClient | UDP 客户端 |

#### 返回值

接收到的数据。

## 示例

### UDP 客户端

```csharp
// 发送数据
var client = new UdpClient();
var data = Encoding.UTF8.GetBytes("Hello UDP");
await client.SendAsync(data, "127.0.0.1", 8080);

// 接收数据
var received = await client.ReceiveAsync();
var message = Encoding.UTF8.GetString(received);
Console.WriteLine(message);
```

## 特性

- 支持 UDP 客户端功能
- 支持发送和接收数据
- 支持指定目标主机和端口
- 异步操作
