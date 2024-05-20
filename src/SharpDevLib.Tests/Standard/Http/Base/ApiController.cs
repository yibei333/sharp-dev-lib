using EmbedIO.Routing;
using EmbedIO;
using EmbedIO.WebApi;
using System.Collections.Generic;
using System.Linq;
using SharpDevLib.Tests.Data;
using System;
using System.Text;

namespace SharpDevLib.Tests.Standard.Http.Base;

internal class ApiController : WebApiController
{
    [Route(HttpVerbs.Get, "/get")]
    public void Get()
    {
        Console.WriteLine("ok");
    }

    [Route(HttpVerbs.Get, "/get/int")]
    public int GetInt([QueryField] int seed)
    {
        Response.ContentLength64 = 1;
        return seed + 1;
    }

    [Route(HttpVerbs.Get, "/get/string")]
    public void GetString([QueryField] string foo, [QueryField] string bar)
    {
        Response.ContentType = MimeType.PlainText;
        using (var writer = HttpContext.OpenResponseText(Encoding.UTF8, true))
        {
            writer.Write($"{foo}_{bar}");
        }
    }

    [Route(HttpVerbs.Get, "/get/user")]
    public User GetUser()
    {
        return new User("foo", 10);
    }

    [Route(HttpVerbs.Get, "/get/users")]
    public List<User> GetUsers()
    {
        return new List<User>
        {
            new("foo",10),
            new("bar",20),
        };
    }

    [Route(HttpVerbs.Get, "/get/cookie")]
    public void GetCookie()
    {
        var cookies = HttpContext.Request.Cookies.Select(x => new KeyValuePair<string, string>(x.Name, x.Value)).ToList();
        cookies.ForEach(x =>
        {
            Response.Headers.Add("Set-Cookie", $"{x.Key}={x.Value}");
        });
    }
}
