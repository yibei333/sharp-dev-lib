using EmbedIO;
using EmbedIO.Routing;
using EmbedIO.WebApi;
using SharpDevLib.Tests.TestData;
using System;

namespace SharpDevLib.Tests.Transport.Http.Base;

internal class HttpPutController : WebApiController
{
    [Route(HttpVerbs.Put, "/put")]
    public void Put([JsonData] User user)
    {
        Console.WriteLine(HttpContext.Request.UserAgent);
        Console.WriteLine(user.Serialize());
    }

    [Route(HttpVerbs.Put, "/put/int")]
    public void PutInt([JsonData] User user)
    {
        HttpContext.WriteObject(user.Age);
    }

    [Route(HttpVerbs.Put, "/put/string")]
    public void PutString([JsonData] User user)
    {
        HttpContext.WriteObject(user.Name);
    }

    [Route(HttpVerbs.Put, "/put/object")]
    public void PutObject([JsonData] User user)
    {
        HttpContext.WriteObject(user);
    }
}
