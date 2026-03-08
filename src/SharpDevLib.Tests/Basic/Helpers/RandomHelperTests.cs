using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SharpDevLib.Tests.Basic.Helpers;

[TestClass]
public class RandomHelperTests
{
    [TestMethod]
    public void GenerateCodeWithZeroLengthTest()
    {
        var actual = RandomType.Number.GenerateCode(0);
        Assert.AreEqual(string.Empty, actual);
    }

    [TestMethod]
    public void GenerateCodeNumberTest()
    {
        for (int i = 0; i < 100; i++)
        {
            var actual = RandomType.Number.GenerateCode(10);
            Console.WriteLine(actual);
            Assert.AreEqual(10, actual.Length);
            Assert.IsTrue(actual.All(char.IsDigit));
        }
    }

    [TestMethod]
    public void GenerateCodeLetterLowerTest()
    {
        for (int i = 0; i < 100; i++)
        {
            var actual = RandomType.LetterLower.GenerateCode(10);
            Console.WriteLine(actual);
            Assert.AreEqual(10, actual.Length);
            Assert.IsTrue(actual.All(c => char.IsLetter(c) && char.IsLower(c)));
        }
    }

    [TestMethod]
    public void GenerateCodeLetterUpperTest()
    {
        for (int i = 0; i < 100; i++)
        {
            var actual = RandomType.LetterUpper.GenerateCode(10);
            Console.WriteLine(actual);
            Assert.AreEqual(10, actual.Length);
            Assert.IsTrue(actual.All(c => char.IsLetter(c) && char.IsUpper(c)));
        }
    }

    [TestMethod]
    public void GenerateCodeLetterTest()
    {
        for (int i = 0; i < 100; i++)
        {
            var actual = RandomType.Letter.GenerateCode(10);
            Console.WriteLine(actual);
            Assert.AreEqual(10, actual.Length);
            Assert.IsTrue(actual.All(char.IsLetter));
        }
    }

    [TestMethod]
    public void GenerateCodeNumberAndLetterTest()
    {
        for (int i = 0; i < 100; i++)
        {
            var actual = RandomType.NumberAndLetter.GenerateCode(10);
            Console.WriteLine(actual);
            Assert.AreEqual(10, actual.Length);
            Assert.IsTrue(actual.All(c => char.IsLetterOrDigit(c)));
        }
    }

    [TestMethod]
    public void GenerateCodeMixTest()
    {
        for (int i = 0; i < 100; i++)
        {
            var actual = RandomType.Mix.GenerateCode(10);
            Console.WriteLine(actual);
            Assert.AreEqual(10, actual.Length);
            Assert.IsTrue(actual.All(c => char.IsLetterOrDigit(c) || "!@#$%^&*()_+-=[]{}|;:,.<>?".Contains(c)));
        }
    }

    [TestMethod]
    public void GenerateCodeNumberCharacterDistributionTest()
    {
        var chars = new char[10000];
        for (int i = 0; i < 10000; i++)
        {
            chars[i] = RandomType.Number.GenerateCode(1)[0];
        }
        var distinctChars = chars.Distinct().ToArray();
        Console.WriteLine($"Distinct characters: {string.Join(", ", distinctChars)}");
        Assert.HasCount(10, distinctChars);
        CollectionAssert.AreEquivalent("0123456789".ToArray(), distinctChars);
    }

    [TestMethod]
    public void GenerateCodeLetterLowerCharacterDistributionTest()
    {
        var chars = new char[10000];
        for (int i = 0; i < 10000; i++)
        {
            chars[i] = RandomType.LetterLower.GenerateCode(1)[0];
        }
        var distinctChars = chars.Distinct().ToArray();
        Console.WriteLine($"Distinct characters: {string.Join(", ", distinctChars)}");
        Assert.HasCount(26, distinctChars);
        CollectionAssert.AreEquivalent("abcdefghijklmnopqrstuvwxyz".ToArray(), distinctChars);
    }

    [TestMethod]
    public void GenerateCodeLetterUpperCharacterDistributionTest()
    {
        var chars = new char[10000];
        for (int i = 0; i < 10000; i++)
        {
            chars[i] = RandomType.LetterUpper.GenerateCode(1)[0];
        }
        var distinctChars = chars.Distinct().ToArray();
        Console.WriteLine($"Distinct characters: {string.Join(", ", distinctChars)}");
        Assert.HasCount(26, distinctChars);
        CollectionAssert.AreEquivalent("ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToArray(), distinctChars);
    }

    [TestMethod]
    public void GenerateCodeUniquenessTest()
    {
        var codes = new string[1000];
        for (int i = 0; i < 1000; i++)
        {
            codes[i] = RandomType.NumberAndLetter.GenerateCode(20);
        }
        var distinctCodes = codes.Distinct().ToArray();
        Console.WriteLine($"Generated {codes.Length} codes, {distinctCodes.Length} unique");
        Assert.HasCount(1000, distinctCodes);
    }

    [TestMethod]
    [DataRow(1)]
    [DataRow(5)]
    [DataRow(10)]
    [DataRow(50)]
    [DataRow(100)]
    [DataRow(255)]
    public void GenerateCodeVariableLengthTest(int length)
    {
        var actual = RandomType.NumberAndLetter.GenerateCode((byte)length);
        Console.WriteLine(actual);
        Assert.AreEqual(length, actual.Length);
    }

    [TestMethod]
    public void GenerateCodeSpecialCharactersTest()
    {
        var specialChars = "!@#$%^&*()_+-=[]{}|;:,.<>?";
        var hasSpecialChar = false;
        for (int i = 0; i < 1000; i++)
        {
            var actual = RandomType.Mix.GenerateCode(10);
            if (actual.Any(c => specialChars.Contains(c)))
            {
                hasSpecialChar = true;
                break;
            }
        }
        Console.WriteLine($"Found special character: {hasSpecialChar}");
        Assert.IsTrue(hasSpecialChar, "Expected to find at least one special character in 1000 attempts");
    }

    [TestMethod]
    public void GenerateCodeCryptographicRandomnessTest()
    {
        var codes = new HashSet<string>();
        for (int i = 0; i < 10000; i++)
        {
            var code = RandomType.NumberAndLetter.GenerateCode(16);
            Assert.DoesNotContain(code, codes, $"Duplicate code found: {code}");
            codes.Add(code);
        }
        Assert.HasCount(10000, codes);
    }
}
