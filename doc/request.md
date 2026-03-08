# Request - 请求模型

提供常用的请求模型类，用于封装 API 请求和方法参数。

## 类

### BaseRequest

请求基类，为所有请求对象提供基础类型支持。可用于反射操作、泛型约束或类型检查等场景。

### IdRequest\<TId\>

包含标识符的请求对象。用于需要通过标识符进行操作或查询的请求场景，如删除、更新、获取等操作。

#### 泛型参数

| 参数 | 说明 |
| --- | --- |
| TId | 标识符类型，如 int、long、Guid 等 |

#### 构造函数

| 构造函数 | 说明 |
| --- | --- |
| IdRequest() | 实例化 IdRequest 对象 |
| IdRequest(TId id) | 实例化 IdRequest 对象并初始化标识符 |

#### 属性

| 属性 | 类型 | 说明 |
| --- | --- | --- |
| Id | TId | 标识符，用于唯一标识操作对象 |

### NameRequest

包含名称的请求对象。用于需要通过名称进行操作或查询的请求场景，如按名称搜索、获取名称列表等。

#### 构造函数

| 构造函数 | 说明 |
| --- | --- |
| NameRequest() | 实例化 NameRequest 对象 |
| NameRequest(string? name) | 实例化 NameRequest 对象并初始化名称 |

#### 属性

| 属性 | 类型 | 说明 |
| --- | --- | --- |
| Name | string? | 名称，用于描述或标识对象 |

### DataRequest\<TData\>

包含数据的请求对象。用于包装单个数据对象进行传输，适用于 API 请求或方法参数。

#### 泛型参数

| 参数 | 说明 |
| --- | --- |
| TData | 数据类型，可以是任何可序列化的类型 |

#### 构造函数

| 构造函数 | 说明 |
| --- | --- |
| DataRequest() | 实例化 DataRequest 对象 |
| DataRequest(TData? data) | 实例化 DataRequest 对象并初始化数据 |

#### 属性

| 属性 | 类型 | 说明 |
| --- | --- | --- |
| Data | TData? | 数据对象 |

### PageRequest

分页请求对象。继承自 BaseRequest，用于分页查询请求场景，包含当前页索引和每页数据条数。

#### 构造函数

| 构造函数 | 说明 |
| --- | --- |
| PageRequest() | 实例化 PageRequest 对象，使用默认参数（index=0, size=20） |
| PageRequest(int index, int size) | 实例化 PageRequest 对象 |

#### 属性

| 属性 | 类型 | 说明 |
| --- | --- | --- |
| Index | int | 当前页索引，从 0 开始 |
| Size | int | 每页数据条数 |

#### 异常

| 异常 | 说明 |
| --- | --- |
| ArgumentException | 当 index 或 size 小于 0 时引发异常 |

### IdNameRequest\<TId\>

包含标识符和名称的复合请求对象。继承自 `IdRequest\<TId\>`，在标识符基础上增加了名称字段，适用于需要同时传输 ID 和名称的请求场景。

#### 泛型参数

| 参数 | 说明 |
| --- | --- |
| TId | 标识符类型，如 int、long、Guid 等 |

#### 构造函数

| 构造函数 | 说明 |
| --- | --- |
| IdNameRequest() | 实例化 IdNameRequest 对象 |
| IdNameRequest(TId id) | 实例化 IdNameRequest 对象并初始化标识符 |
| IdNameRequest(string? name) | 实例化 IdNameRequest 对象并初始化名称 |
| IdNameRequest(TId id, string? name) | 实例化 IdNameRequest 对象并初始化标识符和名称 |

#### 属性

| 属性 | 类型 | 说明 |
| --- | --- | --- |
| Id | TId | 标识符，继承自 IdRequest |
| Name | string? | 名称，用于描述或标识对象 |

### IdDataRequest\<TId, TData\>

包含标识符和数据的复合请求对象。继承自 `IdRequest\<TId\>`，在标识符基础上增加了数据字段，适用于需要同时传输 ID 和数据的请求场景。

#### 泛型参数

| 参数 | 说明 |
| --- | --- |
| TId | 标识符类型，如 int、long、Guid 等 |
| TData | 数据类型，可以是任何可序列化的类型 |

#### 构造函数

| 构造函数 | 说明 |
| --- | --- |
| IdDataRequest() | 实例化 IdDataRequest 对象 |
| IdDataRequest(TData? data) | 实例化 IdDataRequest 对象并初始化数据 |
| IdDataRequest(TId id) | 实例化 IdDataRequest 对象并初始化标识符 |
| IdDataRequest(TId id, TData data) | 实例化 IdDataRequest 对象并初始化标识符和数据 |

#### 属性

| 属性 | 类型 | 说明 |
| --- | --- | --- |
| Id | TId | 标识符，继承自 IdRequest |
| Data | TData? | 数据对象 |

### IdNameDataRequest\<TId, TData\>

包含标识符、名称和数据的复合请求对象。继承自 `IdDataRequest\<TId,TData\>`，在标识符和数据基础上增加了名称字段，适用于需要同时传输 ID、名称和数据的请求场景。

#### 泛型参数

| 参数 | 说明 |
| --- | --- |
| TId | 标识符类型，如 int、long、Guid 等 |
| TData | 数据类型，可以是任何可序列化的类型 |

