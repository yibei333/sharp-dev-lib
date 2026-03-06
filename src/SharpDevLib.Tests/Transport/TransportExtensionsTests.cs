using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpDevLib.Transport;

namespace SharpDevLib.Tests.Transport;

[TestClass]
public class TransportExtensionsTests
{
    [TestMethod]
    public void GetAvailbelTcpTest()
    {
        var port = TcpHelper.GetAvailableTcpPort(50, 100);
        Assert.IsGreaterThanOrEqualTo(50, port);
        Assert.IsLessThanOrEqualTo(100, port);
    }

    [TestMethod]
    public void GetAvailbelUdpTest()
    {
        var port = UdpHelper.GetAvailableUdpPort(50, 100);
        Assert.IsGreaterThanOrEqualTo(50, port);
        Assert.IsLessThanOrEqualTo(100, port);
    }
}
