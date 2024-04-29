using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpDevLib.Tests.Data;
using System;

namespace SharpDevLib.Tests;

[TestClass]
public class ReflectionUtilTests
{
    [TestMethod]
    public void EnsureTypeContainsPublicParamterLessConstructorTest()
    {
        typeof(ToDoItem).EnsureTypeContainsPublicParamterLessConstructor();
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void EnsureTypeContainsPublicParamterLessConstructorArgumentNullExceptionTest()
    {
        Type? type = null;
        type.EnsureTypeContainsPublicParamterLessConstructor();
    }

    [TestMethod]
    [ExpectedException(typeof(MissingMethodException))]
    public void EnsureTypeContainsPublicParamterLessConstructorMissingMethodExceptionTest1()
    {
        typeof(User).EnsureTypeContainsPublicParamterLessConstructor();
    }

    [TestMethod]
    [ExpectedException(typeof(MissingMethodException))]
    public void EnsureTypeContainsPublicParamterLessConstructorMissingMethodExceptionTest2()
    {
        typeof(Department).EnsureTypeContainsPublicParamterLessConstructor();
    }

    [TestMethod]
    public void GetObjectTypeNameTest()
    {
        var deparment = Department.Create();
        Assert.AreEqual("SharpDevLib.Tests.Data.Department",deparment.GetTypeName());
        Assert.AreEqual("SharpDevLib.Tests.Data.Department",deparment.GetTypeName(true));
        Assert.AreEqual("Department",deparment.GetTypeName(false));

    }

    [TestMethod]
    [ExpectedException(typeof(NullReferenceException))]
    public void GetObjectTypeNameNullReferenceExceptionTest()
    {
        Department? a = null;
        _ = a.GetTypeName();
    }

    [TestMethod]
    public void GetTypeNameTest()
    {
        var type = typeof(Department);
        Assert.AreEqual("SharpDevLib.Tests.Data.Department", type.GetTypeName());
        Assert.AreEqual("SharpDevLib.Tests.Data.Department", type.GetTypeName(true));
        Assert.AreEqual("Department", type.GetTypeName(false));
    }

    [TestMethod]
    [ExpectedException(typeof(NullReferenceException))]
    public void GetTypeNameNullReferenceExceptionTest()
    {
        Type? type = null;
        _ = type.GetTypeName();
    }
}
