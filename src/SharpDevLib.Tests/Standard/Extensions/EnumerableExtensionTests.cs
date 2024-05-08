using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpDevLib.Standard;
using SharpDevLib.Tests.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SharpDevLib.Tests.Standard.Extensions;

[TestClass]
public class EnumerableExtensionTests
{
    [TestMethod]
    public void DistinctByObjectValueTest()
    {
        List<User?> users = new()
        {
            null,
            new ("foo",10),
            new ("foo",10),
            new ("foo",20),
            new ("bar",20),
            null
        };
        var distinctedUsers = users.DistinctByObjectValue().ToList();
        Console.WriteLine(distinctedUsers.Serialize());
        Assert.AreEqual(4, distinctedUsers?.Count);
    }
}