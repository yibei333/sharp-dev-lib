using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace SharpDevLib.Tests.Standard.Random;

[TestClass]
public class RandomTests
{
    [TestMethod]
    public void DefaultGenerateCodeTest()
    {
        var code = new System.Random().GenerateCode();
        Console.WriteLine(code);
        Assert.AreEqual(6, code.Length);
    }

    [TestMethod]
    public void OnlyNumberGenerateCodeTest()
    {
        var code = new System.Random().GenerateCode(new GenerateRandomCodeOption { UseLowerLetter = false, UseUpperLetter = false });
        Console.WriteLine(code);
        Assert.AreEqual(6, code.Length);
    }

    [TestMethod]
    public void OnlyLowerLetterGenerateCodeTest()
    {
        var code = new System.Random().GenerateCode(new GenerateRandomCodeOption { UseNumber = false, UseUpperLetter = false });
        Console.WriteLine(code);
        Assert.AreEqual(6, code.Length);
    }

    [TestMethod]
    public void OnlyUpperLetterGenerateCodeTest()
    {
        var code = new System.Random().GenerateCode(new GenerateRandomCodeOption { UseNumber = false, UseLowerLetter = false });
        Console.WriteLine(code);
        Assert.AreEqual(6, code.Length);
    }

    [TestMethod]
    public void OnlySpecialSymbolGenerateCodeTest()
    {
        var code = new System.Random().GenerateCode(new GenerateRandomCodeOption { UseNumber = false, UseLowerLetter = false, UseUpperLetter = false, UseSpecialSymbol = true });
        Console.WriteLine(code);
        Assert.AreEqual(6, code.Length);
    }

    [TestMethod]
    public void MixGenerateCodeTest()
    {
        var code = new System.Random().GenerateCode(new GenerateRandomCodeOption { UseSpecialSymbol = true });
        Console.WriteLine(code);
        Assert.AreEqual(6, code.Length);
    }

    [TestMethod]
    public void CustomGenerateCodeTest()
    {
        var code = new System.Random().GenerateCode(new GenerateRandomCodeOption { UseCustomSeed = true, CustomSeed = "123abc" });
        Console.WriteLine(code);
        Assert.AreEqual(6, code.Length);
    }

    [TestMethod]
    public void LengthGenerateCodeTest()
    {
        var code = new System.Random().GenerateCode(new GenerateRandomCodeOption { UseSpecialSymbol = true, Length = 30 });
        Console.WriteLine(code);
        Assert.AreEqual(30, code.Length);
    }
}
