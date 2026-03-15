# HTTP DELETE

提供`HTTP` DELETE 请求功能。

##### 简单示例

``` csharp
using SharpDevLib;

var response = await new HttpRequestModel("https://jsonplaceholder.typicode.com/posts/1")
                .DeleteAsync();
Console.WriteLine(response);
//****request****
//url:https://jsonplaceholder.typicode.com/posts/1
//method:DELETE
//headers:
//User-Agent:Mozilla/5.0,(Windows NT 10.0; Win64; x64),AppleWebKit/537.36,(KHTML, like Gecko),Chrome/124.0.0.0,Safari/537.36,Edg/124.0.0.0
//Transfer-Encoding:chunked
//****response****
//code:OK
//headers:
//Date:Wed, 11 Mar 2026 08:05:17 GMT
//Connection:keep-alive
//Access-Control-Allow-Credentials:true
//Cache-Control:no-cache
//ETag:W/"2-vyGp6PvFo4RvsFtPoIWeCReyIC8"
//nel:{"report_to":"heroku-nel","response_headers":["Via"],"max_age":3600,"success_fraction":0.01,"failure_fraction":0.1}
//Pragma:no-cache
//report-to:{"group":"heroku-nel","endpoints":[{"url":"https://nel.heroku.com/reports?s=ExTPbrdSqJmyYAAXdEEgST2uBKcrGMpcySKcPou9qOI%3D\u0026sid=e11707d5-02a7-43ef-b45e-2cf4d2036f7d\u0026ts=1773216317"}],"max_age":3600}
//reporting-endpoints:heroku-nel="https://nel.heroku.com/reports?s=ExTPbrdSqJmyYAAXdEEgST2uBKcrGMpcySKcPou9qOI%3D&sid=e11707d5-02a7-43ef-b45e-2cf4d2036f7d&ts=1773216317"
//Server:cloudflare
//Vary:Origin,Accept-Encoding
//Via:2.0 heroku-router
//X-Content-Type-Options:nosniff
//X-Powered-By:Express
//x-ratelimit-limit:1000
//x-ratelimit-remaining:999
//x-ratelimit-reset:1773216337
//cf-cache-status:DYNAMIC
//Server-Timing:cfCacheStatus;desc="DYNAMIC",cfEdge;dur=5,cfOrigin;dur=256
//CF-RAY:9da90da1dfddfc46-AMS
//Alt-Svc:h3=":443"
//Content-Type:application/json; charset=utf-8
//Content-Length:2
//Expires:-1
//reply:
//{}
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

var response = await new HttpRequestModel("https://jsonplaceholder.typicode.com/posts/1")
                .UseClientId("some id")
                .AddHeader("Authorization", ["Bearer token123"])
                .AddCookie(new Cookie("foo", "bar", "/", "jsonplaceholder.typicode.com"))
                .AddParameter("id","1")
                .DeleteAsync();
Console.WriteLine(response);
//****request****
//url:https://jsonplaceholder.typicode.com/posts/1?id=1
//method:DELETE
//headers:
//Authorization:Bearer token123
//Transfer-Encoding:chunked
//Cookie:foo=bar;
//****response****
//code:OK
//headers:
//Date:Wed, 11 Mar 2026 08:03:57 GMT
//Connection:keep-alive
//Access-Control-Allow-Credentials:true
//Cache-Control:no-cache
//ETag:W/"2-vyGp6PvFo4RvsFtPoIWeCReyIC8"
//nel:{"report_to":"heroku-nel","response_headers":["Via"],"max_age":3600,"success_fraction":0.01,"failure_fraction":0.1}
//Pragma:no-cache
//report-to:{"group":"heroku-nel","endpoints":[{"url":"https://nel.heroku.com/reports?s=mSMoyufS7RPZ4dUw71PdYrTYCcuvNIxrCjGZ8ibQzJs%3D\u0026sid=e11707d5-02a7-43ef-b45e-2cf4d2036f7d\u0026ts=1773216237"}],"max_age":3600}
//reporting-endpoints:heroku-nel="https://nel.heroku.com/reports?s=mSMoyufS7RPZ4dUw71PdYrTYCcuvNIxrCjGZ8ibQzJs%3D&sid=e11707d5-02a7-43ef-b45e-2cf4d2036f7d&ts=1773216237"
//Server:cloudflare
//Vary:Origin,Accept-Encoding
//Via:2.0 heroku-router
//X-Content-Type-Options:nosniff
//X-Powered-By:Express
//x-ratelimit-limit:1000
//x-ratelimit-remaining:999
//x-ratelimit-reset:1773216277
//cf-cache-status:DYNAMIC
//Server-Timing:cfCacheStatus;desc="DYNAMIC",cfEdge;dur=5,cfOrigin;dur=85
//CF-RAY:9da90babd9991c98-AMS
//Alt-Svc:h3=":443"
//Content-Type:application/json; charset=utf-8
//Content-Length:2
//Expires:-1
//reply:
//{}
```

## 相关文档
- [HTTP GET](http-get.md)
- [HTTP POST](http-post.md)
- [HTTP PUT](http-put.md)
- [网络传输](../README.md#网络传输)
