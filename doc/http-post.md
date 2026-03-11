# HTTP POST

提供`HTTP` POST 请求功能。

##### 简单实例
``` csharp
using SharpDevLib;

var json = new { userId = 1, title = "foo", body = "bar" }.Serialize();
var response=await new HttpRequest("https://jsonplaceholder.typicode.com/posts")
                    .AddJson(json)
                    .PostAsync();
Console.WriteLine(response);
//****request****
//url:https://jsonplaceholder.typicode.com/posts
//method:POST
//headers:
//User-Agent:Mozilla/5.0,(Windows NT 10.0; Win64; x64),AppleWebKit/537.36,(KHTML, like Gecko),Chrome/124.0.0.0,Safari/537.36,Edg/124.0.0.0
//Content-Type:application/json; charset=utf-8
//Content-Length:39
//content-type:application/json; charset=utf-8
//json:
//{"userId":1,"title":"foo","body":"bar"}
//****response****
//code:Created
//headers:
//Date:Wed, 11 Mar 2026 07:51:31 GMT
//Connection:keep-alive
//Access-Control-Allow-Credentials:true
//Access-Control-Expose-Headers:Location
//Cache-Control:no-cache
//ETag:W/"41-EnFAIaFe//2cDeroiws5hNclP8k"
//Location:https://jsonplaceholder.typicode.com/posts/101
//nel:{"report_to":"heroku-nel","response_headers":["Via"],"max_age":3600,"success_fraction":0.01,"failure_fraction":0.1}
//Pragma:no-cache
//report-to:{"group":"heroku-nel","endpoints":[{"url":"https://nel.heroku.com/reports?s=jE5A0mn3CEB0le8CeJWlTr4Vu22oOKtRKLBXl0T9LUI%3D\u0026sid=e11707d5-02a7-43ef-b45e-2cf4d2036f7d\u0026ts=1773215491"}],"max_age":3600}
//reporting-endpoints:heroku-nel="https://nel.heroku.com/reports?s=jE5A0mn3CEB0le8CeJWlTr4Vu22oOKtRKLBXl0T9LUI%3D&sid=e11707d5-02a7-43ef-b45e-2cf4d2036f7d&ts=1773215491"
//Server:cloudflare
//Vary:Origin,X-HTTP-Method-Override,Accept-Encoding
//Via:2.0 heroku-router
//X-Content-Type-Options:nosniff
//X-Powered-By:Express
//x-ratelimit-limit:1000
//x-ratelimit-remaining:999
//x-ratelimit-reset:1773215497
//cf-cache-status:DYNAMIC
//Server-Timing:cfCacheStatus;desc="DYNAMIC",cfEdge;dur=3,cfOrigin;dur=86
//CF-RAY:9da8f9780b9ad5a7-AMS
//Alt-Svc:h3=":443"
//Content-Type:application/json; charset=utf-8
//Content-Length:65
//Expires:-1
//reply:
//{
//  "userId": 1,
//  "title": "foo",
//  "body": "bar",
//  "id": 101
//}
```

##### 完整实例
```csharp
using System.Net;
using SharpDevLib;

//配置，可以全局设置一次，也可以每次传入
HttpConfig.Default.Timeout = TimeSpan.FromSeconds(10);
HttpConfig.Default.RetryCount = 5;
HttpConfig.Default.OnSendProgress = (p) =>
{
    Console.WriteLine($"进度: {p.ProgressString}");
    Console.WriteLine($"速度: {p.Speed}");
};
HttpConfig.Default.UserAgent = null;

var response = await new HttpRequest("https://jsonplaceholder.typicode.com/posts")
                .UseClientId("some id")
                .SetConfig(new HttpConfig { RetryCount = 1 })
                .AddHeader("Authorization", ["Bearer token123"])
                .AddCookie(new Cookie("foo", "bar", "/", "jsonplaceholder.typicode.com"))
                .AddJson("{\"postId\":1}")
                //form-data
                //.AddParameter("postId", "1")
                //multi-part form data
                //.AddFile(new HttpFormFile("file","test.txt","hello,world".Utf8Decode()))
                .PostAsync();
var text = await response.EnsureSuccessStatusCode().ReadAsStringAsync();
Console.WriteLine(text);
//{
//  "postId": 10,
//  "id": 101
//}
```

## 相关文档
- [HTTP GET](http-get.md)
- [HTTP PUT](http-put.md)
- [HTTP DELETE](http-delete.md)
- [网络传输](../README.md#网络传输)
