using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpDevLib.Tests.TestData;
using System.Collections.Generic;

namespace SharpDevLib.Tests.Model;

[TestClass]
public class EmptyReplyTests
{
    [TestMethod]
    public void DefaultValuesTest()
    {
        var reply = new EmptyReply();
        Assert.IsFalse(reply.Success);
        Assert.IsNull(reply.Description);
    }

    [TestMethod]
    public void SucceedTest()
    {
        var reply = EmptyReply.Succeed("操作成功");
        Assert.IsTrue(reply.Success);
        Assert.AreEqual("操作成功", reply.Description);
    }

    [TestMethod]
    public void SucceedWithoutDescriptionTest()
    {
        var reply = EmptyReply.Succeed();
        Assert.IsTrue(reply.Success);
        Assert.IsNull(reply.Description);
    }

    [TestMethod]
    public void FailedTest()
    {
        var reply = EmptyReply.Failed("操作失败");
        Assert.IsFalse(reply.Success);
        Assert.AreEqual("操作失败", reply.Description);
    }

    [TestMethod]
    public void FailedWithoutDescriptionTest()
    {
        var reply = EmptyReply.Failed();
        Assert.IsFalse(reply.Success);
        Assert.IsNull(reply.Description);
    }

    [TestMethod]
    public void WithCustomValuesTest()
    {
        var reply = new EmptyReply
        {
            Success = true,
            Description = "自定义描述"
        };
        Assert.IsTrue(reply.Success);
        Assert.AreEqual("自定义描述", reply.Description);
    }
}

[TestClass]
public class DataReplyTests
{
    [TestMethod]
    public void DefaultValuesTest()
    {
        var reply = new DataReply<User>();
        Assert.IsFalse(reply.Success);
        Assert.IsNull(reply.Description);
        Assert.IsNull(reply.Data);
    }

    [TestMethod]
    public void SucceedWithDataTest()
    {
        var user = new User("Alice", 30);
        var reply = DataReply.Succeed(user, "获取成功");
        Assert.IsTrue(reply.Success);
        Assert.AreEqual("获取成功", reply.Description);
        Assert.IsNotNull(reply.Data);
        Assert.AreEqual("Alice", reply.Data.Name);
        Assert.AreEqual(30, reply.Data.Age);
    }

    [TestMethod]
    public void SucceedWithoutDescriptionTest()
    {
        var user = new User("Bob", 25);
        var reply = DataReply.Succeed(user);
        Assert.IsTrue(reply.Success);
        Assert.IsNull(reply.Description);
        Assert.IsNotNull(reply.Data);
        Assert.AreEqual("Bob", reply.Data.Name);
        Assert.AreEqual(25, reply.Data.Age);
    }

    [TestMethod]
    public void FailedTest()
    {
        var reply = DataReply.Failed<User>("获取失败");
        Assert.IsFalse(reply.Success);
        Assert.AreEqual("获取失败", reply.Description);
        Assert.IsNull(reply.Data);
    }

    [TestMethod]
    public void FailedWithoutDescriptionTest()
    {
        var reply = DataReply.Failed<User>();
        Assert.IsFalse(reply.Success);
        Assert.IsNull(reply.Description);
        Assert.IsNull(reply.Data);
    }

    [TestMethod]
    public void WithCustomValuesTest()
    {
        var user = new User("Charlie", 35);
        var reply = new DataReply<User>
        {
            Success = true,
            Description = "自定义描述",
            Data = user
        };
        Assert.IsTrue(reply.Success);
        Assert.AreEqual("自定义描述", reply.Description);
        Assert.IsNotNull(reply.Data);
        Assert.AreEqual("Charlie", reply.Data.Name);
        Assert.AreEqual(35, reply.Data.Age);
    }

    [TestMethod]
    public void GenericTypeTest()
    {
        Assert.AreEqual(typeof(User), typeof(DataReply<User>).GetGenericArguments()[0]);
        Assert.AreEqual(typeof(string), typeof(DataReply<string>).GetGenericArguments()[0]);
    }
}

