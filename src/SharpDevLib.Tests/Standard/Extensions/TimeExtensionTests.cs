using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace SharpDevLib.Tests.Standard.Extensions;

[TestClass]
public class TimeExtensionTests
{
    [TestMethod]
    [DataRow(2022, 1, 1, 1640995200000)]
    [DataRow(2022, 2, 1, 1643673600000)]
    public void ToUtcTimestampTest(int year, int month, int day, long expected)
    {
        var time = new DateTime(year, month, day, 0, 0, 0, DateTimeKind.Utc);
        Assert.AreEqual(expected, time.ToUtcTimestamp());
    }

    [TestMethod]
    [DataRow(1640995200000, 2022, 1, 1)]
    [DataRow(1643673600000, 2022, 2, 1)]
    public void ToUtcTimeTest(long timestamp, int year, int month, int day)
    {
        var expected = new DateTime(year, month, day, 0, 0, 0, DateTimeKind.Utc);
        var actual = timestamp.ToUtcTime();
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    [DataRow(2022, 1, 1, "yyyy-MM-dd HH:mm:ss.fff", "2022-01-01 00:00:00.000")]
    [DataRow(2022, 1, 1, "yyyy-MM-dd HH:mm:ss", "2022-01-01 00:00:00")]
    [DataRow(2022, 1, 1, "yyyy-MM-dd", "2022-01-01")]
    [DataRow(2022, 2, 1, "yyyy-MM-dd HH:mm:ss", "2022-02-01 00:00:00")]
    [DataRow(2022, 2, 1, "yyyy-MM-dd", "2022-02-01")]
    public void ToTimeStringTest(int year, int month, int day, string format, string expected)
    {
        var actual = new DateTime(year, month, day, 0, 0, 0, DateTimeKind.Utc).ToTimeString(format);
        Assert.AreEqual(expected, actual);
    }
}
