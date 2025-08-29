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
        var request = new HttpKeyValueRequest("/api/delete", new Dictionary<string, string> { { "name", "foo" }, { "age", "10" } });
        var response = request.DeleteAsync().GetAwaiter().GetResult();
        Assert.IsTrue(response.IsSuccess);
    }

    [TestMethod]
    public void DeleteIntTest()
    {
        var count = 0;
        var request = new HttpKeyValueRequest("/api/delete/int", new Dictionary<string, string> { { "name", "foo" }, { "age", "10" } })
        {
            OnReceiveProgress = p =>
            {
                count++;
                Console.WriteLine($"receive->{p}");
            }
        };
        var response = request.DeleteAsync<int>().GetAwaiter().GetResult();
        Assert.IsGreaterThan(0, count);
        Assert.IsTrue(response.IsSuccess);
        Assert.AreEqual(10, response.Data);
    }

    [TestMethod]
    public void DeleteStringTest()
    {
        var request = new HttpKeyValueRequest("/api/delete/string", new Dictionary<string, string> { { "name", "foo" }, { "age", "10" } });
        var response = request.DeleteAsync<string>().GetAwaiter().GetResult();
        Assert.IsTrue(response.IsSuccess);
        Assert.AreEqual("foo", response.Data);
    }

    [TestMethod]
    public void DeleteObjectTest()
    {
        var request = new HttpKeyValueRequest("/api/delete/object", new Dictionary<string, string> { { "name", "foo" }, { "age", "10" } });
        var response = request.DeleteAsync<User>().GetAwaiter().GetResult();
        Assert.IsTrue(response.IsSuccess);
        Assert.IsNotNull(response.Data);
        Assert.AreEqual("foo", response.Data.Name);
        Assert.AreEqual(10, response.Data.Age);
    }
}
