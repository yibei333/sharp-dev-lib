# HTTP DELETE - HTTP DELETE 请求

提供 HTTP DELETE 请求功能。

## 类

### HttpRequest

HTTP 请求类，用于配置和发送 HTTP 请求。

## 扩展方法

### DeleteAsync

发送 HTTP DELETE 请求。

#### 方法签名

```csharp
public static async Task<HttpResponse> DeleteAsync(this HttpRequest request)
```

#### 参数

| 参数 | 类型 | 说明 |
| --- | --- | --- |
| request | HttpRequest | HTTP 请求配置 |

#### 返回值

HTTP 响应对象。

## 示例

### 基本 DELETE 请求

```csharp
var request = new HttpRequest("https://api.example.com/users/1");
var response = await request.DeleteAsync();
Console.WriteLine(response.Content);
```

### 删除资源

```csharp
var request = new HttpRequest("https://api.example.com/users/1")
{
    Headers = new Dictionary<string, string>
    {
        { "Authorization", "Bearer token123" }
    }
};
var response = await request.DeleteAsync();
```

## 特性

- 支持发送 HTTP DELETE 请求
- 支持删除资源
- 支持自定义请求头
- 异步操作