#### 构造函数

| 构造函数 | 说明 |
| --- | --- |
| IdNameDataRequest() | 实例化 IdNameDataRequest 对象 |
| IdNameDataRequest(TData? data) | 实例化 IdNameDataRequest 对象并初始化数据 |
| IdNameDataRequest(TId id) | 实例化 IdNameDataRequest 对象并初始化标识符 |
| IdNameDataRequest(string? name) | 实例化 IdNameDataRequest 对象并初始化名称 |
| IdNameDataRequest(TId id, TData data) | 实例化 IdNameDataRequest 对象并初始化标识符和数据 |
| IdNameDataRequest(TId id, string? name) | 实例化 IdNameDataRequest 对象并初始化标识符和名称 |
| IdNameDataRequest(string? name, TData? data) | 实例化 IdNameDataRequest 对象并初始化名称和数据 |
| IdNameDataRequest(TId id, string? name, TData? data) | 实例化 IdNameDataRequest 对象并初始化标识符、名称和数据 |

#### 属性

| 属性 | 类型 | 说明 |
| --- | --- | --- |
| Id | TId | 标识符，继承自 IdRequest |
| Name | string? | 名称，用于描述或标识数据对象 |
| Data | TData? | 数据对象，继承自 IdDataRequest |

## 示例

### 使用 IdRequest

```csharp
var request = new IdRequest<int>(1);
Console.WriteLine(request.Id); // 输出: 1

var request2 = new IdRequest<string>("user-001");
Console.WriteLine(request2.Id); // 输出: user-001
```

### 使用 NameRequest

```csharp
var request = new NameRequest("张三");
Console.WriteLine(request.Name); // 输出: 张三

var request2 = new NameRequest();
Console.WriteLine(request2.Name); // 输出: (null)
```

### 使用 DataRequest

```csharp
var user = new User { Id = 1, Name = "张三" };
var request = new DataRequest<User>(user);
Console.WriteLine(request.Data.Name); // 输出: 张三

var request2 = new DataRequest<User>();
Console.WriteLine(request2.Data); // 输出: (null)
```

### 使用 PageRequest

```csharp
// 使用默认参数
var request1 = new PageRequest();
Console.WriteLine(request1.Index); // 输出: 0
Console.WriteLine(request1.Size); // 输出: 20

// 自定义参数
var request2 = new PageRequest(1, 10);
Console.WriteLine(request2.Index); // 输出: 1
Console.WriteLine(request2.Size); // 输出: 10
```

### 使用 IdNameRequest

```csharp
var request = new IdNameRequest<int>(1, "张三");
Console.WriteLine(request.Id); // 输出: 1
Console.WriteLine(request.Name); // 输出: 张三

var request2 = new IdNameRequest<int>(1);
Console.WriteLine(request2.Id); // 输出: 1
Console.WriteLine(request2.Name); // 输出: (null)
```

### 使用 IdDataRequest

```csharp
var user = new User { Id = 1, Name = "张三" };
var request = new IdDataRequest<int, User>(1, user);
Console.WriteLine(request.Id); // 输出: 1
Console.WriteLine(request.Data.Name); // 输出: 张三

var request2 = new IdDataRequest<int, User>(1);
Console.WriteLine(request2.Id); // 输出: 1
Console.WriteLine(request2.Data); // 输出: (null)
```

### 使用 IdNameDataRequest

```csharp
var user = new User { Id = 1, Name = "张三", Age = 25 };
var request = new IdNameDataRequest<int, User>(1, "用户", user);
Console.WriteLine(request.Id); // 输出: 1
Console.WriteLine(request.Name); // 输出: 用户
Console.WriteLine(request.Data.Name); // 输出: 张三

var request2 = new IdNameDataRequest<int, User>(1);
Console.WriteLine(request2.Id); // 输出: 1
Console.WriteLine(request2.Name); // 输出: (null)
Console.WriteLine(request2.Data); // 输出: (null)
```

### 在 API 中使用

```csharp
// GET /api/user/1
public User GetUser([FromRoute] int id)
{
    var request = new IdRequest<int>(id);
    return userService.GetUser(request);
}

// POST /api/user
public User CreateUser([FromBody] User user)
{
    var request = new DataRequest<User>(user);
    return userService.CreateUser(request);
}

// GET /api/users?page=1&size=10
public PageReply<User> GetUsers([FromQuery] int page, [FromQuery] int size)
{
    var request = new PageRequest(page, size);
    return userService.GetUsers(request);
}

// PUT /api/user/1
public User UpdateUser([FromRoute] int id, [FromBody] User user)
{
    var request = new IdDataRequest<int, User>(id, user);
    return userService.UpdateUser(request);
}
```

### 作为泛型约束

```csharp
public interface IUserService
{
    User GetUser(IdRequest<int> request);
    void CreateUser(DataRequest<User> request);
    void UpdateUser(IdDataRequest<int, User> request);
    void DeleteUser(IdRequest<int> request);
    PageReply<User> GetUsers(PageRequest request);
}
```

## 特性

- 提供常用的请求模型类型
- 支持泛型，适用于各种数据类型
- 提供多种组合方式（ID、名称、数据）
- 支持灵活的构造函数重载
- 继承关系清晰，便于扩展
- PageRequest 支持分页查询
