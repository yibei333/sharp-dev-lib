using EmbedIO.Routing;
using EmbedIO;
using EmbedIO.WebApi;
using System.Collections.Generic;
using System.Linq;
using SharpDevLib.Tests.Data;
using System;

namespace SharpDevLib.Tests.Standard.Http.Base;

internal class ApiController : WebApiController
{
    [Route(HttpVerbs.Get, "/get")]
    public void Get()
    {
        Console.WriteLine("ok");
    }

    [Route(HttpVerbs.Get, "/get/int")]
    public void GetInt([QueryField] int seed)
    {
        HttpContext.WriteObject(seed + 1);
    }

    [Route(HttpVerbs.Get, "/get/string")]
    public void GetString([QueryField] string foo, [QueryField] string bar)
    {
        HttpContext.WriteObject($"{foo}_{bar}");
    }

    [Route(HttpVerbs.Get, "/get/user")]
    public void GetUser()
    {
        HttpContext.WriteObject(new User("foo", 10));
    }

    [Route(HttpVerbs.Get, "/get/users")]
    public void GetUsers()
    {
        var data = new List<User>
        {
            new("foo",10),
            new("bar",20),
        };
        HttpContext.WriteObject(data);
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
