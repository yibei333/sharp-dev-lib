using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpDevLib.Standard;
using System;

namespace SharpDevLib.Tests.Standard.Http;

[TestClass]
public class HttpGetTests
{
    [TestMethod]
    public void GetTest()
    {
        var parameters = new System.Collections.Generic.Dictionary<string, string>
        {
            { "wd","foo" }
        };
        var request = new HttpKeyValueRequest("https://www.baidu.com", parameters)
        {
            OnSendProgress = p => Console.WriteLine($"send->{p}"),
            OnReceiveProgress = p => Console.WriteLine($"receive->{p}")
        };
        var response = request.GetAsync().GetAwaiter().GetResult();
        Assert.IsTrue(response.IsSuccess);
        Console.WriteLine(request);
        Console.WriteLine(response);
    }
}
