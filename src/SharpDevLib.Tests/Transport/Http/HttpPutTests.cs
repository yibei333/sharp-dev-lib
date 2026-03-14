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
        var response = await new HttpRequest(BaseUrl.CombinePath("/api/put"))
            .UseClientId("PutTest")
            .AddJson(_userJson)
            .PutAsync();
        Assert.IsTrue(response.IsSuccess);
    }

    [TestMethod]
    public async Task PutIntTest()
    {
        var response = await new HttpRequest(BaseUrl.CombinePath("/api/put/int"))
            .AddJson(_userJson)
            .PutAsync();
        Assert.IsTrue(response.IsSuccess);
        Assert.AreEqual(10, await response.GetResponseDataAsync<int>());
    }

    [TestMethod]
    public async Task PutStringTest()
    {
        var response = await new HttpRequest(BaseUrl.CombinePath("/api/put/string"))
            .AddJson(_userJson)
            .PutAsync();
        Assert.IsTrue(response.IsSuccess);
        Assert.AreEqual("foo", await response.GetResponseDataAsync<string>());
    }

    [TestMethod]
    public async Task PutObjectTest()
    {
        var response = await new HttpRequest(BaseUrl.CombinePath("/api/put/object"))
            .AddJson(_userJson)
            .PutAsync();
        Assert.IsTrue(response.IsSuccess);
        var data = await response.GetResponseDataAsync<User>();
        Assert.IsNotNull(data);
        Assert.AreEqual(10, data.Age);
        Assert.AreEqual("foo", data.Name);
    }
}
