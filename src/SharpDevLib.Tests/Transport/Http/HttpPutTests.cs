using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpDevLib.Tests.TestData;
using SharpDevLib.Tests.Transport.Http.Base;
using System.Threading.Tasks;

namespace SharpDevLib.Tests.Transport.Http;

[TestClass]
public class HttpPutTests : HttpBaseTests
{
    static readonly string _userJson = new User("foo", 10).Serialize();

    [TestMethod]
    public async Task PutTest()
    {
        HttpHelper.SetConfig("PutTest", new HttpConfig { UserAgent = null });
        await new HttpRequestModel(BaseUrl.CombinePath("/api/put"))
            .UseClientId("PutTest")
            .AddJson(_userJson)
            .PutAsync()
            .EnsureSuccessStatusCode();
    }

    [TestMethod]
    public async Task PutIntTest()
    {
        var actual = await new HttpRequestModel(BaseUrl.CombinePath("/api/put/int"))
            .AddJson(_userJson)
            .PutAsync()
            .EnsureSuccessStatusCode()
            .GetResponseDataAsync<int>();
        Assert.AreEqual(10, actual);
    }

    [TestMethod]
    public async Task PutStringTest()
    {
        var actual = await new HttpRequestModel(BaseUrl.CombinePath("/api/put/string"))
            .AddJson(_userJson)
            .PutAsync()
            .EnsureSuccessStatusCode()
            .GetResponseDataAsync<string>();
        Assert.AreEqual("foo", actual);
    }

    [TestMethod]
    public async Task PutObjectTest()
    {
        var data = await new HttpRequestModel(BaseUrl.CombinePath("/api/put/object"))
            .AddJson(_userJson)
            .PutAsync()
            .EnsureSuccessStatusCode()
            .GetResponseDataAsync<User>();
        Assert.IsNotNull(data);
        Assert.AreEqual(10, data.Age);
        Assert.AreEqual("foo", data.Name);
    }
}
