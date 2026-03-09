# HTTP DELETE

提供`HTTP` DELETE 请求功能。

##### 实例

```csharp
using SharpDevLib;
using System.Net;

//基本DELETE请求
var request = new HttpRequest("https://api.example.com/users/1");
var response = await request.DeleteAsync();
Console.WriteLine(response.StatusCode);
//NoContent

//带查询参数的DELETE请求
var request = new HttpRequest("https://api.example.com/users")
{
    Parameters = new Dictionary<string, string?>
    {
        { "id", "1" },
        { "confirm", "true" }
    }
};
var response = await request.DeleteAsync();

//链式调用添加参数
var request = new HttpRequest("https://api.example.com/users")
    .AddParameter("id", "1")
    .AddParameter("permanent", "true");
var response = await request.DeleteAsync();

//带请求头的DELETE请求
var request = new HttpRequest("https://api.example.com/users/1")
{
    Headers = new Dictionary<string, string[]>
    {
        { "Authorization", ["Bearer token123"] },
        { "X-Confirm", ["true"] }
    }
};
var response = await request.DeleteAsync();

//添加Cookie
var request = new HttpRequest("https://api.example.com/users/1")
    .AddCookie(new Cookie("sessionId", "xyz789", "/", "api.example.com"));
var response = await request.DeleteAsync();

//配置HTTP选项
var config = new HttpConfig
{
    Timeout = TimeSpan.FromSeconds(30),
    RetryCount = 3
};
var request = new HttpRequest("https://api.example.com/users/1")
{
    Config = config
};
var response = await request.DeleteAsync();
Console.WriteLine($"重试次数: {response.RetryCount}");

//删除并检查响应
var request = new HttpRequest("https://api.example.com/users/1");
var response = await request.DeleteAsync();
if (response.IsSuccessStatusCode)
{
    Console.WriteLine("删除成功");
}
else
{
    Console.WriteLine($"删除失败: {response.Message}");
}

//批量删除多个资源
var ids = new[] { 1, 2, 3, 4, 5 };
foreach (var id in ids)
{
    var request = new HttpRequest($"https://api.example.com/users/{id}");
    var response = await request.DeleteAsync();
    if (response.IsSuccessStatusCode)
    {
        Console.WriteLine($"用户 {id} 删除成功");
    }
    else
    {
        Console.WriteLine($"用户 {id} 删除失败: {response.Message}");
    }
}

//处理软删除
var request = new HttpRequest("https://api.example.com/users/1")
{
    Parameters = new Dictionary<string, string?>
    {
        { "soft", "true" }
    }
};
var response = await request.DeleteAsync();
Console.WriteLine(response.Content);
//{"success":true,"deleted":false,"message":"用户已软删除"}

//删除失败处理
var request = new HttpRequest("https://api.example.com/users/999");
var response = await request.DeleteAsync();
if (!response.IsSuccessStatusCode)
{
    switch (response.StatusCode)
    {
        case HttpStatusCode.NotFound:
            Console.WriteLine("资源不存在");
            break;
        case HttpStatusCode.Forbidden:
            Console.WriteLine("无权限删除");
            break;
        default:
            Console.WriteLine($"删除失败: {response.Message}");
            break;
    }
}

//带确认的删除
var request = new HttpRequest("https://api.example.com/users/1")
{
    Headers = new Dictionary<string, string[]>
    {
        { "X-Confirm-Delete", ["true"] },
        { "X-Reason", ["用户请求"] }
    }
};
var response = await request.DeleteAsync();

//使用全局配置
HttpConfig.Default.Timeout = TimeSpan.FromSeconds(60);
var request = new HttpRequest("https://api.example.com/users/1");
var response = await request.DeleteAsync();
Console.WriteLine($"耗时: {response.TotalTimeConsuming}");

//删除前检查
var checkRequest = new HttpRequest("https://api.example.com/users/1");
var checkResponse = await checkRequest.GetAsync();
if (checkResponse.IsSuccessStatusCode)
{
    var deleteRequest = new HttpRequest("https://api.example.com/users/1");
    var deleteResponse = await deleteRequest.DeleteAsync();
    Console.WriteLine($"删除结果: {deleteResponse.IsSuccessStatusCode}");
}
```

## 相关文档
- [HTTP GET](http-get.md)
- [HTTP POST](http-post.md)
- [HTTP PUT](http-put.md)
- [网络传输](../README.md#网络传输)
