# HTTP GET

提供`HTTP` GET 请求功能。

##### 实例

```csharp
using SharpDevLib;
using System.Net;

//基本GET请求
var request = new HttpRequest("https://api.example.com/users");
var response = await request.GetAsync();
Console.WriteLine(response.StatusCode);
//OK
Console.WriteLine(response.Content);
//{"data":[...]}

//带查询参数的GET请求
var request = new HttpRequest("https://api.example.com/users")
{
    Parameters = new Dictionary<string, string?>
    {
        { "page", "1" },
        { "size", "10" },
        { "keyword", "test" }
    }
};
var response = await request.GetAsync();
Console.WriteLine(response.HttpResponseMessage.RequestMessage.RequestUri);
//https://api.example.com/users?page=1&size=10&keyword=test

//链式调用添加参数
var request = new HttpRequest("https://api.example.com/search")
    .AddParameter("q", "keyword")
    .AddParameter("sort", "asc")
    .AddParameter("limit", "20");
var response = await request.GetAsync();

//带请求头的GET请求
var request = new HttpRequest("https://api.example.com/users")
{
    Headers = new Dictionary<string, string[]>
    {
        { "Authorization", ["Bearer token123"] },
        { "Accept", ["application/json"] }
    }
};
var response = await request.GetAsync();

//添加Cookie
var request = new HttpRequest("https://api.example.com/users")
    .AddCookie(new Cookie("sessionId", "abc123", "/", "api.example.com"));
var response = await request.GetAsync();

//配置HTTP选项
var config = new HttpConfig
{
    Timeout = TimeSpan.FromSeconds(30),
    RetryCount = 3,
    UserAgent = "MyApp/1.0"
};
var request = new HttpRequest("https://api.example.com/users")
{
    Config = config
};
var response = await request.GetAsync();

//监控下载进度
var progress = new HttpProgress();
config = new HttpConfig
{
    OnReceiveProgress = p =>
    {
        Console.WriteLine($"进度: {p.ProgressString}");
        Console.WriteLine($"速度: {p.Speed}");
    }
};
var request = new HttpRequest("https://example.com/largefile.zip")
{
    Config = config
};
var response = await request.GetAsync();
Console.WriteLine(progress.ToString());
//********progress********
//Total:10485760
//Transfered:10485760
//Progress:100
//ProgressString:100%
//Speed:1024KB/s

//获取响应流（用于下载文件）
var request = new HttpRequest("https://example.com/file.zip");
using var stream = await request.GetStreamAsync();
using var fileStream = File.Create("file.zip");
await stream.CopyToAsync(fileStream);

//检查响应状态
var request = new HttpRequest("https://api.example.com/users");
var response = await request.GetAsync();
if (response.IsSuccessStatusCode)
{
    Console.WriteLine("请求成功");
}
else
{
    Console.WriteLine($"请求失败: {response.Message}");
}

//使用全局配置
HttpConfig.Default.Timeout = TimeSpan.FromSeconds(60);
HttpConfig.Default.UserAgent = "DefaultUserAgent";
var request = new HttpRequest("https://api.example.com/users");
var response = await request.GetAsync();
```

## 相关文档
- [HTTP POST](http-post.md)
- [HTTP PUT](http-put.md)
- [HTTP DELETE](http-delete.md)
- [网络传输](../README.md#网络传输)
