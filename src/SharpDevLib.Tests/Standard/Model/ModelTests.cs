using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SharpDevLib.Tests.Standard.Model;

[TestClass]
public class ModelTests
{
    [TestMethod]
    public void Test()
    {
        var request = new GetUsersRequest { Name = "b" };
        var reply = GetUsers(request);
        Console.WriteLine(reply.Serialize(new JsonOption { FormatJson = true }));
    }

    //分页获取用户
    public static PageReply<NameDto> GetUsers(GetUsersRequest request)
    {
        IEnumerable<NameDto> query = _repository;
        if (!string.IsNullOrWhiteSpace(request.Name))
        {
            query = query.Where(u => u.Name!.Contains(request.Name));
        }
        var totalCount = query.Count();
        var data = query.OrderBy(u => u.Name).Skip(request.Index * request.Size).Take(request.Size).ToList();
        return Reply.PageSucceed(data, totalCount, request);
    }

    static readonly List<NameDto> _repository =
    [
        new NameDto("foo"),
        new NameDto("bar"),
        new NameDto("baz"),
    ];
}

public class GetUsersRequest : PageRequest
{
    public string? Name { get; set; }
}
