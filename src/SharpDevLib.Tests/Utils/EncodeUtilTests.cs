using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpDevLib.Tests.Data;
using System;
using System.Text;

namespace SharpDevLib.Tests;

[TestClass]
public class EncodeUtilTests
{
    [TestMethod]
    public void Base64EncodeObjectTest()
    {
        Department? department = null;
        Assert.AreEqual(string.Empty, department.Base64Encode());

        department = Department.Create();
        department.Id = Guid.Empty;
        //json:{"Id":"00000000-0000-0000-0000-000000000000","Name":"IT","ParentId":null,"Order":1,"Children":[]}
        var expected = "eyJJZCI6IjAwMDAwMDAwLTAwMDAtMDAwMC0wMDAwLTAwMDAwMDAwMDAwMCIsIk5hbWUiOiJJVCIsIlBhcmVudElkIjpudWxsLCJPcmRlciI6MSwiQ2hpbGRyZW4iOltdfQ==";
        Assert.AreEqual(expected, department.Base64Encode());
    }

    [TestMethod]
    public void Base64EncodeBytesTest()
    {
        var bytes = Array.Empty<byte>();
        var actual= bytes.Base64Encode();
        var expected = string.Empty;
        Assert.AreEqual(expected,actual);

        bytes=Encoding.UTF8.GetBytes("{\"Id\":\"00000000-0000-0000-0000-000000000000\",\"Name\":\"IT\",\"ParentId\":null,\"Order\":1,\"Children\":[]}");
        actual= bytes.Base64Encode();
        expected = "eyJJZCI6IjAwMDAwMDAwLTAwMDAtMDAwMC0wMDAwLTAwMDAwMDAwMDAwMCIsIk5hbWUiOiJJVCIsIlBhcmVudElkIjpudWxsLCJPcmRlciI6MSwiQ2hpbGRyZW4iOltdfQ==";
        Assert.AreEqual(expected,actual);
    }

    [TestMethod]
    [DataRow("","")]
    [DataRow(null, "")]
    [DataRow("abc", "YWJj")]
    [DataRow("hello , world!", "aGVsbG8gLCB3b3JsZCE=")]
    public void Base64EncodeStringTest(string str,string expected)
    {
        var actual = str.Base64Encode();
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    [DataRow("", "")]
    [DataRow(null, "")]
    [DataRow("YWJj", "abc")]
    [DataRow("aGVsbG8gLCB3b3JsZCE=", "hello , world!")]
    public void Base64DecodeTest(string str, string expected)
    {
        var actual = str.Base64Decode();
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    [DataRow("","")]
    [DataRow(null,"")]
    [DataRow("https://docs.microsoft.com?a=1&b=2", "https%3A%2F%2Fdocs.microsoft.com%3Fa%3D1%26b%3D2")]
    public void UrlEncodeTest(string url, string expected)
    {
        var actual = url.UrlEncode();
        Assert.AreEqual(expected.ToLower(), actual.ToLower());
    }

    [TestMethod]
    [DataRow("", "")]
    [DataRow(null, "")]
    [DataRow("https%3A%2F%2Fdocs.microsoft.com%3Fa%3D1%26b%3D2", "https://docs.microsoft.com?a=1&b=2")]
    public void UrlDecodeTest(string url, string expected)
    {
        var actual = url.UrlDecode();
        Assert.AreEqual(expected.ToLower(), actual.ToLower());
    }

    [TestMethod]
    [DataRow("", "")]
    [DataRow(null, "")]
    [DataRow("https://docs.microsoft.com?a=1&b=2", "aHR0cHM6Ly9kb2NzLm1pY3Jvc29mdC5jb20_YT0xJmI9Mg")]
    [DataRow("https://docs.microsoft.com?a=1&b=2&c=3", "aHR0cHM6Ly9kb2NzLm1pY3Jvc29mdC5jb20_YT0xJmI9MiZjPTM")]
    [DataRow("https://docs.microsoft.com?a=1&b=2&c=31", "aHR0cHM6Ly9kb2NzLm1pY3Jvc29mdC5jb20_YT0xJmI9MiZjPTMx")]
    public void Base64UrlEncodeTest(string url, string expected)
    {
        var actual = url.Base64UrlEncode();
        Assert.AreEqual(expected.ToLower(), actual.ToLower());
    }

    [TestMethod]
    public void Base64UrlEncodeBytesTest()
    {
        var bytes = Array.Empty<byte>();
        var actual = bytes.Base64UrlEncode();
        var expected = string.Empty;
        Assert.AreEqual(expected, actual);

        bytes = Encoding.UTF8.GetBytes("https://docs.microsoft.com?a=1&b=2");
        actual = bytes.Base64UrlEncode();
        expected = "aHR0cHM6Ly9kb2NzLm1pY3Jvc29mdC5jb20_YT0xJmI9Mg";
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    [DataRow("", "")]
    [DataRow(null, "")]
    [DataRow("aHR0cHM6Ly9kb2NzLm1pY3Jvc29mdC5jb20_YT0xJmI9Mg", "https://docs.microsoft.com?a=1&b=2")]
    [DataRow("aHR0cHM6Ly9kb2NzLm1pY3Jvc29mdC5jb20_YT0xJmI9MiZjPTM", "https://docs.microsoft.com?a=1&b=2&c=3")]
    [DataRow("aHR0cHM6Ly9kb2NzLm1pY3Jvc29mdC5jb20_YT0xJmI9MiZjPTMx", "https://docs.microsoft.com?a=1&b=2&c=31")]
    public void Base64UrlDecodeTest(string url, string expected)
    {
        var actual = url.Base64UrlDecode();
        Assert.AreEqual(expected.ToLower(), actual.ToLower());
    }

    [TestMethod]
    [DataRow("aHR0cHM6Ly9kb2NzLm1pY3Jvc29mdC5jb20_YT0xJmI9MiZjPTM==")]
    [DataRow("aHR0cHM6Ly9kb2NzLm1pY3Jvc29mdC5jb20_YT0xJmI9MiZjPTMx=")]
    [DataRow("aHR0cHM6Ly9kb2NzLm1pY3Jvc29mdC5jb20_YT0xJmI9MiZjPTMx==")]
    [ExpectedException(typeof(FormatException))]
    public void Base64UrlDecodeExceptionTest(string url)
    {
        _ = url.Base64UrlDecode();
    }

    [TestMethod]
    [DataRow("", "")]
    [DataRow(null, "")]
    [DataRow("aHR0cHM6Ly9kb2NzLm1pY3Jvc29mdC5jb20_YT0xJmI9Mg", "https://docs.microsoft.com?a=1&b=2")]
    [DataRow("aHR0cHM6Ly9kb2NzLm1pY3Jvc29mdC5jb20_YT0xJmI9MiZjPTM", "https://docs.microsoft.com?a=1&b=2&c=3")]
    [DataRow("aHR0cHM6Ly9kb2NzLm1pY3Jvc29mdC5jb20_YT0xJmI9MiZjPTMx", "https://docs.microsoft.com?a=1&b=2&c=31")]
    public void Base64UrlDecodeBytesTest(string url, string expected)
    {
        var bytes = url.Base64UrlDecodeBytes();
        var actual = Encoding.UTF8.GetString(bytes);
        Assert.AreEqual(expected.ToLower(), actual.ToLower());
    }
}
