using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpDevLib.Standard;
using SharpDevLib.Tests.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SharpDevLib.Tests.Standard.Extensions;

[TestClass]
public class TreeExtensionTests
{
    [TestMethod]
    public void Test()
    {
        var users = new List<User>
        {
            new User("foo",10),
            new User("bar",20),
        };

        var a = users.OrderByDynamic("Age", false).ToList();
        //var a = users.OrderBy(x => x.Name).ToList();
        Console.WriteLine(a.Serialize());
    }
}
