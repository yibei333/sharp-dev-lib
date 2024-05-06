using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpDevLib.Standard;
using SharpDevLib.Tests.Data;
using System;

namespace SharpDevLib.Tests.Standard.Extensions;

[TestClass]
public class CloneExtensionTests
{
    static readonly User _user = new("foo", 10);
    static readonly string _userString = _user.ToString();

    [TestMethod]
    public void CloneTest()
    {
        Assert.IsNull(((User?)null).DeepClone(false));

        var obj = _user.DeepClone();
        Assert.AreNotEqual(obj, _user);
        Assert.AreEqual(_userString, obj?.ToString());
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void CloneExceptionTest()
    {
        ((User?)null).DeepClone(true);
    }
}
