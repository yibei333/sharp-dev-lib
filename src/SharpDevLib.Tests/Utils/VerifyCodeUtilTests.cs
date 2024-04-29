using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace SharpDevLib.Tests;

[TestClass]
public class VerifyCodeUtilTests
{
    [TestMethod]
    public void GenerateCodeTest()
    {
        var code = VerifyCodeUtil.GenerateCode();
        Assert.AreEqual(6, code.Code.Length);
        Console.WriteLine(code.Serialize().FormatJson());
        code = VerifyCodeUtil.GenerateCode(5);
        Console.WriteLine(code.Serialize().FormatJson());
        Assert.AreEqual(5, code.Code.Length);
        code = VerifyCodeUtil.GenerateCode(5, false);
        Console.WriteLine(code.Serialize().FormatJson());
        Assert.AreEqual(5, code.Code.Length);
    }

    [TestMethod]
    public void GenerateCodeImageTest()
    {
        var code = VerifyCodeUtil.GenerateCodeImage();
        var path = AppDomain.CurrentDomain.BaseDirectory.CombinePath("Data/verifycode.png");
        Assert.AreEqual(6, code.Code.Length);
        Assert.IsTrue(code.Binary?.Length > 0);
        code.SaveFile(path);
        Assert.IsTrue(File.Exists(path));
        Console.WriteLine(code.Serialize().FormatJson());
        Console.WriteLine($"<img src='{code.Base64File}'/>");
    }

    [TestMethod]
    [DataRow("")]
    [DataRow(null)]
    [ExpectedException(typeof(ArgumentNullException))]
    public void SaveFileArgumentNullExceptionTest(string? path) {
        var code = new VerifyCodeResult("123");
        code.SaveFile(path);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void SaveFileInvalidOperationExceptionTest() {
        var code = new VerifyCodeResult("123");
        code.SaveFile("aaa");
    }
}
