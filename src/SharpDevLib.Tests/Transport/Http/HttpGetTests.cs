using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpDevLib.Tests.Basic.Helpers;
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
    public async Task GetTest()
    {
        var response = await new HttpRequestModel(BaseUrl.CombinePath("/api/get")).GetAsync();
        Console.WriteLine(response.ToString());
        Assert.IsTrue(response.IsSuccess);
    }

    [TestMethod]
    public async Task GetIntTest()
    {
        var actual = await new HttpRequestModel(BaseUrl.CombinePath("/api/get/int"))
            .AddParameter("seed", "1")
            .GetAsync()
            .EnsureSuccessStatusCode()
            .GetResponseDataAsync<int>();
        Assert.AreEqual(2, actual);
    }

    [TestMethod]
    public async Task GetStringTest()
    {
        var response = await new HttpRequestModel(BaseUrl.CombinePath("/api/get/string"))
            .AddParameter("foo", "foo")
            .AddParameter("bar", "bar")
            .GetAsync();
        Assert.IsTrue(response.IsSuccess);
        Assert.AreEqual("foo_bar", await response.GetResponseDataAsync<string>());
        Assert.AreEqual("foo_bar", await response.GetResponseDataAsync<string>());
    }

    [TestMethod]
    public async Task GetUserTest()
    {
        var data = await new HttpRequestModel(BaseUrl.CombinePath("/api/get/user"))
            .GetAsync()
            .EnsureSuccessStatusCode()
            .GetResponseDataAsync<User>();
        Assert.IsNotNull(data);
        Assert.AreEqual("foo", data.Name);
        Assert.AreEqual(10, data.Age);
    }

    [TestMethod]
    public async Task GetUsersTest()
    {
        var data = await new HttpRequestModel(BaseUrl.CombinePath("/api/get/users"))
            .GetAsync()
            .EnsureSuccessStatusCode()
            .GetResponseDataAsync<List<User>>(new JsonOption { NameFormat = JsonNameFormat.KebabCaseUpper });
        Console.WriteLine(data.Serialize(JsonHelperTests.FormatJsonOption));

        Assert.IsNotNull(data);
        Assert.HasCount(2, data);
        Assert.AreEqual("foo", data.First().Name);
        Assert.AreEqual(10, data.First().Age);
        Assert.AreEqual("bar", data.Last().Name);
        Assert.AreEqual(20, data.Last().Age);
    }

    [TestMethod]
    public async Task GetStreamTest()
    {
        var count = 0;
        var config = new HttpConfig
        {
            Timeout = TimeSpan.FromSeconds(2),
            OnReceiveProgress = p =>
            {
                count++;
                Console.WriteLine($"receive->{p}");
            }
        };
        HttpHelper.SetConfig("Default", config);
        using var stream = await new HttpRequestModel(BaseUrl.CombinePath("/statics/TestFile.txt"))
                        .UseClientId("Default")
                        .GetStreamAsync();
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
        var cookies = await new HttpRequestModel(BaseUrl.CombinePath(url))
            .AddCookie(new System.Net.Cookie("foo", "bar", "/", "localhost"))
            .GetAsync()
            .EnsureSuccessStatusCode()
            .GetResponseDataAsync<string>();
        Console.WriteLine(cookies);
        Assert.Contains("bar", cookies);
    }

    [TestMethod]
    public async Task SetCookieTestAsync()
    {
        await new HttpRequestModel(BaseUrl.CombinePath("/api/get/cookie/set")).GetAsync().EnsureSuccessStatusCode();

        var cookies = await new HttpRequestModel(BaseUrl.CombinePath("/api/get/cookie"))
            .AddCookie(new System.Net.Cookie("foo", "bar", "/", "localhost"))
            .GetAsync()
            .EnsureSuccessStatusCode()
            .GetResponseDataAsync<string>();
        Assert.Contains("bar", cookies);
        Assert.Contains("baz", cookies);
    }

    [TestMethod]
    public async Task ErrorTest()
    {
        var response = await new HttpRequestModel(BaseUrl.CombinePath("/api/get/error")).GetAsync();
        Assert.IsFalse(response.IsSuccess);
        Console.WriteLine(response.ToString());
    }

    [TestMethod]
    public async Task RetryTest()
    {
        var count = 5;
        HttpHelper.SetConfig("RetryTestDefault", new HttpConfig { Timeout = TimeSpan.FromSeconds(2), RetryCount = count });
        var url = BaseUrl.CombinePath("/api/get/retry");
        var response = await new HttpRequestModel(url)
            .UseClientId("RetryTestDefault")
            .AddParameter("count", count.ToString())
            .AddParameter("id", Guid.NewGuid().ToString())
            .GetAsync();
        Assert.IsTrue(response.IsSuccess);

        response = await new HttpRequestModel(url)
            .AddParameter("count", "6")
            .AddParameter("id", Guid.NewGuid().ToString())
            .GetAsync();
        Assert.IsFalse(response.IsSuccess);

        HttpHelper.SetConfig("RetryTestClient1", new HttpConfig { RetryCount = 3 });
        response = await new HttpRequestModel(url)
            .UseClientId("RetryTestClient1")
            .AddParameter("count", "6")
            .AddParameter("id", Guid.NewGuid().ToString())
            .GetAsync();
        Assert.IsFalse(response.IsSuccess);

        HttpHelper.SetConfig("RetryTestClient2", new HttpConfig { RetryCount = 10 });
        response = await new HttpRequestModel(url)
            .UseClientId("RetryTestClient2")
            .AddParameter("count", "6")
            .AddParameter("id", Guid.NewGuid().ToString())
            .GetAsync();
        Assert.IsTrue(response.IsSuccess);
    }

    [TestMethod]
    public async Task TimeoutTest()
    {
        HttpHelper.SetConfig("TimeoutTestDefault", new HttpConfig { Timeout = TimeSpan.FromSeconds(2) });
        var url = BaseUrl.CombinePath("/api/get/timeout");
        var response = await new HttpRequestModel(url)
            .UseClientId("Default")
            .GetAsync();
        Assert.IsTrue(response.IsSuccess);

        HttpHelper.SetConfig("TimeoutTestClient1", new HttpConfig { Timeout = TimeSpan.FromSeconds(1) });
        response = await new HttpRequestModel(url)
            .UseClientId("TimeoutTestClient1")
            .GetAsync();
        Assert.IsFalse(response.IsSuccess);

        HttpHelper.SetConfig("TimeoutTestClient2", new HttpConfig { Timeout = TimeSpan.FromSeconds(2) });
        response = await new HttpRequestModel(url)
            .UseClientId("TimeoutTestClient2")
            .GetAsync();
        Assert.IsTrue(response.IsSuccess);

        HttpHelper.SetConfig("TimeoutTestClient3", new HttpConfig { Timeout = TimeSpan.FromSeconds(1) });
        response = await new HttpRequestModel(url)
            .UseClientId("TimeoutTestClient3")
            .GetAsync();
        Assert.IsFalse(response.IsSuccess);
    }
}
