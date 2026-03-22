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
        await HttpHelper
            .NewRequest(BaseUrl.CombinePath("/api/delete"))
            .AddParameter("name", "foo")
            .AddParameter("age", "10")
            .DeleteAsync()
            .EnsureSuccessStatusCode();
    }

    [TestMethod]
    public async Task DeleteIntTest()
    {
        var actual = await HttpHelper
            .NewRequest(BaseUrl.CombinePath("/api/delete/int"))
            .AddParameter("name", "foo")
            .AddParameter("age", "10")
            .DeleteAsync()
            .EnsureSuccessStatusCode()
            .GetResponseDataAsync<int>();
        Assert.AreEqual(10, actual);
    }

    [TestMethod]
    public async Task DeleteStringTest()
    {
        var actual = await HttpHelper
            .NewRequest(BaseUrl.CombinePath("/api/delete/string"))
            .AddParameter("name", "foo")
            .AddParameter("age", "10")
            .DeleteAsync()
            .EnsureSuccessStatusCode()
            .GetResponseDataAsync<string>();
        Assert.AreEqual("foo", actual);
    }

    [TestMethod]
    public async Task DeleteObjectTest()
    {
        var data = await HttpHelper
            .NewRequest(BaseUrl.CombinePath("/api/delete/object"))
            .AddParameter("name", "foo")
            .AddParameter("age", "10")
            .DeleteAsync()
            .EnsureSuccessStatusCode()
            .GetResponseDataAsync<User>();
        Assert.IsNotNull(data);
        Assert.AreEqual("foo", data.Name);
        Assert.AreEqual(10, data.Age);
    }
}
