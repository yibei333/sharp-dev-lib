using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpDevLib.Standard;
using System;

namespace SharpDevLib.Tests.Standard.Extensions;

[TestClass]
public class ReflectionExtensionTests
{
    [TestMethod]
    public void GetTypeNameTest()
    {
        var type = typeof(A<B<int>, C<string>>);
        var actual = type.GetTypeName();
        var expected = "A<B<Int32>,C<String>>";
        Console.WriteLine(actual);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void GetTypeFullNameTest()
    {
        var type = typeof(A<B<int>, C<string>>);
        var actual = type.GetTypeName(true);
        var expected = "SharpDevLib.Tests.Standard.Extensions.A<SharpDevLib.Tests.Standard.Extensions.B<System.Int32>,SharpDevLib.Tests.Standard.Extensions.C<System.String>>";
        Console.WriteLine(actual);
        Assert.AreEqual(expected, actual);
    }
}

class A<T1, T2> { }

class B<T> { }

class C<T> { }