using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpDevLib.Tests.TestData;
using System;
using System.IO;

namespace SharpDevLib.Tests.Standard.Extensions;

[TestClass]
public class EnumExtensionTests
{
    const string _json = "[{\"Key\":\"Male\",\"Value\":1},{\"Key\":\"Female\",\"Value\":2},{\"Key\":\"Other\",\"Value\":3},{\"Key\":\"SomeVeryLongLongLongLongLongLongValue\",\"Value\":4}]";
    const string _dicJson = "{\"Male\":1,\"Female\":2,\"Other\":3,\"SomeVeryLongLongLongLongLongLongValue\":4}";

    [TestMethod]
    [DataRow(1, Gender.Male)]
    [DataRow(2, Gender.Female)]
    public void IntToEnumTest(int intValue, Gender expected)
    {
        Assert.AreEqual(expected, intValue.ToEnum<Gender>());
    }

    [TestMethod]
    [DataRow(0)]
    [DataRow(5)]
    public void IntToEnumExceptionTest(int intValue)
    {
        Assert.ThrowsExactly<InvalidDataException>(() => intValue.ToEnum<Gender>());
    }

    [TestMethod]
    [DataRow("Male", Gender.Male)]
    [DataRow("male", Gender.Male)]
    [DataRow("Female", Gender.Female)]
    public void StringToEnumTest(string stringValue, Gender expected)
    {
        Assert.AreEqual(expected, stringValue.ToEnum<Gender>());
    }

    [TestMethod]
    [DataRow("")]
    [DataRow(" ")]
    [DataRow("male")]
    [DataRow("Male1")]
    public void StringToEnumExceptionTest(string stringValue)
    {
        Assert.ThrowsExactly<InvalidDataException>(() => stringValue.ToEnum<Gender>(false));
    }

    [TestMethod]
    public void GetDictionaryTest()
    {
        var actual = EnumExtension.GetDictionary<Gender>().Serialize();
        Console.WriteLine(actual);
        Assert.AreEqual(_dicJson, actual);
    }

    [TestMethod]
    public void GetKeyValuesTest()
    {
        var actual = EnumExtension.GetKeyValues<Gender>().Serialize();
        Assert.AreEqual(_json, actual);
    }
}
