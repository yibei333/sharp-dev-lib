using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace SharpDevLib.Tests.Basic.Helpers;

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
        var spaceName = typeof(ReflectionHelperTests).Namespace;
        var expected = $"{spaceName}.A<{spaceName}.B<System.Int32>, {spaceName}.C<System.String>>";
        Console.WriteLine(actual);
        Assert.AreEqual(expected, actual);
    }
}

class A<T1, T2> { }

class B<T> { }

class C<T> { }