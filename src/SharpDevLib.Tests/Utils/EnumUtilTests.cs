using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SharpDevLib.Tests;

[TestClass]
public class EnumUtilTests
{
    [TestMethod]
    [DataRow(0, true)]
    [DataRow(6, true)]
    [DataRow(-1, false)]
    [DataRow(7, false)]
    public void IsEnumTest(int value, bool expected)
    {
        var day = (DayOfWeek)value;
        Assert.AreEqual(expected, EnumUtil.IsEnum<DayOfWeek>(day));
    }

    [TestMethod]
    [DataRow(0, "Sunday")]
    [DataRow(6, "Saturday")]
    [DataRow(7, "")]
    [DataRow(-1, "")]
    public void GetStringTest(int value, string expected)
    {
        var actual = ((DayOfWeek)value).GetString();
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    [DataRow(0, 0)]
    [DataRow(6, 6)]
    public void GetIntTest(int value, int expected)
    {
        var actual = ((DayOfWeek)value).GetInt();
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    [DataRow(-1)]
    [DataRow(7)]
    [ExpectedException(typeof(ArgumentException))]
    public void GetIntExceptionTest(int value)
    {
        _ = ((DayOfWeek)value).GetInt();
    }

    [TestMethod]
    public void GetDictionaryTest()
    {
        var expected = new Dictionary<string, int>
        {
            {"Sunday",0 },
            {"Monday",1 },
            {"Tuesday",2 },
            {"Wednesday",3 },
            {"Thursday",4 },
            {"Friday",5 },
            {"Saturday",6 }
        };
        var actual = EnumUtil.GetDictionary<DayOfWeek>();
        Assert.AreEqual(expected.Serialize(), actual.Serialize());
    }

    [TestMethod]
    public void GetKeyValuesTest()
    {
        var expected = new List<KeyValuePair<string, int>>
        {
           new KeyValuePair<string, int>("Sunday",0),
           new KeyValuePair<string, int>("Monday",1),
           new KeyValuePair<string, int>("Tuesday",2),
           new KeyValuePair<string, int>("Wednesday",3),
           new KeyValuePair<string, int>("Thursday",4),
           new KeyValuePair<string, int>("Friday",5),
           new KeyValuePair<string, int>("Saturday",6)
        };
        var actual = EnumUtil.GetKeyValues<DayOfWeek>().ToList();
        Assert.AreEqual(expected.Serialize(), actual.Serialize());
    }
}
