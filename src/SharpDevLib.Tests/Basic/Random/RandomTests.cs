using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace SharpDevLib.Tests.Basic.Random;

[TestClass]
public class RandomTests
{
    const string _seed = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ~!@#$%^&*()_+{}:<>?.,/';\"[]\\|-=`";
    const string _numberSeed = "0123456789";
    const string _lowerLetterSeed = "abcdefghijklmnopqrstuvwxyz";
    const string _upperLetterSeed = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    const string _specialSeed = "~!@#$%^&*()_+{}:<>?.,/';\"[]\\|-=`";

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
        var code = new System.Random().GenerateCode(new GenerateRandomCodeOption { Seed = _numberSeed });
        Console.WriteLine(code);
        Assert.AreEqual(6, code.Length);
    }

    [TestMethod]
    public void OnlyLowerLetterGenerateCodeTest()
    {
        var code = new System.Random().GenerateCode(new GenerateRandomCodeOption { Seed = _lowerLetterSeed });
        Console.WriteLine(code);
        Assert.AreEqual(6, code.Length);
    }

    [TestMethod]
    public void OnlyUpperLetterGenerateCodeTest()
    {
        var code = new System.Random().GenerateCode(new GenerateRandomCodeOption { Seed = _upperLetterSeed });
        Console.WriteLine(code);
        Assert.AreEqual(6, code.Length);
    }

    [TestMethod]
    public void OnlySpecialSymbolGenerateCodeTest()
    {
        var code = new System.Random().GenerateCode(new GenerateRandomCodeOption { Seed = _specialSeed });
        Console.WriteLine(code);
        Assert.AreEqual(6, code.Length);
    }

    [TestMethod]
    public void MixGenerateCodeTest()
    {
        var code = new System.Random().GenerateCode();
        Console.WriteLine(code);
        Assert.AreEqual(6, code.Length);
    }

    [TestMethod]
    public void CustomGenerateCodeTest()
    {
        var code = new System.Random().GenerateCode(new GenerateRandomCodeOption { Seed = "123abc" });
        Console.WriteLine(code);
        Assert.AreEqual(6, code.Length);
    }

    [TestMethod]
    public void LengthGenerateCodeTest()
    {
        var code = new System.Random().GenerateCode(new GenerateRandomCodeOption { Length = 30 });
        Console.WriteLine(code);
        Assert.AreEqual(30, code.Length);
    }
}
