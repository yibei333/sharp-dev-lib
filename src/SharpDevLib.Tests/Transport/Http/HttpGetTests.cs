using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpDevLib.Tests.TestData;
using SharpDevLib.Tests.Transport.Http.Base;
using SharpDevLib.Transport;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SharpDevLib.Tests.Transport.Http;

[TestClass]
public class HttpGetTests : HttpBaseTests
{
    [TestMethod]
    public void GetTest()
    {
        var request = new HttpRequest(BaseUrl.CombinePath("/api/get"));
        var response = request.GetAsync().GetAwaiter().GetResult();
        Assert.IsTrue(response.IsSuccess);
    }

    [TestMethod]
    public void GetIntTest()
    {
        var count = 0;
        var request = new HttpRequest(BaseUrl.CombinePath("/api/get/int"))
        {
            Parameters = new Dictionary<string, string?> { { "seed", "1" } },
            Config = new HttpConfig
            {
                OnReceiveProgress = p =>
                {
                    count++;
                    Console.WriteLine($"receive->{p}");
                }
            }
        };
        var response = request.GetAsync().GetAwaiter().GetResult();
        Assert.IsGreaterThan(0, count);
        Assert.IsTrue(response.IsSuccess);
        var actual = response.GetResponseDataAsync<int>().GetAwaiter().GetResult();
        Assert.AreEqual(2, actual);
        Console.WriteLine(request);
    }

    [TestMethod]
    public void GetStringTest()
    {
        var request = new HttpRequest(BaseUrl.CombinePath("/api/get/string"))
        {
            Parameters = new Dictionary<string, string?> { { "foo", "foo" }, { "bar", "bar" } }
        };
        var response = request.GetAsync().GetAwaiter().GetResult();
        Assert.IsTrue(response.IsSuccess);
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
        var request = new HttpRequest(BaseUrl.CombinePath("/api/get/users"));
        var response = request.GetAsync().GetAwaiter().GetResult();
        Assert.IsTrue(response.IsSuccess);
        var data = response.GetResponseDataAsync<List<User>>().GetAwaiter().GetResult();
        Assert.IsNotNull(data);
        Assert.HasCount(2, data);
        Assert.AreEqual("foo", data.First().Name);
        Assert.AreEqual(10, data.First().Age);
        Assert.AreEqual("bar", data.Last().Name);
        Assert.AreEqual(20, data.Last().Age);
    }

    [TestMethod]
    public void GetStreamTest()
    {
        var count = 0;
        var request = new HttpRequest(BaseUrl.CombinePath("/statics/TestFile.txt"))
        {
            Config = new HttpConfig
            {
                OnReceiveProgress = p =>
                {
                    count++;
                    Console.WriteLine($"receive->{p}");
                }
            }
        };
        var response = request.GetStreamAsync().GetAwaiter().GetResult();
        using var memoryStream = new MemoryStream();
        response.CopyTo(memoryStream);
        var actual = memoryStream.ToArray().Utf8Encode();
        Assert.AreEqual("Hello,World!", actual);
        Assert.IsGreaterThan(0, count);
    }

    static readonly string[] value = ["BIDUPSID=601145944A9976FC12AF00B3136B48F0; PSTM=1715422096; BAIDUID=59DA881A28C93BDBA3A461A5358B8861:FG=1; delPer=0; PSINO=7; ZFY=m:Bnz:BEloKMh6w68:AuIR5F:Bb:BXP9xty8LFvFaveyp9H4:C; BAIDUID_BFESS=59DA881A28C93BDBA3A461A5358B8861:FG=1; H_WISE_SIDS=60175_60269_60274_60289_60299; H_PS_PSSID=60269_60274_60289_60299; BA_HECTOR=80aga10g04050ga5ak8lak85dtl3dc1j4lbe61v; BDRCVFR[S4-dAuiWMmn]=I67x6TjHwwYf0; BDORZ=B490B5EBF6F3CD402E515D22BCDA1598; RT=\"z=1&dm=baidu.com&si=7b9c824a-f453-4b8f-af11-4d5eae458720&ss=lw8my2mv&sl=0&tt=0&bcn=https%3A%2F%2Ffclog.baidu.com%2Flog%2Fweirwood%3Ftype%3Dperf\""];