[TestClass]
public class PageReplyTests
{
    [TestMethod]
    public void DefaultValuesTest()
    {
        var reply = new PageReply<User>();
        Assert.IsFalse(reply.Success);
        Assert.IsNull(reply.Description);
        Assert.IsNull(reply.Data);
        Assert.AreEqual(0, reply.TotalCount);
        Assert.AreEqual(0, reply.Index);
        Assert.AreEqual(0, reply.Size);
    }

    [TestMethod]
    public void SucceedWithDataTest()
    {
        var data = new List<User>
        {
            new("Alice", 30),
            new("Bob", 25),
            new("Charlie", 35)
        };
        var request = new GetUsersRequest { Index = 0, Size = 10 };
        var reply = PageReply.Succeed<User>(data, 100, request, "获取成功");
        Assert.IsTrue(reply.Success);
        Assert.AreEqual("获取成功", reply.Description);
        Assert.IsNotNull(reply.Data);
        Assert.HasCount(3, reply.Data);
        Assert.AreEqual(100, reply.TotalCount);
        Assert.AreEqual(0, reply.Index);
        Assert.AreEqual(10, reply.Size);
    }

    [TestMethod]
    public void SucceedWithoutDescriptionTest()
    {
        var data = new List<User> { new("Alice", 30) };
        var request = new GetUsersRequest { Index = 0, Size = 10 };
        var reply = PageReply.Succeed<User>(data, 10, request);
        Assert.IsTrue(reply.Success);
        Assert.IsNull(reply.Description);
        Assert.IsNotNull(reply.Data);
        Assert.HasCount(1, reply.Data);
        Assert.AreEqual(10, reply.TotalCount);
    }

    [TestMethod]
    public void SucceedWithEmptyDataTest()
    {
        var data = new List<User>();
        var request = new GetUsersRequest { Index = 0, Size = 10 };
        var reply = PageReply.Succeed(data, 0, request);
        Assert.IsTrue(reply.Success);
        Assert.AreEqual(0, reply.Data?.Count);
        Assert.AreEqual(0, reply.TotalCount);
    }

    [TestMethod]
    public void FailedTest()
    {
        var reply = PageReply.Failed<User>("查询失败");
        Assert.IsFalse(reply.Success);
        Assert.AreEqual("查询失败", reply.Description);
        Assert.IsNull(reply.Data);
    }

    [TestMethod]
    public void FailedWithoutDescriptionTest()
    {
        var reply = PageReply.Failed<User>();
        Assert.IsFalse(reply.Success);
        Assert.IsNull(reply.Description);
        Assert.IsNull(reply.Data);
    }

    [TestMethod]
    public void WithCustomValuesTest()
    {
        var data = new List<User> { new("David", 40) };
        var reply = new PageReply<User>
        {
            Success = true,
            Description = "自定义描述",
            Data = data,
            TotalCount = 50,
            Index = 2,
            Size = 20
        };
        Assert.IsTrue(reply.Success);
        Assert.AreEqual("自定义描述", reply.Description);
        Assert.IsNotNull(reply.Data);
        Assert.HasCount(1, reply.Data);
        Assert.AreEqual(50, reply.TotalCount);
        Assert.AreEqual(2, reply.Index);
        Assert.AreEqual(20, reply.Size);
    }

    [TestMethod]
    public void GenericTypeTest()
    {
        Assert.AreEqual(typeof(User), typeof(PageReply<User>).GetGenericArguments()[0]);
        Assert.AreEqual(typeof(string), typeof(PageReply<string>).GetGenericArguments()[0]);
    }

    [TestMethod]
    [DataRow(0, 10, 0, 100)]
    [DataRow(1, 10, 10, 100)]
    [DataRow(5, 20, 100, 200)]
    public void VariousPaginationValuesTest(int index, int size, int totalCount, int expectedTotalCount)
    {
        var data = new List<User> { new("Test", 20) };
        var request = new GetUsersRequest { Index = index, Size = size };
        var reply = PageReply.Succeed(data, totalCount, request);
        Assert.AreEqual(index, reply.Index);
        Assert.AreEqual(size, reply.Size);
        Assert.AreNotEqual(expectedTotalCount, reply.TotalCount);
    }
}