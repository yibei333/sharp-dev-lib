using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpDevLib.Standard;
using SharpDevLib.Tests.Data;
using SharpDevLib.Tests.Standard.Http.Base;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace SharpDevLib.Tests.Standard.Http;

[TestClass]
public class HttpPostTests : HttpBaseTests
{
    static string _userJson = new User("foo", 10).Serialize();

    [TestMethod]
    public void PostJsonTest()
    {
        var request = new HttpJsonRequest("/api/post", _userJson);
        var response = request.PostAsync().GetAwaiter().GetResult();
        Assert.IsTrue(response.IsSuccess);
    }

    [TestMethod]
    public void PostJsonIntTest()
    {
        var request = new HttpJsonRequest("/api/post/int", _userJson);
        var response = request.PostAsync<int>().GetAwaiter().GetResult();
        Assert.IsTrue(response.IsSuccess);
        Assert.AreEqual(10, response.Data);
    }

    [TestMethod]
    public void PostJsonStringTest()
    {
        var request = new HttpJsonRequest("/api/post/string", _userJson);
        var response = request.PostAsync<string>().GetAwaiter().GetResult();
        Assert.IsTrue(response.IsSuccess);
        Assert.AreEqual("foo", response.Data);
    }

    [TestMethod]
    public void PostJsonObjectTest()
    {
        var request = new HttpJsonRequest("/api/post/object", _userJson);
        var response = request.PostAsync<User>().GetAwaiter().GetResult();
        Assert.IsTrue(response.IsSuccess);
        Assert.IsNotNull(response.Data);
        Assert.AreEqual(10, response.Data.Age);
        Assert.AreEqual("foo", response.Data.Name);
    }

    [TestMethod]
    public void PostUrlEncodedFormTest()
    {
        var request = new HttpUrlEncodedFormRequest("/api/post/form", new Dictionary<string, string>
        {
            { "Name","foo" },
            { "Age","10" },
        });
        var response = request.PostAsync().GetAwaiter().GetResult();
        Assert.IsTrue(response.IsSuccess);
    }

    [TestMethod]
    public void PostUrlEncodedFormObjectTest()
    {
        var request = new HttpUrlEncodedFormRequest("/api/post/form/object", new Dictionary<string, string>
        {
            { "Name","foo" },
            { "Age","10" },
        });
        var response = request.PostAsync<User>().GetAwaiter().GetResult();
        Assert.IsTrue(response.IsSuccess);
        Assert.IsNotNull(response.Data);
        Assert.AreEqual(10, response.Data.Age);
        Assert.AreEqual("foo", response.Data.Name);
    }

    [TestMethod]
    public void PostMultiPartFormTest()
    {
        var request = new HttpMultiPartFormDataRequest("/api/post/form/multi", new Dictionary<string, string>
        {
            { "Name","foo" },
            { "Age","10" },
        },
        [
            new FormFile("file","TestFile.txt",File.ReadAllBytes(AppDomain.CurrentDomain.BaseDirectory.CombinePath("Data/TestFile.txt"))),
            new FormFile("file","Foo.txt",File.OpenRead(AppDomain.CurrentDomain.BaseDirectory.CombinePath("Data/Foo.txt"))),
        ]);
        var response = request.PostAsync().GetAwaiter().GetResult();
        Assert.IsTrue(response.IsSuccess);
    }

    [TestMethod]
    public void PostMultiPartFormObjectTest()
    {
        var request = new HttpMultiPartFormDataRequest("/api/post/form/multi/object", new Dictionary<string, string>
        {
            { "Name","foo" },
            { "Age","10" },
        },
        [
            new FormFile("file","TestFile.txt",File.ReadAllBytes(AppDomain.CurrentDomain.BaseDirectory.CombinePath("Data/TestFile.txt"))),
            new FormFile("file","Foo.txt",File.OpenRead(AppDomain.CurrentDomain.BaseDirectory.CombinePath("Data/Foo.txt"))),
        ]);
        var response = request.PostAsync<User>().GetAwaiter().GetResult();
        Assert.IsTrue(response.IsSuccess);
        Assert.IsNotNull(response.Data);
        Assert.AreEqual(10, response.Data.Age);
        Assert.AreEqual("foo", response.Data.Name);
        Assert.IsTrue(File.Exists(AppDomain.CurrentDomain.BaseDirectory.CombinePath("Data/Tests/PostTestFile.txt")));
        Assert.AreEqual("Hello,World!", File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory.CombinePath("Data/Tests/PostTestFile.txt")));
        Assert.IsTrue(File.Exists(AppDomain.CurrentDomain.BaseDirectory.CombinePath("Data/Tests/PostFoo.txt")));
        Assert.AreEqual("foo", File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory.CombinePath("Data/Tests/PostFoo.txt")));
    }
}
