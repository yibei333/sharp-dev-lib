# HTTP GET

提供`HTTP` GET 请求功能。

##### 简单实例

```csharp
using SharpDevLib;
using System.Net;

//简单示例
var request = new HttpRequest("https://jsonplaceholder.typicode.com/posts/1");
var response = await request.GetAsync();
Console.WriteLine(response.ToString());
//****request****
//url:https://jsonplaceholder.typicode.com/posts/1
//method:GET
//headers:
//User-Agent:Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/124.0.0.0 Safari/537.36 Edg/124.0.0.0
//****response****
//code:OK
//reply:
//{
//  "userId": 1,
//  "id": 1,
//  "title": "sunt aut facere repellat provident occaecati excepturi optio reprehenderit",
//  "body": "quia et suscipit\nsuscipit recusandae consequuntur expedita et cum\nreprehenderit molestiae ut ut quas totam\nnostrum rerum est autem sunt rem eveniet architecto"
//}
```

##### 完整实例

```csharp
using SharpDevLib;
using System.Net;

//配置，可以全局设置一次，也可以每次传入
//HttpConfig.Default=new HttpConfig();
HttpConfig.Default.Timeout=TimeSpan.FromSeconds(10);
HttpConfig.Default.RetryCount=5;
HttpConfig.Default.OnReceiveProgress=(p)=>
{
    Console.WriteLine($"进度: {p.ProgressString}");
    Console.WriteLine($"速度: {p.Speed}");
};
HttpConfig.Default.UserAgent=null;

var request = new HttpRequest("https://jsonplaceholder.typicode.com/comments")
    .AddParameter("postId","1")
    .AddParameter("someParameter","test")
    .AddHeader("Authorization",["Bearer token123"])
    .AddCookie(new Cookie("foo","bar", "/", "api.example.com"));
var response = await request.GetAsync();
var result=await response.EnsureSuccessStatusCode().GetResponseDataAsync<string>();
Console.WriteLine(result);
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
```

## 相关文档
- [HTTP POST](http-post.md)
- [HTTP PUT](http-put.md)
- [HTTP DELETE](http-delete.md)
- [网络传输](../README.md#网络传输)
