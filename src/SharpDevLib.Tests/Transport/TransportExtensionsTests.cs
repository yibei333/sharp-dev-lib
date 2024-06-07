using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpDevLib.Transport;

namespace SharpDevLib.Tests.Transport;

[TestClass]
public class TransportExtensionsTests
{
    [TestMethod]
    public void GetAvailbelTcpTest()
    {
        var port = TransportExtensions.GetAvailableTcpPort(50, 100);
        Assert.IsTrue(port >= 50);
        Assert.IsTrue(port <= 100);
    }

    [TestMethod]
    public void GetAvailbelUdpTest()
    {
        var port = TransportExtensions.GetAvailableUdpPort(50, 100);
        Assert.IsTrue(port >= 50);
        Assert.IsTrue(port <= 100);
    }
}
