# Response - 响应模型

提供常用的响应模型类，用于封装 API 响应和方法返回值。

##### 使用 EmptyReply

```csharp
// 构建成功的空响应
var reply1 = EmptyReply.Succeed();
Console.WriteLine(reply1.Success); // 输出: True

var reply2 = EmptyReply.Succeed("操作成功");
Console.WriteLine(reply2.Success); // 输出: True
Console.WriteLine(reply2.Description); // 输出: 操作成功

// 构建失败的空响应
var reply3 = EmptyReply.Failed();
Console.WriteLine(reply3.Success); // 输出: False

var reply4 = EmptyReply.Failed("操作失败");
Console.WriteLine(reply4.Success); // 输出: False
Console.WriteLine(reply4.Description); // 输出: 操作失败

// 使用 ExtraData
var reply5 = EmptyReply.Succeed("操作成功");
reply5.ExtraData = new { Code = 1001, Message = "Additional info" };
Console.WriteLine(reply5.ExtraData); // 输出: { Code = 1001, Message = Additional info }
```

##### 使用 DataReply

```csharp
var user = new User { Id = 1, Name = "张三" };

// 构建成功的数据响应
var reply1 = DataReply<User>.Succeed(user);
Console.WriteLine(reply1.Success); // 输出: True
Console.WriteLine(reply1.Data.Name); // 输出: 张三

var reply2 = DataReply<User>.Succeed(user, "查询成功");
Console.WriteLine(reply2.Success); // 输出: True
Console.WriteLine(reply2.Description); // 输出: 查询成功
Console.WriteLine(reply2.Data.Name); // 输出: 张三

// 构建失败的数据响应
var reply3 = DataReply<User>.Failed("用户不存在");
Console.WriteLine(reply3.Success); // 输出: False
Console.WriteLine(reply3.Description); // 输出: 用户不存在
Console.WriteLine(reply3.Data); // 输出: (null)

// 使用 ExtraData
var reply4 = DataReply<User>.Succeed(user);
reply4.ExtraData = new { TotalCount = 100 };
Console.WriteLine(reply4.ExtraData); // 输出: { TotalCount = 100 }
```

##### 使用 PageReply

```csharp
var data = new List<NameDto>
{
    new("张三"),
    new("李四")
};

// 构建成功的分页响应（使用索引和大小）
var reply1 = PageReply.Succeed(data, 100, 0, 20);
Console.WriteLine(reply1.Success); // 输出: True
Console.WriteLine(reply1.Index); // 输出: 0
Console.WriteLine(reply1.Size); // 输出: 20
Console.WriteLine(reply1.TotalCount); // 输出: 100
Console.WriteLine(reply1.PageCount); // 输出: 5
Console.WriteLine(reply1.Data.Count); // 输出: 2

var reply2 = PageReply.Succeed(data, 100, 0, 20, "查询成功");
Console.WriteLine(reply2.Success); // 输出: True
Console.WriteLine(reply2.Description); // 输出: 查询成功

// 构建成功的分页响应（使用 PageRequest）
var request = new PageRequest(0, 20);
var reply3 = PageReply.Succeed(data, 100, request);
Console.WriteLine(reply3.Success); // 输出: True
Console.WriteLine(reply3.Index); // 输出: 0
Console.WriteLine(reply3.Size); // 输出: 20

// 构建失败的分页响应
var reply4 = PageReply.Failed<NameDto>("查询失败");
Console.WriteLine(reply4.Success); // 输出: False
Console.WriteLine(reply4.Description); // 输出: 查询失败

// 使用 ExtraData
var reply5 = PageReply.Succeed(data, 100, 0, 20);
reply5.ExtraData = new { QueryTime = "2024-01-01" };
Console.WriteLine(reply5.ExtraData); // 输出: { QueryTime = 2024-01-01 }
```

## 相关文档
- [请求模型](request.md)
- [DTO](dto.md)
- [模型](../README.md#模型)