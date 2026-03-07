using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SharpDevLib.Tests.Basic.Model;

[TestClass]
public class DataRequestTests
{
    [TestMethod]
    public void DefaultValuesTest()
    {
        var request = new DataRequest<TestData>();
        Assert.IsNull(request.Data);
    }

    [TestMethod]
    public void WithDataTest()
    {
        var data = new TestData { Value = "test" };
        var request = new DataRequest<TestData>
        {
            Data = data
        };
        Assert.IsNotNull(request.Data);
        Assert.AreEqual("test", request.Data.Value);
    }

    [TestMethod]
    public void GenericTypeTest()
    {
        Assert.AreEqual(typeof(TestData), typeof(DataRequest<TestData>).GetGenericArguments()[0]);
    }
}

[TestClass]
public class IdRequestTests
{
    [TestMethod]
    public void DefaultValuesTest()
    {
        var request = new IdRequest<string>();
        Assert.IsNull(request.Id);
    }

    [TestMethod]
    public void WithIdTest()
    {
        var request = new IdRequest<string>
        {
            Id = "test-id"
        };
        Assert.AreEqual("test-id", request.Id);
    }

    [TestMethod]
    [DataRow("")]
    [DataRow(" ")]
    [DataRow("test-id-123")]
    public void VariousIdValuesTest(string id)
    {
        var request = new IdRequest<string> { Id = id };
        Assert.AreEqual(id, request.Id);
    }
}

[TestClass]
public class NameRequestTests
{
    [TestMethod]
    public void DefaultValuesTest()
    {
        var request = new NameRequest();
        Assert.IsNull(request.Name);
    }

    [TestMethod]
    public void WithNameTest()
    {
        var request = new NameRequest
        {
            Name = "TestName"
        };
        Assert.AreEqual("TestName", request.Name);
    }

    [TestMethod]
    [DataRow("")]
    [DataRow(" ")]
    [DataRow("Test Name")]
    [DataRow("测试名称")]
    public void VariousNameValuesTest(string name)
    {
        var request = new NameRequest { Name = name };
        Assert.AreEqual(name, request.Name);
    }
}

[TestClass]
public class IdNameRequestTests
{
    [TestMethod]
    public void DefaultValuesTest()
    {
        var request = new IdNameRequest<string>();
        Assert.IsNull(request.Id);
        Assert.IsNull(request.Name);
    }

    [TestMethod]
    public void WithIdAndNameTest()
    {
        var request = new IdNameRequest<string>
        {
            Id = "test-id",
            Name = "TestName"
        };
        Assert.AreEqual("test-id", request.Id);
        Assert.AreEqual("TestName", request.Name);
    }

    [TestMethod]
    public void OnlyIdTest()
    {
        var request = new IdNameRequest<string> { Id = "test-id" };
        Assert.AreEqual("test-id", request.Id);
        Assert.IsNull(request.Name);
    }

    [TestMethod]
    public void OnlyNameTest()
    {
        var request = new IdNameRequest<string> { Name = "TestName" };
        Assert.AreEqual("TestName", request.Name);
        Assert.IsNull(request.Id);
    }
}

[TestClass]
public class PageRequestTests
{
    [TestMethod]
    public void DefaultValuesTest()
    {
        var request = new PageRequest();
        Assert.AreEqual(0, request.Index);
        Assert.AreEqual(20, request.Size);
    }

    [TestMethod]
    public void WithCustomValuesTest()
    {
        var request = new PageRequest
        {
            Index = 5,
            Size = 20
        };
        Assert.AreEqual(5, request.Index);
        Assert.AreEqual(20, request.Size);
    }

    [TestMethod]
    [DataRow(0, 1)]
    [DataRow(1, 10)]
    [DataRow(10, 50)]
    [DataRow(100, 100)]
    public void VariousIndexAndSizeValuesTest(int index, int size)
    {
        var request = new PageRequest { Index = index, Size = size };
        Assert.AreEqual(index, request.Index);
        Assert.AreEqual(size, request.Size);
    }

    [TestMethod]
    public void ZeroSizeTest()
    {
        var request = new PageRequest { Size = 0 };
        Assert.AreEqual(0, request.Size);
    }
}

[TestClass]
public class IdDataRequestTests
{
    [TestMethod]
    public void DefaultValuesTest()
    {
        var request = new IdDataRequest<string, TestData>();
        Assert.IsNull(request.Id);
        Assert.IsNull(request.Data);
    }

    [TestMethod]
    public void WithIdAndDataTest()
    {
        var data = new TestData { Value = "test" };
        var request = new IdDataRequest<string, TestData>
        {
            Id = "test-id",
            Data = data
        };
        Assert.AreEqual("test-id", request.Id);
        Assert.IsNotNull(request.Data);
        Assert.AreEqual("test", request.Data.Value);
    }

    [TestMethod]
    public void OnlyIdTest()
    {
        var request = new IdDataRequest<string, TestData> { Id = "test-id" };
        Assert.AreEqual("test-id", request.Id);
        Assert.IsNull(request.Data);
    }

    [TestMethod]
    public void OnlyDataTest()
    {
        var data = new TestData { Value = "test" };
        var request = new IdDataRequest<string, TestData> { Data = data };
        Assert.IsNull(request.Id);
        Assert.IsNotNull(request.Data);
        Assert.AreEqual("test", request.Data.Value);
    }
}

[TestClass]
public class IdNameDataRequestTests
{
    [TestMethod]
    public void DefaultValuesTest()
    {
        var request = new IdNameDataRequest<string, TestData>();
        Assert.IsNull(request.Id);
        Assert.IsNull(request.Name);
        Assert.IsNull(request.Data);
    }

    [TestMethod]
    public void WithAllFieldsTest()
    {
        var data = new TestData { Value = "test" };
        var request = new IdNameDataRequest<string, TestData>
        {
            Id = "test-id",
            Name = "TestName",
            Data = data
        };
        Assert.AreEqual("test-id", request.Id);
        Assert.AreEqual("TestName", request.Name);
        Assert.IsNotNull(request.Data);
        Assert.AreEqual("test", request.Data.Value);
    }

    [TestMethod]
    public void PartialFieldsTest()
    {
        var request = new IdNameDataRequest<string, TestData>
        {
            Id = "test-id",
            Name = "TestName"
        };
        Assert.AreEqual("test-id", request.Id);
        Assert.AreEqual("TestName", request.Name);
        Assert.IsNull(request.Data);
    }
}

class TestData
{
    public string? Value { get; set; }
}
