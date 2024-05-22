using EmbedIO;
using EmbedIO.Routing;
using EmbedIO.WebApi;
using SharpDevLib.Tests.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace SharpDevLib.Tests.Standard.Http.Base;

internal class HttpGetController : WebApiController
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

    static readonly Dictionary<string, int> _retryCount = new();

    [Route(HttpVerbs.Get, "/get/retry")]
    public void GetRetry([QueryField] int count, [QueryField] string id)
    {
        if (_retryCount.ContainsKey(id))
        {
            if (_retryCount[id] != count)
            {
                _retryCount[id] = _retryCount[id] + 1;
                throw new Exception("retry please");
            }
        }
        else
        {
            _retryCount[id] = 1;
            throw new Exception("retry please");
        }
    }

    [Route(HttpVerbs.Get, "/get/timeout")]
    public void Timeout()
    {
        Thread.Sleep(1500);
    }
}
