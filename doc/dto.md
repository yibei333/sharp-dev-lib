# DTO - 数据传输对象

提供常用的数据传输对象（DTO）类，用于封装和传输数据。

## 类

### BaseDto

DTO 基类，为所有数据传输对象提供基础类型支持。可用于反射操作、泛型约束或类型检查等场景。

### IdDto\<TId\>

包含标识符的数据传输对象。用于需要通过标识符进行操作或查询的场景，如删除、更新、获取等操作。

#### 泛型参数

| 参数 | 说明 |
| --- | --- |
| TId | 标识符类型，如 int、long、Guid 等 |

#### 构造函数

| 构造函数 | 说明 |
| --- | --- |
| IdDto() | 实例化 IdDto 对象 |
| IdDto(TId id) | 实例化 IdDto 对象并初始化标识符 |

#### 属性

| 属性 | 类型 | 说明 |
| --- | --- | --- |
| Id | TId | 标识符，用于唯一标识对象 |

### NameDto

包含名称的数据传输对象。用于需要通过名称进行操作或查询的场景，如按名称搜索、获取名称列表等。

#### 构造函数

| 构造函数 | 说明 |
| --- | --- |
| NameDto() | 实例化 NameDto 对象 |
| NameDto(string? name) | 实例化 NameDto 对象并初始化名称 |

#### 属性

| 属性 | 类型 | 说明 |
| --- | --- | --- |
| Name | string? | 名称，用于描述或标识对象 |

### DataDto\<TData\>

包含数据的数据传输对象。用于包装单个数据对象进行传输，适用于 API 响应或方法返回值。

#### 泛型参数

| 参数 | 说明 |
| --- | --- |
| TData | 数据类型，可以是任何可序列化的类型 |

#### 构造函数

| 构造函数 | 说明 |
| --- | --- |
| DataDto() | 实例化 DataDto 对象 |
| DataDto(TData? data) | 实例化 DataDto 对象并初始化数据 |

#### 属性

| 属性 | 类型 | 说明 |
| --- | --- | --- |
| Data | TData? | 数据对象 |

### IdNameDto\<TId\>

包含标识符和名称的复合数据传输对象。继承自 `IdDto\<TId\>`，在标识符基础上增加了名称字段，适用于需要同时传输 ID 和名称的场景。

#### 泛型参数

| 参数 | 说明 |
| --- | --- |
| TId | 标识符类型，如 int、long、Guid 等 |

#### 构造函数

| 构造函数 | 说明 |
| --- | --- |
| IdNameDto() | 实例化 IdNameDto 对象 |
| IdNameDto(TId id) | 实例化 IdNameDto 对象并初始化标识符 |
| IdNameDto(string? name) | 实例化 IdNameDto 对象并初始化名称 |
| IdNameDto(TId id, string? name) | 实例化 IdNameDto 对象并初始化标识符和名称 |

#### 属性

| 属性 | 类型 | 说明 |
| --- | --- | --- |
| Id | TId | 标识符，继承自 IdDto |
| Name | string? | 名称，用于描述或显示对象 |

### IdDataDto\<TId, TData\>

包含标识符和数据的复合数据传输对象。继承自 `DataDto\<TData\>`，在数据基础上增加了标识符字段，适用于需要同时传输 ID 和数据的场景。

#### 泛型参数

| 参数 | 说明 |
| --- | --- |
| TId | 标识符类型，如 int、long、Guid 等 |
| TData | 数据类型，可以是任何可序列化的类型 |

#### 构造函数

| 构造函数 | 说明 |
| --- | --- |
| IdDataDto() | 实例化 IdDataDto 对象 |
| IdDataDto(TData? data) | 实例化 IdDataDto 对象并初始化数据 |
| IdDataDto(TId id) | 实例化 IdDataDto 对象并初始化标识符 |
| IdDataDto(TId id, TData? data) | 实例化 IdDataDto 对象并初始化标识符和数据 |

#### 属性

| 属性 | 类型 | 说明 |
| --- | --- | --- |
| Id | TId | 标识符，用于唯一标识数据对象 |
| Data | TData? | 数据对象，继承自 DataDto |

### IdNameDataDto\<TId, TData\>

包含标识符、名称和数据的复合数据传输对象。继承自 `DataDto\<TData\>`，在数据基础上增加了标识符和名称字段，适用于需要同时传输 ID、名称和数据的场景。

#### 泛型参数

| 参数 | 说明 |
| --- | --- |
| TId | 标识符类型，如 int、long、Guid 等 |
| TData | 数据类型，可以是任何可序列化的类型 |

#### 构造函数

