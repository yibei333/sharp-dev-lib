# HTTP POST - HTTP POST 请求

提供 HTTP POST 请求功能。

## 类

### HttpRequest

HTTP 请求类，用于配置和发送 HTTP 请求。

## 扩展方法

### PostAsync

发送 HTTP POST 请求。

#### 方法签名

```csharp
public static async Task<HttpResponse> PostAsync(this HttpRequest request, object? data = null)
```

#### 参数

| 参数 | 类型 | 说明 |
| --- | --- | --- |
| request | HttpRequest | HTTP 请求配置 |
| data | object? | 请求体数据 |

#### 返回值

HTTP 响应对象。

## 示例

### 基本 POST 请求

```csharp
var request = new HttpRequest("https://api.example.com/users");
var data = new { Name = "张三", Age = 25 };
var response = await request.PostAsync(data);
Console.WriteLine(response.Content);
```

### 发送 JSON 数据

```csharp
var request = new HttpRequest("https://api.example.com/users")
{
    ContentType = "application/json"
};
var data = new { Name = "张三", Age = 25 };
var response = await request.PostAsync(data);
```

### 带请求头的 POST 请求

```csharp
var request = new HttpRequest("https://api.example.com/users")
{
    Headers = new Dictionary<string, string>
    {
        { "Authorization", "Bearer token123" }
    }
};
var data = new { Name = "张三", Age = 25 };
var response = await request.PostAsync(data);
```

## 特性

- 支持发送 HTTP POST 请求
- 支持发送 JSON 数据
- 支持自定义请求头
- 异步操作
