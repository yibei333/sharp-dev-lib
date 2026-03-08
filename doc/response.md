# Response - 响应模型

提供常用的响应模型类，用于封装 API 响应和方法返回值。

## 类

### BaseReply

响应基类，为所有响应对象提供基础类型支持。包含操作是否成功、描述信息和额外数据等通用字段。适用于 API 响应或方法返回值等场景。

#### 属性

| 属性 | 类型 | 说明 |
| --- | --- | --- |
| Success | bool | 操作是否成功，用于判断业务逻辑是否正确执行 |
| Description | string? | 描述信息，用于说明操作结果或错误原因 |
| ExtraData | object? | 额外数据，用于传递非结构化的额外信息 |

### EmptyReply

空响应对象。继承自 BaseReply，用于仅返回操作结果而不包含具体数据的场景。适用于删除、更新等不返回数据的操作。

#### 静态方法

| 方法 | 说明 |
| --- | --- |
| EmptyReply.Succeed(string? description = null) | 构建成功的空响应 |
| EmptyReply.Failed(string? description = null) | 构建失败的空响应 |

### DataReply\<TData\>

包含数据的响应对象。继承自 BaseReply，在基础响应字段上增加了数据字段，适用于需要返回具体数据的场景。

#### 泛型参数

| 参数 | 说明 |
| --- | --- |
| TData | 数据类型，可以是任何可序列化的类型 |

#### 属性

| 属性 | 类型 | 说明 |
| --- | --- | --- |
| Success | bool | 继承自 BaseReply |
| Description | string? | 继承自 BaseReply |
| ExtraData | object? | 继承自 BaseReply |
| Data | TData? | 数据对象，用于承载具体的业务数据 |

#### 静态方法（通过 DataReply 静态类）

| 方法 | 说明 |
| --- | --- |
| DataReply.Succeed\<TData\>(TData data, string? description = null) | 构建成功的数据响应 |
| DataReply.Failed\<TData\>(string? description = null) | 构建失败的数据响应 |

### PageReply\<TData\>

分页响应对象。继承自 BaseReply，在基础响应字段上增加了分页相关字段和数据列表，适用于分页查询场景。

#### 泛型参数

| 参数 | 说明 |
| --- | --- |
| TData | 数据类型，可以是任何可序列化的类型 |

#### 属性

| 属性 | 类型 | 说明 |
| --- | --- | --- |
| Success | bool | 继承自 BaseReply |
| Description | string? | 继承自 BaseReply |
| ExtraData | object? | 继承自 BaseReply |
| Index | int | 当前页索引，从 0 开始 |
| Size | int | 每页数据条数 |
| TotalCount | long | 总记录数 |
| PageCount | long | 总页数，根据 TotalCount 和 Size 自动计算 |
| Data | List\<TData\>? | 当前页的数据列表 |

#### 静态方法（通过 PageReply 静态类）

| 方法 | 说明 |
| --- | --- |
| PageReply.Succeed\<TData\>(List\<TData\> data, long total, int index, int size, string? description = null) | 构建成功的分页响应 |
| PageReply.Succeed\<TData\>(List\<TData\> data, long total, PageRequest request, string? description = null) | 构建成功的分页响应（使用 PageRequest） |
| PageReply.Failed\<TData\>(string? description = null) | 构建失败的分页响应 |

## 示例

### 使用 EmptyReply

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

### 使用 DataReply

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

### 使用 PageReply

```csharp
var users = new List<User>
{
    new() { Id = 1, Name = "张三" },
    new() { Id = 2, Name = "李四" }
};

// 构建成功的分页响应（使用索引和大小）
var reply1 = PageReply<User>.Succeed(users, 100, 0, 20);
Console.WriteLine(reply1.Success); // 输出: True
Console.WriteLine(reply1.Index); // 输出: 0
Console.WriteLine(reply1.Size); // 输出: 20
Console.WriteLine(reply1.TotalCount); // 输出: 100
Console.WriteLine(reply1.PageCount); // 输出: 5
Console.WriteLine(reply1.Data.Count); // 输出: 2

var reply2 = PageReply<User>.Succeed(users, 100, 0, 20, "查询成功");
Console.WriteLine(reply2.Success); // 输出: True
Console.WriteLine(reply2.Description); // 输出: 查询成功

// 构建成功的分页响应（使用 PageRequest）
var request = new PageRequest(0, 20);
var reply3 = PageReply<User>.Succeed(users, 100, request);
Console.WriteLine(reply3.Success); // 输出: True
Console.WriteLine(reply3.Index); // 输出: 0
Console.WriteLine(reply3.Size); // 输出: 20

// 构建失败的分页响应
var reply4 = PageReply<User>.Failed("查询失败");
Console.WriteLine(reply4.Success); // 输出: False
Console.WriteLine(reply4.Description); // 输出: 查询失败

// 使用 ExtraData
var reply5 = PageReply<User>.Succeed(users, 100, 0, 20);
reply5.ExtraData = new { QueryTime = "2024-01-01" };
Console.WriteLine(reply5.ExtraData); // 输出: { QueryTime = 2024-01-01 }
```

### 在 API 中使用

```csharp
// DELETE /api/user/1
public EmptyReply DeleteUser([FromRoute] int id)
{
    try
    {
        userService.Delete(id);
        return EmptyReply.Succeed("删除成功");
    }
    catch (Exception ex)
    {
        return EmptyReply.Failed($"删除失败: {ex.Message}");
    }
}

// GET /api/user/1
public DataReply<User> GetUser([FromRoute] int id)
{
    var user = userService.GetById(id);
    if (user is null)
    {
        return DataReply<User>.Failed("用户不存在");
    }
    return DataReply<User>.Succeed(user);
}

// GET /api/users?page=1&size=10
public PageReply<User> GetUsers([FromQuery] PageRequest request)
{
    var (users, total) = userService.GetUsers(request);
    return PageReply<User>.Succeed(users, total, request);
}
```

### 作为泛型约束

```csharp
public interface IUserService
{
    DataReply<User> GetUser(IdRequest<int> request);
    EmptyReply CreateUser(DataRequest<User> request);
    EmptyReply UpdateUser(IdDataRequest<int, User> request);
    EmptyReply DeleteUser(IdRequest<int> request);
    PageReply<User> GetUsers(PageRequest request);
}
```

### 计算总页数

```csharp
// 总页数自动计算
var reply = PageReply<User>.Succeed(users, 95, 0, 20);
Console.WriteLine(reply.PageCount); // 输出: 5 (Math.Ceiling(95 / 20.0))

// 边界情况
var reply2 = PageReply<User>.Succeed(users, 0, 0, 20);
Console.WriteLine(reply2.PageCount); // 输出: 0

var reply3 = PageReply<User>.Succeed(users, 100, 0, 0);
Console.WriteLine(reply3.PageCount); // 输出: 0
```

## 特性

- 提供常用的响应模型类型
- 支持泛型，适用于各种数据类型
- 提供静态工厂方法，便于快速构建响应
- 继承关系清晰，便于扩展
- BaseReply 提供通用字段（Success、Description、ExtraData）
- EmptyReply 适用于不返回数据的操作
- DataReply 适用于返回单个数据对象
- PageReply 适用于分页查询场景
- PageReply 自动计算总页数
- 支持 PageRequest 对象简化分页响应构建
