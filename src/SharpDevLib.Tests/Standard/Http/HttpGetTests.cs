using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpDevLib.Standard;
using SharpDevLib.Tests.Data;
using SharpDevLib.Tests.Standard.Http.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SharpDevLib.Tests.Standard.Http;

[TestClass]
public class HttpGetTests : HttpBaseTests
{
    [TestMethod]
    public void GetTest()
    {
        var request = new HttpKeyValueRequest("/api/get");
        var response = request.GetAsync().GetAwaiter().GetResult();
        Assert.IsTrue(response.IsSuccess);
    }

    [TestMethod]
    public void GetIntTest()
    {
        var count = 0;
        var request = new HttpKeyValueRequest("/api/get/int", new Dictionary<string, string> { { "seed", "1" } })
        {
            OnReceiveProgress = p =>
            {
                count++;
                Console.WriteLine($"receive->{p}");
            }
        };
        var response = request.GetAsync<int>().GetAwaiter().GetResult();
        Console.WriteLine(response);
        Assert.IsTrue(count > 0);
        Assert.IsTrue(response.IsSuccess);
        Assert.AreEqual(2, response.Data);
    }

    [TestMethod]
    public void GetStringTest()
    {
        var request = new HttpKeyValueRequest("/api/get/string", new Dictionary<string, string> { { "foo", "foo" }, { "bar", "bar" } });
        var response = request.GetAsync<string>().GetAwaiter().GetResult();
        Console.WriteLine(response);
        Assert.IsTrue(response.IsSuccess);
        Assert.AreEqual("foo_bar", response.Data);
    }

    [TestMethod]
    public void GetUserTest()
    {
        var request = new HttpKeyValueRequest("/api/get/user");
        var response = request.GetAsync<User>().GetAwaiter().GetResult();
        Assert.IsTrue(response.IsSuccess);
        Assert.IsNotNull(response.Data);
        Assert.AreEqual("foo", response.Data.Name);
        Assert.AreEqual(10, response.Data.Age);
    }

    [TestMethod]
    public void GetUsersTest()
    {
        var request = new HttpKeyValueRequest("/api/get/users");
        var response = request.GetAsync<List<User>>().GetAwaiter().GetResult();
        Assert.IsTrue(response.IsSuccess);
        Assert.IsNotNull(response.Data);
        Assert.AreEqual(2, response.Data.Count);
        Assert.AreEqual("foo", response.Data.First().Name);
        Assert.AreEqual(10, response.Data.First().Age);
        Assert.AreEqual("bar", response.Data.Last().Name);
        Assert.AreEqual(20, response.Data.Last().Age);
    }

    [TestMethod]
    public void CookieTest()
    {
        var url = "/api/get/cookie";
        var request = new HttpKeyValueRequest(url)
        {
            Headers = new Dictionary<string, IEnumerable<string>>
            {
                { "Cookie",new string[]{ "BIDUPSID=601145944A9976FC12AF00B3136B48F0; PSTM=1715422096; BAIDUID=59DA881A28C93BDBA3A461A5358B8861:FG=1; delPer=0; PSINO=7; ZFY=m:Bnz:BEloKMh6w68:AuIR5F:Bb:BXP9xty8LFvFaveyp9H4:C; BAIDUID_BFESS=59DA881A28C93BDBA3A461A5358B8861:FG=1; H_WISE_SIDS=60175_60269_60274_60289_60299; H_PS_PSSID=60269_60274_60289_60299; BA_HECTOR=80aga10g04050ga5ak8lak85dtl3dc1j4lbe61v; BDRCVFR[S4-dAuiWMmn]=I67x6TjHwwYf0; BDORZ=B490B5EBF6F3CD402E515D22BCDA1598; RT=\"z=1&dm=baidu.com&si=7b9c824a-f453-4b8f-af11-4d5eae458720&ss=lw8my2mv&sl=0&tt=0&bcn=https%3A%2F%2Ffclog.baidu.com%2Flog%2Fweirwood%3Ftype%3Dperf\"" } }
            }
        };
        var response = request.GetAsync().GetAwaiter().GetResult();
        Console.WriteLine(response);
        Assert.IsTrue(response.IsSuccess);
        Assert.AreEqual(1, response.Cookies?.Count(x => x.Name == "BIDUPSID"));
        Assert.AreEqual("601145944A9976FC12AF00B3136B48F0", response.Cookies?.FirstOrDefault(x => x.Name == "BIDUPSID")?.Value);
    }
}
