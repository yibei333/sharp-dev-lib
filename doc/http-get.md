# HTTP GET - HTTP GET 请求

提供 HTTP GET 请求功能。

## 类

### HttpRequest

HTTP 请求类，用于配置和发送 HTTP 请求。

## 扩展方法

### GetAsync

发送 HTTP GET 请求。

#### 方法签名

```csharp
public static async Task<HttpResponse> GetAsync(this HttpRequest request)
```

#### 参数

| 参数 | 类型 | 说明 |
| --- | --- | --- |
| request | HttpRequest | HTTP 请求配置 |

#### 返回值

HTTP 响应对象。

## 示例

### 基本 GET 请求

```csharp
var request = new HttpRequest("https://api.example.com/users");
var response = await request.GetAsync();
Console.WriteLine(response.Content);
```

### 带查询参数的 GET 请求

```csharp
var request = new HttpRequest("https://api.example.com/users")
{
    QueryParams = new Dictionary<string, string>
    {
        { "page", "1" },
        { "size", "10" }
    }
};
var response = await request.GetAsync();
```

### 带请求头的 GET 请求

```csharp
var request = new HttpRequest("https://api.example.com/users")
{
    Headers = new Dictionary<string, string>
    {
        { "Authorization", "Bearer token123" },
        { "Accept", "application/json" }
    }
};
var response = await request.GetAsync();
```

## 特性

- 支持发送 HTTP GET 请求
- 支持查询参数
- 支持自定义请求头
- 异步操作
