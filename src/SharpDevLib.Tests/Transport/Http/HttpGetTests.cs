using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpDevLib.Tests.TestData;
using SharpDevLib.Tests.Transport.Http.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SharpDevLib.Tests.Transport.Http;

[TestClass]
public class HttpGetTests : HttpBaseTests
{
    [TestMethod]
    public void GetTest()
    {
        var response = new HttpRequest(BaseUrl.CombinePath("/api/get"))
            .GetAsync()
            .GetAwaiter()
            .GetResult();
        Console.WriteLine(response.ToString());
        Assert.IsTrue(response.IsSuccess);
    }

    [TestMethod]
    public void GetIntTest()
    {
        var response = new HttpRequest(BaseUrl.CombinePath("/api/get/int"))
            .AddParameter("seed", "1")
            .GetAsync()
            .GetAwaiter()
            .GetResult();
        Console.WriteLine(response);
        Assert.IsTrue(response.IsSuccess);
        var actual = response.GetResponseDataAsync<int>().GetAwaiter().GetResult();
        Assert.AreEqual(2, actual);
    }

    [TestMethod]
    public void GetStringTest()
    {
        var response = new HttpRequest(BaseUrl.CombinePath("/api/get/string"))
            .AddParameter("foo", "foo")
            .AddParameter("bar", "bar")
            .GetAsync()
            .GetAwaiter()
            .GetResult();
        Assert.IsTrue(response.IsSuccess);
        Assert.AreEqual("foo_bar", response.GetResponseDataAsync<string>().GetAwaiter().GetResult());
        Assert.AreEqual("foo_bar", response.GetResponseDataAsync<string>().GetAwaiter().GetResult());
    }

    [TestMethod]
    public void GetUserTest()
    {
        var request = new HttpRequest(BaseUrl.CombinePath("/api/get/user"));
        var response = request.GetAsync().GetAwaiter().GetResult();
        Assert.IsTrue(response.IsSuccess);
        var data = response.GetResponseDataAsync<User>().GetAwaiter().GetResult();
        Assert.IsNotNull(data);
        Assert.AreEqual("foo", data.Name);
        Assert.AreEqual(10, data.Age);
    }

    [TestMethod]
    public void GetUsersTest()
    {
        var response = new HttpRequest(BaseUrl.CombinePath("/api/get/users"))
            .GetAsync()
            .GetAwaiter()
            .GetResult();
        Assert.IsTrue(response.IsSuccess);
        var data = response.GetResponseDataAsync<List<User>>().GetAwaiter().GetResult();
        Assert.IsNotNull(data);
        Assert.HasCount(2, data);
        Assert.AreEqual("foo", data.First().Name);
        Assert.AreEqual(10, data.First().Age);
        Assert.AreEqual("bar", data.Last().Name);
        Assert.AreEqual(20, data.Last().Age);
        Console.WriteLine(response.ToString());
    }

    [TestMethod]
    public void GetStreamTest()
    {
        var count = 0;
        var config = new HttpConfig
        {
            OnReceiveProgress = p =>
            {
                count++;
                Console.WriteLine($"receive->{p}");
            }
        };
        using var stream = new HttpRequest(BaseUrl.CombinePath("/statics/TestFile.txt"))
            .SetConfig(config)
            .GetStreamAsync()
            .GetAwaiter()
            .GetResult();
        using var memoryStream = new MemoryStream();
        stream.CopyTo(memoryStream);
        var actual = memoryStream.ToArray().Utf8Encode();
        Assert.AreEqual("Hello,World!", actual);
        Assert.IsGreaterThan(0, count);
    }

    [TestMethod]
    public async Task GetCookieTest()
    {
        var url = "/api/get/cookie";
        var response = new HttpRequest(BaseUrl.CombinePath(url))
            .AddCookie(new System.Net.Cookie("foo", "bar", "/", "localhost"))
            .GetAsync()
            .GetAwaiter()
            .GetResult();
        Assert.IsTrue(response.IsSuccess);
        var cookies = await response.GetResponseDataAsync<string>();
        Console.WriteLine(cookies);
        Assert.Contains("bar", cookies);
    }