| 构造函数 | 说明 |
| --- | --- |
| IdNameDataDto() | 实例化 IdNameDataDto 对象 |
| IdNameDataDto(TData? data) | 实例化 IdNameDataDto 对象并初始化数据 |
| IdNameDataDto(TId id) | 实例化 IdNameDataDto 对象并初始化标识符 |
| IdNameDataDto(string name) | 实例化 IdNameDataDto 对象并初始化名称 |
| IdNameDataDto(TId id, TData? data) | 实例化 IdNameDataDto 对象并初始化标识符和数据 |
| IdNameDataDto(TId id, string name) | 实例化 IdNameDataDto 对象并初始化标识符和名称 |
| IdNameDataDto(string name, TData? data) | 实例化 IdNameDataDto 对象并初始化名称和数据 |
| IdNameDataDto(TId id, string name, TData? data) | 实例化 IdNameDataDto 对象并初始化标识符、名称和数据 |

#### 属性

| 属性 | 类型 | 说明 |
| --- | --- | --- |
| Id | TId | 标识符，用于唯一标识数据对象 |
| Name | string? | 名称，用于描述或显示数据对象 |
| Data | TData? | 数据对象，继承自 DataDto |

## 示例

### 使用 IdDto

```csharp
var idDto = new IdDto<int>(1);
Console.WriteLine(idDto.Id); // 输出: 1

var idDto2 = new IdDto<string>("user-001");
Console.WriteLine(idDto2.Id); // 输出: user-001
```

### 使用 NameDto

```csharp
var nameDto = new NameDto("张三");
Console.WriteLine(nameDto.Name); // 输出: 张三

var nameDto2 = new NameDto();
Console.WriteLine(nameDto2.Name); // 输出: (null)
```

### 使用 DataDto

```csharp
var user = new User { Id = 1, Name = "张三" };
var dataDto = new DataDto<User>(user);
Console.WriteLine(dataDto.Data.Name); // 输出: 张三

var dataDto2 = new DataDto<User>();
Console.WriteLine(dataDto2.Data); // 输出: (null)
```

### 使用 IdNameDto

```csharp
var idNameDto = new IdNameDto<int>(1, "张三");
Console.WriteLine(idNameDto.Id); // 输出: 1
Console.WriteLine(idNameDto.Name); // 输出: 张三

var idNameDto2 = new IdNameDto<int>(1);
Console.WriteLine(idNameDto2.Id); // 输出: 1
Console.WriteLine(idNameDto2.Name); // 输出: (null)
```

### 使用 IdDataDto

```csharp
var user = new User { Id = 1, Name = "张三" };
var idDataDto = new IdDataDto<int, User>(1, user);
Console.WriteLine(idDataDto.Id); // 输出: 1
Console.WriteLine(idDataDto.Data.Name); // 输出: 张三

var idDataDto2 = new IdDataDto<int, User>(1);
Console.WriteLine(idDataDto2.Id); // 输出: 1
Console.WriteLine(idDataDto2.Data); // 输出: (null)
```

### 使用 IdNameDataDto

```csharp
var user = new User { Id = 1, Name = "张三", Age = 25 };
var idNameDataDto = new IdNameDataDto<int, User>(1, "用户", user);
Console.WriteLine(idNameDataDto.Id); // 输出: 1
Console.WriteLine(idNameDataDto.Name); // 输出: 用户
Console.WriteLine(idNameDataDto.Data.Name); // 输出: 张三

var idNameDataDto2 = new IdNameDataDto<int, User>(1);
Console.WriteLine(idNameDataDto2.Id); // 输出: 1
Console.WriteLine(idNameDataDto2.Name); // 输出: (null)
Console.WriteLine(idNameDataDto2.Data); // 输出: (null)
```

### 在 API 中使用

```csharp
// 请求模型
public class DeleteUserRequest : IdDto<int>
{
}

// 响应模型
public class DeleteUserResponse : BaseDto
{
    public bool Success { get; set; }
}

// API 方法
public DeleteUserResponse DeleteUser(DeleteUserRequest request)
{
    var user = repository.GetById(request.Id);
    repository.Delete(user);
    return new DeleteUserResponse { Success = true };
}
```

### 作为泛型约束

```csharp
public interface IRepository<T> where T : BaseDto
{
    T GetById(int id);
    void Add(T entity);
    void Delete(T entity);
}
```

## 特性

- 提供常用的数据传输对象类型
- 支持泛型，适用于各种数据类型
- 提供多种组合方式（ID、名称、数据）
- 支持灵活的构造函数重载
- 继承关系清晰，便于扩展
