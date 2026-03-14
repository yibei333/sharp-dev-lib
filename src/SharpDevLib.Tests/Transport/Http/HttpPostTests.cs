using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpDevLib.Tests.TestData;
using SharpDevLib.Tests.Transport.Http.Base;
using System;
using System.IO;
using System.Threading.Tasks;

namespace SharpDevLib.Tests.Transport.Http;

[TestClass]
public class HttpPostTests : HttpBaseTests
{
    static readonly string _userJson = new User("foo", 10).Serialize();

    [TestMethod]
    public async Task PostJsonTest()
    {
        var response = await new HttpRequest(BaseUrl.CombinePath("/api/post"))
            .AddJson(_userJson)
            .PostAsync();
        Assert.IsTrue(response.IsSuccess);
    }

    [TestMethod]
    public async Task PostJsonIntTest()
    {
        Console.WriteLine(_userJson.Utf8Decode().Length);
        HttpHelper.SetConfig("PostJsonIntTest", new HttpConfig
        {
            OnSendProgress = Console.WriteLine
        });
        var response = await new HttpRequest(BaseUrl.CombinePath("/api/post/int"))
            .UseClientId("PostJsonIntTest")
            .AddJson(_userJson)
            .PostAsync();
        Assert.IsTrue(response.IsSuccess);
        Assert.AreEqual(10, await response.GetResponseDataAsync<int>());
    }

    [TestMethod]
    public async Task PostJsonStringTest()
    {
        var response = await new HttpRequest(BaseUrl.CombinePath("/api/post/string"))
            .AddJson(_userJson)
            .PostAsync();
        Assert.IsTrue(response.IsSuccess);
        Assert.AreEqual("foo", await response.GetResponseDataAsync<string>());
    }

    [TestMethod]
    public async Task PostJsonObjectTest()
    {
        var response = await new HttpRequest(BaseUrl.CombinePath("/api/post/object"))
            .AddJson(_userJson)
            .PostAsync();
        Assert.IsTrue(response.IsSuccess);
        var data = await response.GetResponseDataAsync<User>();
        Assert.IsNotNull(data);
        Assert.AreEqual(10, data.Age);
        Assert.AreEqual("foo", data.Name);
        Console.WriteLine(response.ToString());
    }

    [TestMethod]
    public async Task PostUrlEncodedFormTest()
    {
        var response = await new HttpRequest(BaseUrl.CombinePath("/api/post/form"))
            .AddParameter("Name", "foo")
            .AddParameter("Age", "10")
            .PostAsync();
        Assert.IsTrue(response.IsSuccess);
    }

    [TestMethod]
    public async Task PostUrlEncodedFormObjectTest()
    {
        var response = await new HttpRequest(BaseUrl.CombinePath("/api/post/form/object"))
            .AddParameter("Name", "foo")
            .AddParameter("Age", "10")
            .PostAsync();
        Assert.IsTrue(response.IsSuccess);
        var data = await response.GetResponseDataAsync<User>();
        Assert.IsNotNull(data);
        Assert.AreEqual(10, data.Age);
        Assert.AreEqual("foo", data.Name);
        Console.WriteLine(response.ToString());
    }

    [TestMethod]
    public async Task PostMultiPartFormTest()
    {
        var response = await new HttpRequest(BaseUrl.CombinePath("/api/post/form/multi"))
            .AddParameter("Name", "foo")
            .AddParameter("Age", "10")
            .AddFile(new HttpFormFile("file", "TestFile.txt", File.ReadAllBytes(AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/TestFile.txt"))))
            .AddFile(new HttpFormFile("file", "Foo.txt", File.OpenRead(AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/Foo.txt"))))
            .PostAsync();
        Assert.IsTrue(response.IsSuccess);
    }

    [TestMethod]
    public async Task PostMultiPartFormObjectTest()
    {
        var sendCount = 0;
        var config = new HttpConfig
        {
            OnSendProgress = p =>
            {
                sendCount++;
                Console.WriteLine($"send->{p}");
            }
        };
        HttpHelper.SetConfig("PostMultiPartFormObjectTest", config);
        var response = await new HttpRequest(BaseUrl.CombinePath("/api/post/form/multi/object"))
            .UseClientId("PostMultiPartFormObjectTest")
            .AddParameter("Name", "foo")
            .AddParameter("Age", "10")
            .AddFile(new HttpFormFile("file", "TestFile.txt", File.ReadAllBytes(AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/TestFile.txt"))))
            .AddFile(new HttpFormFile("file", "Foo.txt", File.OpenRead(AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/Foo.txt"))))
            .PostAsync();
        Assert.IsTrue(response.IsSuccess);
        Assert.IsGreaterThan(0, sendCount);
        var data = await response.GetResponseDataAsync<User>();
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