    [TestMethod]
    public async Task SetCookieTestAsync()
    {
        var url = "/api/get/cookie/set";
        var response = await new HttpRequest(BaseUrl.CombinePath(url)).GetAsync();
        Console.WriteLine(response);
        Assert.IsTrue(response.IsSuccess);

        url = "/api/get/cookie";
        var response1 = await new HttpRequest(BaseUrl.CombinePath(url)).AddCookie(new System.Net.Cookie("foo", "bar", "/", "localhost")).GetAsync();
        Assert.IsTrue(response1.IsSuccess);
        Console.WriteLine(response1);
        var cookies1 = await response1.GetResponseDataAsync<string>();
        Assert.Contains("bar", cookies1);
        Assert.Contains("baz", cookies1);
    }

    [TestMethod]
    public void ErrorTest()
    {
        var url = "/api/get/error";
        var response = new HttpRequest(BaseUrl.CombinePath(url))
            .GetAsync()
            .GetAwaiter()
            .GetResult();
        Assert.IsFalse(response.IsSuccess);
        Console.WriteLine(response.ToString());
    }

    [TestMethod]
    public void RetryTest()
    {
        var count = 5;
        HttpConfig.Default.RetryCount = count;
        var url = BaseUrl.CombinePath("/api/get/retry");
        var response = new HttpRequest(url)
            .AddParameter("count", count.ToString())
            .AddParameter("id", Guid.NewGuid().ToString())
            .GetAsync()
            .GetAwaiter()
            .GetResult();
        Assert.IsTrue(response.IsSuccess);

        response = new HttpRequest(url)
            .AddParameter("count", "6")
            .AddParameter("id", Guid.NewGuid().ToString())
            .GetAsync()
            .GetAwaiter()
            .GetResult();
        Assert.IsFalse(response.IsSuccess);

        response = new HttpRequest(url)
            .SetConfig(new HttpConfig { RetryCount = 3 })
            .AddParameter("count", "6")
            .AddParameter("id", Guid.NewGuid().ToString())
            .GetAsync()
            .GetAwaiter()
            .GetResult();
        Assert.IsFalse(response.IsSuccess);

        response = new HttpRequest(url)
            .SetConfig(new HttpConfig { RetryCount = 10 })
            .AddParameter("count", "6")
            .AddParameter("id", Guid.NewGuid().ToString())
            .GetAsync()
            .GetAwaiter()
            .GetResult();
        Assert.IsTrue(response.IsSuccess);
    }

    [TestMethod]
    public void TimeoutTest()
    {
        HttpConfig.Default.Timeout = TimeSpan.FromSeconds(2);
        var url = BaseUrl.CombinePath("/api/get/timeout");
        var response = new HttpRequest(url)
            .SetConfig(new HttpConfig { RetryCount = 0 })
            .GetAsync()
            .GetAwaiter()
            .GetResult();
        Assert.IsTrue(response.IsSuccess);

        HttpConfig.Default.Timeout = TimeSpan.FromSeconds(1);
        response = new HttpRequest(url)
            .SetConfig(new HttpConfig { RetryCount = 0 })
            .GetAsync()
            .GetAwaiter()
            .GetResult();
        Assert.IsFalse(response.IsSuccess);

        response = new HttpRequest(url)
            .SetConfig(new HttpConfig { Timeout = TimeSpan.FromSeconds(2), RetryCount = 0 })
            .GetAsync()
            .GetAwaiter()
            .GetResult();
        Assert.IsTrue(response.IsSuccess);

        response = new HttpRequest(url)
            .SetConfig(new HttpConfig { Timeout = TimeSpan.FromSeconds(1), RetryCount = 0 })
            .GetAsync()
            .GetAwaiter()
            .GetResult();
        Assert.IsFalse(response.IsSuccess);
    }
}
