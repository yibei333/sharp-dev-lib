using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpDevLib.Tests.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SharpDevLib.Tests;

[TestClass]
public class TreeUtilTests
{
    [TestMethod]
    public void BuildTreeTest()
    {
        List<Menu>? menus = null;
        Assert.AreEqual(0, menus.BuildTree().Count);

        menus = new List<Menu> {
            new Menu("Root",Guid.Parse("00000000-0000-0000-0000-100000000000"),null,1),
            new Menu("Home",Guid.Parse("00000000-0000-0000-0000-100000000010"),Guid.Parse("00000000-0000-0000-0000-100000000000"),1),
            new Menu("Home_Add",Guid.Parse("00000000-0000-0000-0000-100000000011"),Guid.Parse("00000000-0000-0000-0000-100000000010"),1),
            new Menu("Home_Update",Guid.Parse("00000000-0000-0000-0000-100000000012"),Guid.Parse("00000000-0000-0000-0000-100000000010"),2),
            new Menu("Home_Remove",Guid.Parse("00000000-0000-0000-0000-100000000013"),Guid.Parse("00000000-0000-0000-0000-100000000010"),3),
            new Menu("Home_Query",Guid.Parse("00000000-0000-0000-0000-100000000014"),Guid.Parse("00000000-0000-0000-0000-100000000010"),4),
            new Menu("User",Guid.Parse("00000000-0000-0000-0000-100000000020"),Guid.Parse("00000000-0000-0000-0000-100000000000"),1),
            new Menu("User_Add",Guid.Parse("00000000-0000-0000-0000-100000000021"),Guid.Parse("00000000-0000-0000-0000-100000000020"),1),
            new Menu("User_Update",Guid.Parse("00000000-0000-0000-0000-100000000022"),Guid.Parse("00000000-0000-0000-0000-100000000020"),2),
            new Menu("User_Remove",Guid.Parse("00000000-0000-0000-0000-100000000023"),Guid.Parse("00000000-0000-0000-0000-100000000020"),3),
            new Menu("User_Query",Guid.Parse("00000000-0000-0000-0000-100000000024"),Guid.Parse("00000000-0000-0000-0000-100000000020"),4),
            new Menu("User_Query_All",Guid.Parse("00000000-0000-0000-0000-100000000025"),Guid.Parse("00000000-0000-0000-0000-100000000024"),1),
        };
        var tree = menus.BuildTree();
        Assert.AreEqual(1, tree.Count);
        Assert.AreEqual(2, tree.First().Children.Count);
        Assert.AreEqual(4, tree.First().Children.First().Children.Count);
        Assert.AreEqual(4, tree.First().Children.Last().Children.Count);
        Assert.AreEqual(0, tree.First().Children.Last().Children.First().Children.Count);
        Assert.AreEqual(1, tree.First().Children.Last().Children.Last().Children.Count);
        Console.WriteLine(tree.Serialize().FormatJson());
    }
}
