using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpDevLib.Tests.Data;
using System;

namespace SharpDevLib.Tests;

[TestClass]
public class CloneUtilTests
{
    [TestMethod]
    public void DeepCloneTest()
    {
        var department = Department.Create();
        var clone = department.DeepClone();
        Assert.AreNotEqual(department, clone);
        Assert.AreEqual(department.Serialize(), clone.Serialize());
        Console.WriteLine(department.Serialize());
        Console.WriteLine(clone.Serialize());
    }

    [TestMethod]
    [DataRow(null)]
    [ExpectedException(typeof(ArgumentNullException))]
    public void DeepCloneExceptionTest(Department department)
    {
        _ = department.DeepClone();
    }
}
