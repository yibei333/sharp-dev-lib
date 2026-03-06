using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpDevLib.Tests.TestData;
using SharpDevLib.Tests.Transport.Http.Base;
using System;
using System.IO;

namespace SharpDevLib.Tests.Transport.Http;

[TestClass]
public class HttpPostTests : HttpBaseTests
{
    static readonly string _userJson = new User("foo", 10).Serialize();

    [TestMethod]
    public void PostJsonTest()
    {
        var response = new HttpRequest(BaseUrl.CombinePath("/api/post"), _userJson)
            .PostAsync()
            .GetAwaiter()
            .GetResult();
        Assert.IsTrue(response.IsSuccess);
    }

    [TestMethod]
    public void PostJsonIntTest()
    {
        var response = new HttpRequest(BaseUrl.CombinePath("/api/post/int"), _userJson)
            .PostAsync()
            .GetAwaiter()
            .GetResult();
        Assert.IsTrue(response.IsSuccess);
        Assert.AreEqual(10, response.GetResponseDataAsync<int>().GetAwaiter().GetResult());
    }

    [TestMethod]
    public void PostJsonStringTest()
    {
        var response = new HttpRequest(BaseUrl.CombinePath("/api/post/string"), _userJson)
            .PostAsync()
            .GetAwaiter()
            .GetResult();
        Assert.IsTrue(response.IsSuccess);
        Assert.AreEqual("foo", response.GetResponseDataAsync<string>().GetAwaiter().GetResult());
    }

    [TestMethod]
    public void PostJsonObjectTest()
    {
        var response = new HttpRequest(BaseUrl.CombinePath("/api/post/object"), _userJson)
            .PostAsync()
            .GetAwaiter()
            .GetResult();
        Assert.IsTrue(response.IsSuccess);
        var data = response.GetResponseDataAsync<User>().GetAwaiter().GetResult();
        Assert.IsNotNull(data);
        Assert.AreEqual(10, data.Age);
        Assert.AreEqual("foo", data.Name);
        Console.WriteLine(response.ToString());
    }

    [TestMethod]
    public void PostUrlEncodedFormTest()
    {
        var response = new HttpRequest(BaseUrl.CombinePath("/api/post/form"))
            .AddParameter("Name", "foo")
            .AddParameter("Age", "10")
            .PostAsync()
            .GetAwaiter()
            .GetResult();
        Assert.IsTrue(response.IsSuccess);
    }

    [TestMethod]
    public void PostUrlEncodedFormObjectTest()
    {
        var response = new HttpRequest(BaseUrl.CombinePath("/api/post/form/object"))
            .AddParameter("Name", "foo")
            .AddParameter("Age", "10")
            .PostAsync()
            .GetAwaiter()
            .GetResult();
        Assert.IsTrue(response.IsSuccess);
        var data = response.GetResponseDataAsync<User>().GetAwaiter().GetResult();
        Assert.IsNotNull(data);
        Assert.AreEqual(10, data.Age);
        Assert.AreEqual("foo", data.Name);
        Console.WriteLine(response.ToString());
    }

    [TestMethod]
    public void PostMultiPartFormTest()
    {
        var response = new HttpRequest(BaseUrl.CombinePath("/api/post/form/multi"))
            .AddParameter("Name", "foo")
            .AddParameter("Age", "10")
            .AddFile(new HttpFormFile("file", "TestFile.txt", File.ReadAllBytes(AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/TestFile.txt"))))
            .AddFile(new HttpFormFile("file", "Foo.txt", File.OpenRead(AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/Foo.txt"))))
            .PostAsync()
            .GetAwaiter()
            .GetResult();
        Assert.IsTrue(response.IsSuccess);
    }

    [TestMethod]
    public void PostMultiPartFormObjectTest()
    {
        var sendCount = 0;
        var receiveCount = 0;
        var config = new HttpConfig
        {
            OnReceiveProgress = p =>
            {
                receiveCount++;
                Console.WriteLine($"receive->{p}");
            },
            OnSendProgress = p =>
            {
                sendCount++;
                Console.WriteLine($"send->{p}");
            }
        };
        var response = new HttpRequest(BaseUrl.CombinePath("/api/post/form/multi/object")) { Config = config }
            .AddParameter("Name", "foo")
            .AddParameter("Age", "10")
            .AddFile(new HttpFormFile("file", "TestFile.txt", File.ReadAllBytes(AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/TestFile.txt"))))
            .AddFile(new HttpFormFile("file", "Foo.txt", File.OpenRead(AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/Foo.txt"))))
            .PostAsync()
            .GetAwaiter()
            .GetResult();
        Assert.IsTrue(response.IsSuccess);
        Assert.IsGreaterThan(0, sendCount);
        Assert.IsGreaterThan(0, receiveCount);
        var data = response.GetResponseDataAsync<User>().GetAwaiter().GetResult();
        Assert.IsNotNull(data);
        Assert.AreEqual(10, data.Age);
        Assert.AreEqual("foo", data.Name);
        Assert.IsTrue(File.Exists(AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/Tests/PostTestFile.txt")));
        Assert.AreEqual("Hello,World!", File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/Tests/PostTestFile.txt")));
        Assert.IsTrue(File.Exists(AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/Tests/PostFoo.txt")));
        Assert.AreEqual("foo", File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/Tests/PostFoo.txt")));
        Console.WriteLine(response.ToString());
    }
}
