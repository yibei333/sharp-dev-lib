# HTTP PUT - HTTP PUT 请求

提供 HTTP PUT 请求功能。

## 类

### HttpRequest

HTTP 请求类，用于配置和发送 HTTP 请求。

## 扩展方法

### PutAsync

发送 HTTP PUT 请求。

#### 方法签名

```csharp
public static async Task<HttpResponse> PutAsync(this HttpRequest request, object? data = null)
```

#### 参数

| 参数 | 类型 | 说明 |
| --- | --- | --- |
| request | HttpRequest | HTTP 请求配置 |
| data | object? | 请求体数据 |

#### 返回值

HTTP 响应对象。

## 示例

### 基本 PUT 请求

```csharp
var request = new HttpRequest("https://api.example.com/users/1");
var data = new { Name = "李四", Age = 30 };
var response = await request.PutAsync(data);
Console.WriteLine(response.Content);
```

### 更新资源

```csharp
var request = new HttpRequest("https://api.example.com/users/1")
{
    ContentType = "application/json"
};
var data = new { Name = "李四", Age = 30 };
var response = await request.PutAsync(data);
```

## 特性

- 支持发送 HTTP PUT 请求
- 支持更新资源
- 支持发送 JSON 数据
- 异步操作
