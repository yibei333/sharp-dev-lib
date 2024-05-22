using EmbedIO;
using EmbedIO.Routing;
using EmbedIO.WebApi;
using SharpDevLib.Standard;
using SharpDevLib.Tests.Data;
using System;

namespace SharpDevLib.Tests.Standard.Http.Base;

internal class HttpDeleteController : WebApiController
{
    [Route(HttpVerbs.Delete, "/delete")]
    public void Delete([QueryField] string name, [QueryField] int age)
    {
        var user = new User(name, age);
        Console.WriteLine(user.Serialize());
    }

    [Route(HttpVerbs.Delete, "/delete/int")]
    public void DeleteInt([QueryField] string name, [QueryField] int age)
    {
        var user = new User(name, age);
        HttpContext.WriteObject(user.Age);
    }

    [Route(HttpVerbs.Delete, "/delete/string")]
    public void DeleteString([QueryField] string name, [QueryField] int age)
    {
        var user = new User(name, age);
        HttpContext.WriteObject(user.Name);
    }

    [Route(HttpVerbs.Delete, "/delete/object")]
    public void DeleteObject([QueryField] string name, [QueryField] int age)
    {
        var user = new User(name, age);
        HttpContext.WriteObject(user);
    }
}
