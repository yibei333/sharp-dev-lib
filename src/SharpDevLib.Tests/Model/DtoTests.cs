using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SharpDevLib.Tests.Model;

[TestClass]
public class IdDtoTests
{
    [TestMethod]
    public void WithIdTest()
    {
        var dto = new IdDto<string>
        {
            Id = "test-id"
        };

        Assert.AreEqual("test-id", dto.Id);
    }

    [TestMethod]
    [DataRow("")]
    [DataRow(" ")]
    [DataRow("test-id-123")]
    [DataRow("ABC123")]
    public void VariousIdValuesTest(string id)
    {
        var dto = new IdDto<string> { Id = id };
        Assert.AreEqual(id, dto.Id);
    }

    [TestMethod]
    public void ConstructorTest()
    {
        var dto = new IdDto<string>("test-id");
        Assert.AreEqual("test-id", dto.Id);
    }
}

[TestClass]
public class NameDtoTests
{
    [TestMethod]
    public void DefaultValuesTest()
    {
        var dto = new NameDto();
        Assert.IsNull(dto.Name);
    }

    [TestMethod]
    public void WithNameTest()
    {
        var dto = new NameDto
        {
            Name = "TestName"
        };
        Assert.AreEqual("TestName", dto.Name);
    }

    [TestMethod]
    [DataRow("")]
    [DataRow(" ")]
    [DataRow("Test Name")]
    [DataRow("测试名称")]
    [DataRow("Name123")]
    public void VariousNameValuesTest(string name)
    {
        var dto = new NameDto { Name = name };
        Assert.AreEqual(name, dto.Name);
    }

    [TestMethod]
    public void ConstructorTest()
    {
        var dto = new NameDto("test-name");
        Assert.AreEqual("test-name", dto.Name);
    }

    [TestMethod]
    public void ToStringTest()
    {
        var dto = new NameDto("TestName");
        var result = dto.Serialize();
        Assert.IsNotNull(result);
        Assert.Contains("TestName", result);
    }
}

[TestClass]
public class IdNameDtoTests
{
    [TestMethod]
    public void DefaultValuesTest()
    {
        var dto = new IdNameDto<string>();
        Assert.IsNull(dto.Id);
        Assert.IsNull(dto.Name);
    }

    [TestMethod]
    public void WithIdAndNameTest()
    {
        var dto = new IdNameDto<string>
        {
            Id = "test-id",
            Name = "TestName"
        };

        Assert.AreEqual("test-id", dto.Id);
        Assert.AreEqual("TestName", dto.Name);
    }

    [TestMethod]
    public void OnlyIdTest()
    {
        var dto = new IdNameDto<string> { Id = "test-id" };
        Assert.AreEqual("test-id", dto.Id);
        Assert.IsNull(dto.Name);
    }

    [TestMethod]
    public void OnlyNameTest()
    {
        var dto = new IdNameDto<string> { Name = "TestName" };
        Assert.AreEqual("TestName", dto.Name);
        Assert.IsNull(dto.Id);
    }

    [TestMethod]
    public void ConstructorWithIdAndNameTest()
    {
        var dto = new IdNameDto<string>("test-id", "test-name");
        Assert.AreEqual("test-id", dto.Id);
        Assert.AreEqual("test-name", dto.Name);
    }

    [TestMethod]
    public void ToStringTest()
    {
        var dto = new IdNameDto<string>("test-id", "TestName");
        var result = dto.Serialize();
        Assert.IsNotNull(result);
        Assert.Contains("test-id", result);
        Assert.Contains("TestName", result);
    }
}

[TestClass]
public class DataDtoTests
{
    [TestMethod]
    public void DefaultValuesTest()
    {
        var dto = new DataDto<TestData>();
        Assert.IsNull(dto.Data);
    }

    [TestMethod]
    public void WithDataTest()
    {
        var data = new TestData { Value = "test" };
        var dto = new DataDto<TestData>
        {
            Data = data
        };
        Assert.IsNotNull(dto.Data);
        Assert.AreEqual("test", dto.Data.Value);
    }

