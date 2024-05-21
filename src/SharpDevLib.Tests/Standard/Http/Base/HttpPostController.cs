using EmbedIO;
using EmbedIO.Routing;
using EmbedIO.WebApi;
using HttpMultipartParser;
using SharpCompress;
using SharpDevLib.Standard;
using SharpDevLib.Tests.Data;
using System;
using System.Threading.Tasks;

namespace SharpDevLib.Tests.Standard.Http.Base;

internal class HttpPostController : WebApiController
{
    [Route(HttpVerbs.Post, "/post")]
    public void Post([JsonData] User user)
    {
        Console.WriteLine(user.Serialize());
    }

    [Route(HttpVerbs.Post, "/post/int")]
    public void PostInt([JsonData] User user)
    {
        HttpContext.WriteObject(user.Age);
    }

    [Route(HttpVerbs.Post, "/post/string")]
    public void PostString([JsonData] User user)
    {
        HttpContext.WriteObject(user.Name);
    }

    [Route(HttpVerbs.Post, "/post/object")]
    public void PostObject([JsonData] User user)
    {
        HttpContext.WriteObject(user);
    }

    [Route(HttpVerbs.Post, "/post/form")]
    public void PostFormAsync([FormField] string name, [FormField] int age)
    {
        Console.WriteLine(name);
        Console.WriteLine(age);
    }

    [Route(HttpVerbs.Post, "/post/form/object")]
    public void PostFormObject([FormField] string name, [FormField] int age)
    {
        HttpContext.WriteObject(new User(name, age));
    }

    [Route(HttpVerbs.Post, "/post/form/multi")]
    public async Task PostMultiFormAsync()
    {
        var parser = await MultipartFormDataParser.ParseAsync(Request.InputStream);
        parser.Parameters.ForEach(x =>
        {
            Console.WriteLine($"{x.Name}->{x.Data}");
        });
        parser.Files.ForEach(x =>
        {
            Console.WriteLine($"name->{x.Name},filename->{x.FileName},length->{x.Data.Length}");
        });
    }

    [Route(HttpVerbs.Post, "/post/form/multi/object")]
    public async Task PostMultiFormObjectAsync()
    {
        var parser = await MultipartFormDataParser.ParseAsync(Request.InputStream);
        parser.Files.ForEach(x =>
        {
            var path = AppDomain.CurrentDomain.BaseDirectory.CombinePath($"Data/Tests/Post{x.FileName}");
            x.Data.SaveToFile(path);
        });

        var user = new User(string.Empty, 0);
        parser.Parameters.ForEach(x =>
        {
            if (x.Name.Equals("name", StringComparison.OrdinalIgnoreCase)) user.Name = x.Data;
            else if (x.Name.Equals("age", StringComparison.OrdinalIgnoreCase)) user.Age = int.TryParse(x.Data, out var age) ? age : 0;
        });
        HttpContext.WriteObject(user);
    }
}
