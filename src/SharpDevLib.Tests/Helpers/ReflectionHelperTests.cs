using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace SharpDevLib.Tests.Helpers;

[TestClass]
public class ReflectionHelperTests
{
    [TestMethod]
    public void GetTypeDefinitionNameTest()
    {
        var type = typeof(A<B<int>, C<string>>);
        var actual = type.GetTypeDefinitionName();
        var expected = "A<B<Int32>, C<String>>";
        Console.WriteLine(actual);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void GetTypeDefinitionFullNameTest()
    {
        var type = typeof(A<B<int>, C<string>>);
        var actual = type.GetTypeDefinitionName(true);
        var expected = "SharpDevLib.Tests.Helpers.A<SharpDevLib.Tests.Helpers.B<System.Int32>, SharpDevLib.Tests.Helpers.C<System.String>>";
        Console.WriteLine(actual);
        Assert.AreEqual(expected, actual);
    }
}

class A<T1, T2> { }

class B<T> { }

class C<T> { }