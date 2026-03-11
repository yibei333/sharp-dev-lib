# HTTP GET

提供`HTTP` GET 请求功能。

##### 简单实例

```csharp
//简单示例
using SharpDevLib;

var response = await new HttpRequest("https://jsonplaceholder.typicode.com/posts/1").GetAsync();
Console.WriteLine(response.ToString());
//****request****
//url:https://jsonplaceholder.typicode.com/posts/1
//method:GET
//headers:
//User-Agent:Mozilla/5.0,(Windows NT 10.0; Win64; x64),AppleWebKit/537.36,(KHTML, like Gecko),Chrome/124.0.0.0,Safari/537.36,Edg/124.0.0.0
//Transfer-Encoding:chunked
//****response****
//code:OK
//headers:
//Date:Wed, 11 Mar 2026 06:52:55 GMT
//Connection:keep-alive
//Access-Control-Allow-Credentials:true
//Cache-Control:max-age=43200
//ETag:W/"124-yiKdLzqO5gfBrJFrcdJ8Yq0LGnU"
//nel:{"report_to":"heroku-nel","response_headers":["Via"],"max_age":3600,"success_fraction":0.01,"failure_fraction":0.1}
//Pragma:no-cache
//report-to:{"group":"heroku-nel","endpoints":[{"url":"https://nel.heroku.com/reports?s=VjJpH3PAuNrz6amXKRk4LtjkHFwIK0TVjVOukNsYOpE%3D\u0026sid=e11707d5-02a7-43ef-b45e-2cf4d2036f7d\u0026ts=1753315135"}],"max_age":3600}
//reporting-endpoints:heroku-nel="https://nel.heroku.com/reports?s=VjJpH3PAuNrz6amXKRk4LtjkHFwIK0TVjVOukNsYOpE%3D&sid=e11707d5-02a7-43ef-b45e-2cf4d2036f7d&ts=1753315135"
//Server:cloudflare
//Vary:Origin,Accept-Encoding
//Via:2.0 heroku-router
//X-Content-Type-Options:nosniff
//X-Powered-By:Express
//x-ratelimit-limit:1000
//x-ratelimit-remaining:999
//x-ratelimit-reset:1753315149
//Age:320
//Accept-Ranges:bytes
//cf-cache-status:HIT
//Server-Timing:cfCacheStatus;desc="HIT",cfEdge;dur=5,cfOrigin;dur=0
//CF-RAY:9da8a39d1a6296f2-AMS
//Alt-Svc:h3=":443"
//Content-Type:application/json; charset=utf-8
//Content-Length:292
//Expires:-1
//reply:
//{
//  "userId": 1,
//  "id": 1,
//  "title": "sunt aut facere repellat provident occaecati excepturi optio reprehenderit",
//  "body": "quia et suscipit
//suscipit recusandae consequuntur expedita et cum
//reprehenderit molestiae ut ut quas totam
//nostrum rerum est autem sunt rem eveniet architecto"
//}
```

##### 完整实例

```csharp
using SharpDevLib;
using System.Net;

//配置，可以全局设置一次，也可以每次传入
HttpConfig.Default.Timeout = TimeSpan.FromSeconds(10);
HttpConfig.Default.RetryCount = 5;
//仅当响应头有Content-Length并且调用ReadAsStreamAsync才会起作用
HttpConfig.Default.OnReceiveProgress = (p) =>
{
    Console.WriteLine($"进度: {p.ProgressString}");
    Console.WriteLine($"速度: {p.Speed}");
};
HttpConfig.Default.UserAgent = null;

var response = await new HttpRequest("https://jsonplaceholder.typicode.com/comments")
                .UseClientId("some id")
                .SetConfig(new HttpConfig { RetryCount = 1 })
                .AddHeader("Authorization", ["Bearer token123"])
                .AddCookie(new Cookie("foo", "bar", "/", "jsonplaceholder.typicode.com"))
                .AddParameter("postId", "1")
                .AddParameter("someParameter", "test")
                .GetAsync();
var text = await response.EnsureSuccessStatusCode().ReadAsStringAsync();
Console.WriteLine(text);
//[
//  {
//    "postId": 1,
//    "id": 1,
//    "name": "id labore ex et quam laborum",
//    "email": "Eliseo@gardner.biz",
//    "body": "laudantium enim quasi est quidem magnam voluptate ipsam eos\ntempora quo necessitatibus\ndolor quam autem quasi\nreiciendis et nam sapiente accusantium"
//  },
//  {
//    "postId": 1,
//    "id": 2,
//    "name": "quo vero reiciendis velit similique earum",
//    "email": "Jayne_Kuhic@sydney.com",
//    "body": "est natus enim nihil est dolore omnis voluptatem numquam\net omnis occaecati quod ullam at\nvoluptatem error expedita pariatur\nnihil sint nostrum voluptatem reiciendis et"
//  },
//  {
//    "postId": 1,
//    "id": 3,
//    "name": "odio adipisci rerum aut animi",
//    "email": "Nikita@garfield.biz",
//    "body": "quia molestiae reprehenderit quasi aspernatur\naut expedita occaecati aliquam eveniet laudantium\nomnis quibusdam delectus saepe quia accusamus maiores nam est\ncum et ducimus et vero voluptates excepturi deleniti ratione"
//  },
//  {
//    "postId": 1,
//    "id": 4,
//    "name": "alias odio sit",
//    "email": "Lew@alysha.tv",
//    "body": "non et atque\noccaecati deserunt quas accusantium unde odit nobis qui voluptatem\nquia voluptas consequuntur itaque dolor\net qui rerum deleniti ut occaecati"
//  },
//  {
//    "postId": 1,
//    "id": 5,
//    "name": "vero eaque aliquid doloribus et culpa",
//    "email": "Hayden@althea.biz",
//    "body": "harum non quasi et ratione\ntempore iure ex voluptates in ratione\nharum architecto fugit inventore cupiditate\nvoluptates magni quo et"
//  }
//]
var posts = await response.EnsureSuccessStatusCode().GetResponseDataAsync<List<PostInfo>>();
Console.WriteLine(posts.Count);
//5

//下载并保存到文件
using var stream=await response.ReadAsStreamAsync();
using var fileStream=new FileStream("1.txt",FileMode.OpenOrCreate,FileAccess.ReadWrite);
await stream.CopyToAsync(fileStream);
await fileStream.FlushAsync();

class PostInfo
{
    public int PostId{get;set;}
    public int Id{get;set;}
    public string? Name{get;set;}
    public string? Email{get;set;}
    public string? Body{get;set;}
}
```

## 相关文档
- [HTTP POST](http-post.md)
- [HTTP PUT](http-put.md)
- [HTTP DELETE](http-delete.md)
- [网络传输](../README.md#网络传输)
