using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpDevLib.Tests.TestData;
using SharpDevLib.Tests.Transport.Http.Base;
using System.Threading.Tasks;

namespace SharpDevLib.Tests.Transport.Http;

[TestClass]
public class HttpDeleteTests : HttpBaseTests
{
    [TestMethod]
    public async Task DeleteTest()
    {
        var response = await new HttpRequest(BaseUrl.CombinePath("/api/delete"))
            .AddParameter("name", "foo")
            .AddParameter("age", "10")
            .DeleteAsync();
        Assert.IsTrue(response.IsSuccess);
    }

    [TestMethod]
    public async Task DeleteIntTest()
    {
        var response = await new HttpRequest(BaseUrl.CombinePath("/api/delete/int"))
            .AddParameter("name", "foo")
            .AddParameter("age", "10")
            .DeleteAsync();
        Assert.IsTrue(response.IsSuccess);
        var actual = await response.GetResponseDataAsync<int>();
        Assert.AreEqual(10, actual);
    }

    [TestMethod]
    public async Task DeleteStringTest()
    {
        var response = await new HttpRequest(BaseUrl.CombinePath("/api/delete/string"))
            .AddParameter("name", "foo")
            .AddParameter("age", "10")
            .DeleteAsync();
        Assert.IsTrue(response.IsSuccess);
        var actual = await response.GetResponseDataAsync<string>();
        Assert.AreEqual("foo", actual);
    }

    [TestMethod]
    public async Task DeleteObjectTest()
    {
        var response = await new HttpRequest(BaseUrl.CombinePath("/api/delete/object"))
            .AddParameter("name", "foo")
            .AddParameter("age", "10")
            .DeleteAsync();
        Assert.IsTrue(response.IsSuccess);
        var data = await response.GetResponseDataAsync<User>();
        Assert.IsNotNull(data);
        Assert.AreEqual("foo", data.Name);
        Assert.AreEqual(10, data.Age);
    }
}
