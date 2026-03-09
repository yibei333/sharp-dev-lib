# HTTP PUT

提供`HTTP` PUT 请求功能。

##### 实例

```csharp
using SharpDevLib;
using System.Net;

//基本PUT请求（JSON格式）
var request = new HttpRequest("https://api.example.com/users/1", @"{""name"":""张三"",""age"":26}");
var response = await request.PutAsync();
Console.WriteLine(response.Content);
//{"id":1,"name":"张三","age":26}

//使用对象序列化为JSON
var data = new { name = "李四", age = 31 };
var json = data.Serialize();
var request = new HttpRequest("https://api.example.com/users/2", json);
var response = await request.PutAsync();

//发送表单数据
var request = new HttpRequest("https://api.example.com/users/1")
{
    Parameters = new Dictionary<string, string?>
    {
        { "name", "王五" },
        { "age", "32" }
    }
};
var response = await request.PutAsync();

//链式调用添加表单参数
var request = new HttpRequest("https://api.example.com/users/1")
    .AddParameter("name", "赵六")
    .AddParameter("age", "33");
var response = await request.PutAsync();

//带请求头的PUT请求
var request = new HttpRequest("https://api.example.com/users/1", @"{""name"":""孙七""}")
{
    Headers = new Dictionary<string, string[]>
    {
        { "Authorization", ["Bearer token123"] },
        { "Content-Type", ["application/json"] }
    }
};
var response = await request.PutAsync();

//添加Cookie
var request = new HttpRequest("https://api.example.com/users/1", @"{""name"":""周八""}")
    .AddCookie(new Cookie("sessionId", "xyz789", "/", "api.example.com"));
var response = await request.PutAsync();

//配置HTTP选项
var config = new HttpConfig
{
    Timeout = TimeSpan.FromSeconds(30),
    RetryCount = 3
};
var request = new HttpRequest("https://api.example.com/users/1", @"{""name"":""test""}")
{
    Config = config
};
var response = await request.PutAsync();
Console.WriteLine($"重试次数: {response.RetryCount}");

//发送多部分表单数据（带文件）
var fileBytes = File.ReadAllBytes("avatar.jpg");
var request = new HttpRequest("https://api.example.com/users/1/avatar")
{
    Files = new List<HttpFormFile>
    {
        new HttpFormFile("file", "avatar.jpg", fileBytes)
    }
};
var response = await request.PutAsync();

//使用文件流更新文件
using var fileStream = File.OpenRead("document.pdf");
var request = new HttpRequest("https://api.example.com/documents/1/file")
{
    Files = new List<HttpFormFile>
    {
        new HttpFormFile("file", "document.pdf", fileStream)
    }
};
var response = await request.PutAsync();

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
var request = new HttpRequest("https://api.example.com/files/1")
{
    Files = new List<HttpFormFile>
    {
        new HttpFormFile("file", "update.zip", File.ReadAllBytes("update.zip"))
    },
    Config = config
};
var response = await request.PutAsync();

//处理错误响应
var request = new HttpRequest("https://api.example.com/users/999", @"{""name"":""test""}");
var response = await request.PutAsync();
if (!response.IsSuccessStatusCode)
{
    Console.WriteLine($"错误: {response.Message}");
    Console.WriteLine($"状态码: {response.StatusCode}");
}

//批量更新多个资源
var resources = new[]
{
    new { id = 1, name = "资源1", status = "active" },
    new { id = 2, name = "资源2", status = "active" }
};
foreach (var resource in resources)
{
    var request = new HttpRequest($"https://api.example.com/resources/{resource.id}", resource.Serialize());
    var response = await request.PutAsync();
    Console.WriteLine($"更新资源 {resource.id}: {response.IsSuccessStatusCode}");
}

//使用全局配置
HttpConfig.Default.Timeout = TimeSpan.FromSeconds(60);
var request = new HttpRequest("https://api.example.com/users/1", @"{""name"":""test""}");
var response = await request.PutAsync();
Console.WriteLine($"耗时: {response.TotalTimeConsuming}");
```

## 相关文档
- [HTTP GET](http-get.md)
- [HTTP POST](http-post.md)
- [HTTP DELETE](http-delete.md)
- [网络传输](../README.md#网络传输)