    [TestMethod]
    public void ConstructorWithDataTest()
    {
        var data = new TestData { Value = "test" };
        var dto = new DataDto<TestData>(data);
        Assert.IsNotNull(dto.Data);
        Assert.AreEqual("test", dto.Data.Value);
    }

    [TestMethod]
    public void GenericTypeTest()
    {
        Assert.AreEqual(typeof(TestData), typeof(DataDto<TestData>).GetGenericArguments()[0]);
    }
}

[TestClass]
public class IdDataDtoTests
{
    [TestMethod]
    public void DefaultValuesTest()
    {
        var dto = new IdDataDto<string, TestData>();
        Assert.IsNull(dto.Id);
        Assert.IsNull(dto.Data);
    }

    [TestMethod]
    public void WithIdAndDataTest()
    {
        var data = new TestData { Value = "test" };
        var dto = new IdDataDto<string, TestData>
        {
            Id = "test-id",
            Data = data
        };
        Assert.AreEqual("test-id", dto.Id);
        Assert.IsNotNull(dto.Data);
        Assert.AreEqual("test", dto.Data.Value);
    }

    [TestMethod]
    public void OnlyIdTest()
    {
        var dto = new IdDataDto<string, TestData> { Id = "test-id" };
        Assert.AreEqual("test-id", dto.Id);
        Assert.IsNull(dto.Data);
    }

    [TestMethod]
    public void OnlyDataTest()
    {
        var data = new TestData { Value = "test" };
        var dto = new IdDataDto<string, TestData> { Data = data };
        Assert.IsNull(dto.Id);
        Assert.IsNotNull(dto.Data);
        Assert.AreEqual("test", dto.Data.Value);
    }

    [TestMethod]
    public void ConstructorWithIdAndDataTest()
    {
        var data = new TestData { Value = "test" };
        var dto = new IdDataDto<string, TestData>("test-id", data);
        Assert.AreEqual("test-id", dto.Id);
        Assert.IsNotNull(dto.Data);
        Assert.AreEqual("test", dto.Data.Value);
    }
}

[TestClass]
public class IdNameDataDtoTests
{
    [TestMethod]
    public void DefaultValuesTest()
    {
        var dto = new IdNameDataDto<string, TestData>();
        Assert.IsNull(dto.Id);
        Assert.IsNull(dto.Name);
        Assert.IsNull(dto.Data);
    }

    [TestMethod]
    public void WithAllFieldsTest()
    {
        var data = new TestData { Value = "test" };
        var dto = new IdNameDataDto<string, TestData>
        {
            Id = "test-id",
            Name = "TestName",
            Data = data
        };
        Assert.AreEqual("test-id", dto.Id);
        Assert.AreEqual("TestName", dto.Name);
        Assert.IsNotNull(dto.Data);
        Assert.AreEqual("test", dto.Data.Value);
    }

    [TestMethod]
    public void PartialFieldsTest()
    {
        var dto = new IdNameDataDto<string, TestData>
        {
            Id = "test-id",
            Name = "TestName"
        };
        Assert.AreEqual("test-id", dto.Id);
        Assert.AreEqual("TestName", dto.Name);
        Assert.IsNull(dto.Data);
    }

    [TestMethod]
    public void ConstructorWithAllFieldsTest()
    {
        var data = new TestData { Value = "test" };
        var dto = new IdNameDataDto<string, TestData>("test-id", "test-name", data);
        Assert.AreEqual("test-id", dto.Id);
        Assert.AreEqual("test-name", dto.Name);
        Assert.IsNotNull(dto.Data);
        Assert.AreEqual("test", dto.Data.Value);
    }

    [TestMethod]
    public void ConstructorWithIdAndNameTest()
    {
        var dto = new IdNameDataDto<string, TestData>("test-id", "test-name");
        Assert.AreEqual("test-id", dto.Id);
        Assert.AreEqual("test-name", dto.Name);
        Assert.IsNull(dto.Data);
    }
}
