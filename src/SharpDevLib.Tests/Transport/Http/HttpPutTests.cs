using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpDevLib.Tests.TestData;
using SharpDevLib.Tests.Transport.Http.Base;

namespace SharpDevLib.Tests.Transport.Http;

[TestClass]
public class HttpPutTests : HttpBaseTests
{
    static readonly string _userJson = new User("foo", 10).Serialize();

    [TestMethod]
    public void PutTest()
    {
        var response = new HttpRequest(BaseUrl.CombinePath("/api/put"))
            .AddJson(_userJson)
            .SetConfig(new HttpConfig { UserAgent = null })
            .PutAsync()
            .GetAwaiter()
            .GetResult();
        Assert.IsTrue(response.IsSuccess);
    }

    [TestMethod]
    public void PutIntTest()
    {
        var response = new HttpRequest(BaseUrl.CombinePath("/api/put/int"))
            .AddJson(_userJson)
            .PutAsync()
            .GetAwaiter()
            .GetResult();
        Assert.IsTrue(response.IsSuccess);
        Assert.AreEqual(10, response.GetResponseDataAsync<int>().GetAwaiter().GetResult());
    }

    [TestMethod]
    public void PutStringTest()
    {
        var response = new HttpRequest(BaseUrl.CombinePath("/api/put/string"))
            .AddJson(_userJson)
            .PutAsync()
            .GetAwaiter()
            .GetResult();
        Assert.IsTrue(response.IsSuccess);
        Assert.AreEqual("foo", response.GetResponseDataAsync<string>().GetAwaiter().GetResult());
    }

    [TestMethod]
    public void PutObjectTest()
    {
        var response = new HttpRequest(BaseUrl.CombinePath("/api/put/object"))
            .AddJson(_userJson)
            .PutAsync()
            .GetAwaiter()
            .GetResult();
        Assert.IsTrue(response.IsSuccess);
        var data = response.GetResponseDataAsync<User>().GetAwaiter().GetResult();
        Assert.IsNotNull(data);
        Assert.AreEqual(10, data.Age);
        Assert.AreEqual("foo", data.Name);
    }
}
