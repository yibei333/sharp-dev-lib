using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpDevLib.Tests.TestData;
using SharpDevLib.Tests.Transport.Http.Base;
using System;

namespace SharpDevLib.Tests.Transport.Http;

[TestClass]
public class HttpDeleteTests : HttpBaseTests
{
    [TestMethod]
    public void DeleteTest()
    {
        var response = new HttpRequest(BaseUrl.CombinePath("/api/delete"))
            .AddParameter("name", "foo")
            .AddParameter("age", "10")
            .DeleteAsync()
            .GetAwaiter()
            .GetResult();
        Assert.IsTrue(response.IsSuccess);
    }

    [TestMethod]
    public void DeleteIntTest()
    {
        var response = new HttpRequest(BaseUrl.CombinePath("/api/delete/int"))
            .AddParameter("name", "foo")
            .AddParameter("age", "10")
            .DeleteAsync()
            .GetAwaiter()
            .GetResult();
        Assert.IsTrue(response.IsSuccess);
        var actual = response.GetResponseDataAsync<int>().GetAwaiter().GetResult();
        Assert.AreEqual(10, actual);
    }

    [TestMethod]
    public void DeleteStringTest()
    {
        var response = new HttpRequest(BaseUrl.CombinePath("/api/delete/string"))
            .AddParameter("name", "foo")
            .AddParameter("age", "10")
            .DeleteAsync()
            .GetAwaiter()
            .GetResult();
        Assert.IsTrue(response.IsSuccess);
        var actual = response.GetResponseDataAsync<string>().GetAwaiter().GetResult();
        Assert.AreEqual("foo", actual);
    }

    [TestMethod]
    public void DeleteObjectTest()
    {
        var response = new HttpRequest(BaseUrl.CombinePath("/api/delete/object"))
            .AddParameter("name", "foo")
            .AddParameter("age", "10")
            .DeleteAsync()
            .GetAwaiter()
            .GetResult();
        Assert.IsTrue(response.IsSuccess);
        var data = response.GetResponseDataAsync<User>().GetAwaiter().GetResult();
        Assert.IsNotNull(data);
        Assert.AreEqual("foo", data.Name);
        Assert.AreEqual(10, data.Age);
    }
}
