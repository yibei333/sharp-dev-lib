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
        var request = new HttpJsonRequest("/api/put", _userJson)
        {
            UseEdgeUserAgent = false
        };
        var response = request.PutAsync().GetAwaiter().GetResult();
        Assert.IsTrue(response.IsSuccess);
    }

    [TestMethod]
    public void PutIntTest()
    {
        var request = new HttpJsonRequest("/api/put/int", _userJson);
        var response = request.PutAsync<int>().GetAwaiter().GetResult();
        Assert.IsTrue(response.IsSuccess);
        Assert.AreEqual(10, response.Data);
    }

    [TestMethod]
    public void PutStringTest()
    {
        var request = new HttpJsonRequest("/api/put/string", _userJson);
        var response = request.PutAsync<string>().GetAwaiter().GetResult();
        Assert.IsTrue(response.IsSuccess);
        Assert.AreEqual("foo", response.Data);
    }

    [TestMethod]
    public void PutObjectTest()
    {
        var request = new HttpJsonRequest("/api/put/object", _userJson);
        var response = request.PutAsync<User>().GetAwaiter().GetResult();
        Assert.IsTrue(response.IsSuccess);
        Assert.IsNotNull(response.Data);
        Assert.AreEqual(10, response.Data.Age);
        Assert.AreEqual("foo", response.Data.Name);
    }
}
