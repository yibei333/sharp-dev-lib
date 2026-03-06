using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpDevLib.Tests.TestData;
using SharpDevLib.Tests.Transport.Http.Base;
using SharpDevLib.Transport;

namespace SharpDevLib.Tests.Transport.Http;

[TestClass]
public class HttpPutTests : HttpBaseTests
{
    static readonly string _userJson = new User("foo", 10).Serialize();

    [TestMethod]
    public void PutTest()
    {
        var request = new HttpRequest(BaseUrl.CombinePath("/api/put"), _userJson)
        {
            Config = new HttpConfig
            {
                UserAgent = null
            }
        };
        var response = request.PutAsync().GetAwaiter().GetResult();
        Assert.IsTrue(response.IsSuccess);
    }

    [TestMethod]
    public void PutIntTest()
    {
        var request = new HttpRequest(BaseUrl.CombinePath("/api/put/int"), _userJson);
        var response = request.PutAsync().GetAwaiter().GetResult();
        Assert.IsTrue(response.IsSuccess);
        Assert.AreEqual(10, response.GetResponseDataAsync<int>().GetAwaiter().GetResult());
    }

    [TestMethod]
    public void PutStringTest()
    {
        var request = new HttpRequest(BaseUrl.CombinePath("/api/put/string"), _userJson);
        var response = request.PutAsync().GetAwaiter().GetResult();
        Assert.IsTrue(response.IsSuccess);
        Assert.AreEqual("foo", response.GetResponseDataAsync<string>().GetAwaiter().GetResult());
    }

    [TestMethod]
    public void PutObjectTest()
    {
        var request = new HttpRequest(BaseUrl.CombinePath("/api/put/object"), _userJson);
        var response = request.PutAsync().GetAwaiter().GetResult();
        Assert.IsTrue(response.IsSuccess);
        var data = response.GetResponseDataAsync<User>().GetAwaiter().GetResult();
        Assert.IsNotNull(data);
        Assert.AreEqual(10, data.Age);
        Assert.AreEqual("foo", data.Name);
    }
}
