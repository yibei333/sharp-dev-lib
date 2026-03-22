# HTTP PUT

提供`HTTP` PUT 请求功能。

##### 简单示例
``` csharp
using SharpDevLib;

var json = new { id = 1, userId = 1, title = "foo", body = "bar" }.Serialize();
var response = await HttpHelper
    .NewRequest("https://jsonplaceholder.typicode.com/posts/1")
    .AddJson(json)
    .PutAsync();
Console.WriteLine(response);
//****request****
//url:https://jsonplaceholder.typicode.com/posts/1
//method:PUT
//headers:
//User-Agent:Mozilla/5.0,(Windows NT 10.0; Win64; x64),AppleWebKit/537.36,(KHTML, like Gecko),Chrome/124.0.0.0,Safari/537.36,Edg/124.0.0.0
//Content-Type:application/json; charset=utf-8
//Content-Length:46
//content-type:application/json; charset=utf-8
//json:
//{"id":1,"userId":1,"title":"foo","body":"bar"}
//****response****
//code:OK
//headers:
//Date:Wed, 11 Mar 2026 08:00:22 GMT
//Connection:keep-alive
//Access-Control-Allow-Credentials:true
//Cache-Control:no-cache
//ETag:W/"3f-2+fbgxI9e+sa2n+BkM69S9SdnIY"
//nel:{"report_to":"heroku-nel","response_headers":["Via"],"max_age":3600,"success_fraction":0.01,"failure_fraction":0.1}
//Pragma:no-cache
//report-to:{"group":"heroku-nel","endpoints":[{"url":"https://nel.heroku.com/reports?s=hKx0Fbp4ZO0I71dQaOhWOIKQsSRr1jUQORw2jyP80f0%3D\u0026sid=e11707d5-02a7-43ef-b45e-2cf4d2036f7d\u0026ts=1773216021"}],"max_age":3600}
//reporting-endpoints:heroku-nel="https://nel.heroku.com/reports?s=hKx0Fbp4ZO0I71dQaOhWOIKQsSRr1jUQORw2jyP80f0%3D&sid=e11707d5-02a7-43ef-b45e-2cf4d2036f7d&ts=1773216021"
//Server:cloudflare
//Vary:Origin,Accept-Encoding
//Via:2.0 heroku-router
//X-Content-Type-Options:nosniff
//X-Powered-By:Express
//x-ratelimit-limit:1000
//x-ratelimit-remaining:999
//x-ratelimit-reset:1773216037
//cf-cache-status:DYNAMIC
//Server-Timing:cfCacheStatus;desc="DYNAMIC",cfEdge;dur=9,cfOrigin;dur=256
//CF-RAY:9da906681ad80c13-AMS
//Alt-Svc:h3=":443"
//Content-Type:application/json; charset=utf-8
//Content-Length:63
//Expires:-1
//reply:
//{
//  "id": 1,
//  "userId": 1,
//  "title": "foo",
//  "body": "bar"
//}
```

##### 完整示例

```csharp
using System.Net;
using SharpDevLib;

HttpHelper.SetConfig("some id", new HttpConfig
{
    Timeout = TimeSpan.FromSeconds(10),
    RetryCount = 5,
    OnSendProgress = (p) =>
    {
        Console.WriteLine($"进度: {p.ProgressString}");
        Console.WriteLine($"速度: {p.Speed}");
    },
    UserAgent = null
});

var json = new { id = 1, userId = 1, title = "foo", body = "bar" }.Serialize();
var text = await HttpHelper
    .NewRequest("https://jsonplaceholder.typicode.com/posts/1")
    .UseClientId("some id")
    .AddHeader("Authorization", ["Bearer token123"])
    .AddCookie(new Cookie("foo", "bar", "/", jsonplaceholder.typicode.com"))
    .AddJson(json)
    .PutAsync()
    .EnsureSuccessStatusCode()
    .ReadAsStringAsync();
Console.WriteLine(text);
//{
//  "id": 1,
//  "userId": 1,
//  "title": "foo",
//  "body": "bar"
//}
```

## 相关文档
- [HTTP GET](http-get.md)
- [HTTP POST](http-post.md)
- [HTTP DELETE](http-delete.md)
- [网络传输](../README.md#网络传输)
