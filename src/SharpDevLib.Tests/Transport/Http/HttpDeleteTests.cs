using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpDevLib.Tests.TestData;
using SharpDevLib.Tests.Transport.Http.Base;
using SharpDevLib.Transport;
using System;
using System.Collections.Generic;

namespace SharpDevLib.Tests.Transport.Http;

[TestClass]
public class HttpDeleteTests : HttpBaseTests
{
    [TestMethod]
    public void DeleteTest()
    {
        var request = new HttpRequest(BaseUrl.CombinePath("/api/delete")) { Parameters = new Dictionary<string, string?> { { "name", "foo" }, { "age", "10" } } };
        var response = request.DeleteAsync().GetAwaiter().GetResult();
        Assert.IsTrue(response.IsSuccess);
    }

    [TestMethod]
    public void DeleteIntTest()
    {
        var count = 0;
        var request = new HttpRequest(BaseUrl.CombinePath("/api/delete/int"))
        {
            Parameters = new Dictionary<string, string?> { { "name", "foo" }, { "age", "10" } },
            Config = new HttpConfig
            {
                OnReceiveProgress = p =>
                {
                    count++;
                    Console.WriteLine($"receive->{p}");
                }
            }
        };
        var response = request.DeleteAsync().GetAwaiter().GetResult();
        Assert.IsGreaterThan(0, count);
        Assert.IsTrue(response.IsSuccess);
        var actual = response.GetResponseDataAsync<int>().GetAwaiter().GetResult();
        Assert.AreEqual(10, actual);
    }

    [TestMethod]
    public void DeleteStringTest()
    {
        var request = new HttpRequest(BaseUrl.CombinePath("/api/delete/string")) { Parameters = new Dictionary<string, string?> { { "name", "foo" }, { "age", "10" } } };
        var response = request.DeleteAsync().GetAwaiter().GetResult();
        Assert.IsTrue(response.IsSuccess);
        var actual = response.GetResponseDataAsync<string>().GetAwaiter().GetResult();
        Assert.AreEqual("foo", actual);
    }

    [TestMethod]
    public void DeleteObjectTest()
    {
        var request = new HttpRequest(BaseUrl.CombinePath("/api/delete/object")) { Parameters = new Dictionary<string, string?> { { "name", "foo" }, { "age", "10" } } };
        var response = request.DeleteAsync().GetAwaiter().GetResult();
        Assert.IsTrue(response.IsSuccess);
        var data = response.GetResponseDataAsync<User>().GetAwaiter().GetResult();
        Assert.IsNotNull(data);
        Assert.AreEqual("foo", data.Name);
        Assert.AreEqual(10, data.Age);
    }
}