    [TestMethod]
    public void CookieTest()
    {
        var url = "/api/get/cookie";
        var request = new HttpRequest(BaseUrl.CombinePath(url))
        {
            Headers = new Dictionary<string, string[]>
            {
                { "Cookie", value }
            }
        };
        var response = request.GetAsync().GetAwaiter().GetResult();
        Assert.IsTrue(response.IsSuccess);
        var cookies = response.GetResponseCookies();
        Assert.AreEqual(1, cookies?.Count(x => x?.Name == "BIDUPSID"));
        Assert.AreEqual("601145944A9976FC12AF00B3136B48F0", cookies?.FirstOrDefault(x => x?.Name == "BIDUPSID")?.Value);
    }

    [TestMethod]
    public void RetryTest()
    {
        var count = 5;
        HttpConfig.Default.RetryCount = count;
        var url = BaseUrl.CombinePath("/api/get/retry");
        var request = new HttpRequest(url)
        {
            Parameters = new Dictionary<string, string?> { { "count", count.ToString() }, { "id", Guid.NewGuid().ToString() } }
        };
        var response = request.GetAsync().GetAwaiter().GetResult();
        Assert.IsTrue(response.IsSuccess);

        request = new HttpRequest(url)
        {
            Parameters = new Dictionary<string, string?> { { "count", "6" }, { "id", Guid.NewGuid().ToString() } }
        };
        response = request.GetAsync().GetAwaiter().GetResult();
        Assert.IsFalse(response.IsSuccess);

        request = new HttpRequest(url)
        {
            Parameters = new Dictionary<string, string?> { { "count", "6" }, { "id", Guid.NewGuid().ToString() } },
            Config = new HttpConfig
            {
                RetryCount = 3
            }
        };
        response = request.GetAsync().GetAwaiter().GetResult();
        Assert.IsFalse(response.IsSuccess);

        request = new HttpRequest(url)
        {
            Parameters = new Dictionary<string, string?> { { "count", "3" }, { "id", Guid.NewGuid().ToString() } },
            Config = new HttpConfig
            {
                RetryCount = 3
            }
        };
        response = request.GetAsync().GetAwaiter().GetResult();
        Assert.IsTrue(response.IsSuccess);
    }

    [TestMethod]
    public void TimeoutTest()
    {
        HttpConfig.Default.TimeOut = TimeSpan.FromSeconds(2);
        var url = BaseUrl.CombinePath("/api/get/timeout");
        var request = new HttpRequest(url) { Config = new HttpConfig { RetryCount = 0 } };
        var response = request.GetAsync().GetAwaiter().GetResult();
        Assert.IsTrue(response.IsSuccess);

        HttpConfig.Default.TimeOut = TimeSpan.FromSeconds(1);
        request = new HttpRequest(url) { Config = new HttpConfig { RetryCount = 0 } };
        response = request.GetAsync().GetAwaiter().GetResult();
        Assert.IsFalse(response.IsSuccess);

        request = new HttpRequest(url) { Config = new HttpConfig { TimeOut = TimeSpan.FromSeconds(2),RetryCount = 0 } };
        response = request.GetAsync().GetAwaiter().GetResult();
        Assert.IsTrue(response.IsSuccess);

        request = new HttpRequest(url) { Config = new HttpConfig { TimeOut = TimeSpan.FromSeconds(1), RetryCount = 0 } };
        response = request.GetAsync().GetAwaiter().GetResult();
        Assert.IsFalse(response.IsSuccess);
    }
}
