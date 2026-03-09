# HTTP POST

提供`HTTP` POST 请求功能。

##### 实例

```csharp
using SharpDevLib;
using System.Net;

//基本POST请求（JSON格式）
var request = new HttpRequest("https://api.example.com/users", @"{""name"":""张三"",""age"":25}");
var response = await request.PostAsync();
Console.WriteLine(response.Content);
//{"id":1,"name":"张三","age":25}

//使用对象序列化为JSON
var data = new { name = "李四", age = 30 };
var json = data.Serialize();
var request = new HttpRequest("https://api.example.com/users", json);
var response = await request.PostAsync();

//发送表单数据
var request = new HttpRequest("https://api.example.com/form")
{
    Parameters = new Dictionary<string, string?>
    {
        { "username", "admin" },
        { "password", "123456" }
    }
};
var response = await request.PostAsync();

//链式调用添加表单参数
var request = new HttpRequest("https://api.example.com/form")
    .AddParameter("field1", "value1")
    .AddParameter("field2", "value2");
var response = await request.PostAsync();

//发送多部分表单数据（带文件）
var fileBytes = File.ReadAllBytes("document.pdf");
var request = new HttpRequest("https://api.example.com/upload")
{
    Files = new List<HttpFormFile>
    {
        new HttpFormFile("file", "document.pdf", fileBytes)
    },
    Parameters = new Dictionary<string, string?>
    {
        { "description", "test file" }
    }
};
var response = await request.PostAsync();

//使用文件流上传
using var fileStream = File.OpenRead("image.jpg");
var request = new HttpRequest("https://api.example.com/upload")
{
    Files = new List<HttpFormFile>
    {
        new HttpFormFile("file", "image.jpg", fileStream)
    }
};
var response = await request.PostAsync();

//带请求头的POST请求
var request = new HttpRequest("https://api.example.com/users", @"{""name"":""王五""}")
{
    Headers = new Dictionary<string, string[]>
    {
        { "Authorization", ["Bearer token123"] },
        { "Content-Type", ["application/json"] }
    }
};
var response = await request.PostAsync();

//添加Cookie
var request = new HttpRequest("https://api.example.com/login", @"{""username"":""admin"",""password"":""123456""}")
    .AddCookie(new Cookie("sessionId", "xyz789", "/", "api.example.com"));
var response = await request.PostAsync();

//配置重试和超时
var config = new HttpConfig
{
    Timeout = TimeSpan.FromSeconds(30),
    RetryCount = 3
};
var request = new HttpRequest("https://api.example.com/users", @"{""name"":""test""}")
{
    Config = config
};
var response = await request.PostAsync();
Console.WriteLine($"重试次数: {response.RetryCount}");

//监控上传进度
var progress = new HttpProgress();
config = new HttpConfig
{
    OnSendProgress = p =>
    {
        Console.WriteLine($"上传进度: {p.ProgressString}");
        Console.WriteLine($"上传速度: {p.Speed}");
    }
};
var request = new HttpRequest("https://api.example.com/upload")
{
    Files = new List<HttpFormFile>
    {
        new HttpFormFile("file", "largefile.zip", File.ReadAllBytes("largefile.zip"))
    },
    Config = config
};
var response = await request.PostAsync();

//处理错误响应
var request = new HttpRequest("https://api.example.com/users", @"{""name"":""test""}");
var response = await request.PostAsync();
if (!response.IsSuccessStatusCode)
{
    Console.WriteLine($"错误: {response.Message}");
    Console.WriteLine($"状态码: {response.StatusCode}");
}

//发送大文件时显示进度
var filePath = "largefile.zip";
config = new HttpConfig
{
    OnSendProgress = p =>
    {
        Console.Write($"\r进度: {p.ProgressString} ({p.Transfered.ToFileSizeString()}/{p.Total.ToFileSizeString()}) 速度: {p.Speed}");
    }
};
var request = new HttpRequest("https://api.example.com/upload")
{
    Files = new List<HttpFormFile>
    {
        new HttpFormFile("file", Path.GetFileName(filePath), File.ReadAllBytes(filePath))
    },
    Config = config
};
Console.WriteLine("开始上传...");
await request.PostAsync();
Console.WriteLine("\n上传完成!");
```

## 相关文档
- [HTTP GET](http-get.md)
- [HTTP PUT](http-put.md)
- [HTTP DELETE](http-delete.md)
- [网络传输](../README.md#网络传输)
