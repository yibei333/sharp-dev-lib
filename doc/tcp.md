# TCP - TCP 网络通信

提供 TCP 网络通信功能。

## 类

### TcpHelper

TCP 帮助类，提供 TCP 客户端和服务端功能。

### TcpClient

TCP 客户端类。

### TcpServer

TCP 服务端类。

## 扩展方法

### ConnectAsync

连接到 TCP 服务端。

#### 方法签名

```csharp
public static async Task ConnectAsync(this TcpClient client, string host, int port)
```

#### 参数

| 参数 | 类型 | 说明 |
| --- | --- | --- |
| client | TcpClient | TCP 客户端 |
| host | string | 主机地址 |
| port | int | 端口号 |

#### 返回值

表示异步连接任务的 Task。

### SendAsync

发送数据。

#### 方法签名

```csharp
public static async Task SendAsync(this TcpClient client, byte[] data)
```

#### 参数

| 参数 | 类型 | 说明 |
| --- | --- | --- |
| client | TcpClient | TCP 客户端 |
| data | byte[] | 要发送的数据 |

#### 返回值

表示异步发送任务的 Task。

### ReceiveAsync

接收数据。

#### 方法签名

```csharp
public static async Task<byte[]> ReceiveAsync(this TcpClient client)
```

#### 参数

| 参数 | 类型 | 说明 |
| --- | --- | --- |
| client | TcpClient | TCP 客户端 |

#### 返回值

接收到的数据。

## 示例

### TCP 客户端

```csharp
// 连接到服务端
var client = new TcpClient();
await client.ConnectAsync("127.0.0.1", 8080);

// 发送数据
var data = Encoding.UTF8.GetBytes("Hello Server");
await client.SendAsync(data);

// 接收数据
var received = await client.ReceiveAsync();
var message = Encoding.UTF8.GetString(received);
Console.WriteLine(message);
```

## 特性

- 支持 TCP 客户端功能
- 支持连接到服务端
- 支持发送和接收数据
- 异步操作
