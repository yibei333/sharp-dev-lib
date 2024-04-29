using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpDevLib.Tests.Data;
using System.Collections.Generic;
using System.Linq;

namespace SharpDevLib.Tests;

[TestClass]
public class EnumerableUtilTests
{
    [TestMethod]
    public void DistinctObjectTest()
    {
        var single = Department.Create();
        var list = Department.CreateList();
        var data = new List<Department>();
        Assert.AreEqual(0, data.DistinctObject().Count());

        data.Add(null!);
        Assert.AreEqual(1, data.DistinctObject().Count());
        data.Add(single);
        Assert.AreEqual(2, data.DistinctObject().Count());
        data.Add(null!);
        Assert.AreEqual(2, data.DistinctObject().Count());

        data.AddRange(list);
        Assert.AreEqual(4, data.DistinctObject().Count());

        data.AddRange(list);
        Assert.AreEqual(4, data.DistinctObject().Count());
        
        data.Add(single);
        Assert.AreEqual(4, data.DistinctObject().Count());

        data.Add(single);
        Assert.AreEqual(4, data.DistinctObject().Count());

        data.Add(null!);
        Assert.AreEqual(4, data.DistinctObject().Count());

        data.AddRange(Department.CreateList());
        Assert.AreEqual(6, data.DistinctObject().Count());
        Assert.AreEqual(4, data.DistinctBy(x=>x?.Name).Count());
    }
}
