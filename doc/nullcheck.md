# 空值断言

SharpDevLib 提供了各种类型的空值检查和验证功能。

## 字符串空值检查

### IsNullOrEmpty

```csharp
string? str = "";

// 检查是否为 null 或空
var isEmpty = str.IsNullOrEmpty();
// 结果: true

str = null;
isEmpty = str.IsNullOrEmpty();
// 结果: true

str = "hello";
isEmpty = str.IsNullOrEmpty();
// 结果: false
```

### NotNullOrEmpty

```csharp
string? str = "hello";

// 检查是否不为 null 且不为空
var isNotEmpty = str.NotNullOrEmpty();
// 结果: true

str = null;
isNotEmpty = str.NotNullOrEmpty();
// 结果: false
```

### IsNullOrWhiteSpace

```csharp
string? str = "  ";

// 检查是否为 null 或空白
var isEmpty = str.IsNullOrWhiteSpace();
// 结果: true

str = "";
isEmpty = str.IsNullOrWhiteSpace();
// 结果: true

str = "hello";
isEmpty = str.IsNullOrWhiteSpace();
// 结果: false
```

### NotNullOrWhiteSpace

```csharp
string? str = "hello";

// 检查是否不为 null 且不为空白
var isNotEmpty = str.NotNullOrWhiteSpace();
// 结果: true

str = "  ";
isNotEmpty = str.NotNullOrWhiteSpace();
// 结果: false
```

## 集合空值检查

### IsNullOrEmpty

```csharp
List<int>? list = null;

// 检查是否为 null 或空
var isEmpty = list.IsNullOrEmpty();
// 结果: true

list = new List<int>();
isEmpty = list.IsNullOrEmpty();
// 结果: true

list = new List<int> { 1, 2, 3 };
isEmpty = list.IsNullOrEmpty();
// 结果: false
```

### NotNullOrEmpty

```csharp
List<int>? list = new List<int> { 1, 2, 3 };

// 检查是否不为 null 且不为空
var isNotEmpty = list.NotNullOrEmpty();
// 结果: true

list = null;
isNotEmpty = list.NotNullOrEmpty();
// 结果: false
```

## Guid 空值检查

### IsNullOrEmpty

```csharp
Guid? guid = Guid.Empty;

// 检查是否为 null 或空
var isEmpty = guid.IsNullOrEmpty();
// 结果: true

guid = null;
isEmpty = guid.IsNullOrEmpty();
// 结果: true

guid = Guid.NewGuid();
isEmpty = guid.IsNullOrEmpty();
// 结果: false
```

### NotNullOrEmpty

```csharp
Guid? guid = Guid.NewGuid();

// 检查是否不为 null 且不为空
var isNotEmpty = guid.NotNullOrEmpty();
// 结果: true

guid = Guid.Empty;
isNotEmpty = guid.NotNullOrEmpty();
// 结果: false
```

## Null 特性

### Null 特性

```csharp
public class User
{
    [Null]
    public string? Name { get; set; }

    [Null]
    public int? Age { get; set; }

    [NotNull]
    public string Email { get; set; } = "";

    public DateTime CreateTime { get; set; }
}
```

### ValidateNull

```csharp
var user = new User
{
    Name = null,
    Age = 25,
    Email = "user@example.com",
    CreateTime = DateTime.Now
};

// 验证对象（标记为 [Null] 的属性可以为 null，标记为 [NotNull] 的属性不能为 null）
var isValid = user.ValidateNull();
// 结果: true
```

### GetNullErrors

```csharp
var user = new User
{
    Name = null,
    Age = null,
    Email = "",  // [NotNull] 属性为空字符串
    CreateTime = default
};

// 获取验证错误
var errors = user.GetNullErrors();

foreach (var error in errors)
{
    Console.WriteLine($"错误: {error}");
}
// 可能的输出:
// 错误: Email cannot be null or empty
```

## 完整示例

### 用户数据验证

```csharp
public class User
{
    [Null]
    public string? Name { get; set; }

    [Null]
    public int? Age { get; set; }

    [NotNull]
    public string Email { get; set; } = "";
}

var user = new User
{
    Name = "张三",
    Age = 25,
    Email = "zhangsan@example.com"
};

// 验证
if (user.ValidateNull())
{
    Console.WriteLine("用户数据有效");
}
else
{
    Console.WriteLine("用户数据无效:");
    foreach (var error in user.GetNullErrors())
    {
        Console.WriteLine($"  - {error}");
    }
}
```

### 可选参数处理

```csharp
public void ProcessData(string? name, int? age)
{
    // 使用空值检查
    if (name.NotNullOrWhiteSpace())
    {
        Console.WriteLine($"处理: {name}");
    }

    if (age.NotNullOrEmpty())
    {
        Console.WriteLine($"年龄: {age}");
    }
}

ProcessData("张三", 25);      // 处理: 张三, 年龄: 25
ProcessData("", null);          // 不输出
ProcessData(null, null);        // 不输出
```

### 集合过滤

```csharp
var users = new List<User?>
{
    new User { Name = "张三", Age = 25 },
    null,
    new User { Name = "李四", Age = 30 },
    new User { Name = "", Age = null },
    new User { Name = "  ", Age = 35 }
};

// 过滤有效用户
var validUsers = users
    .Where(u => u != null && u.Name.NotNullOrWhiteSpace())
    .ToList();

// 输出: 张三, 李四
validUsers.ForEach(u => Console.WriteLine(u!.Name));
```

## 相关文档

- [字符串](string.md)
- [集合](collection.md)
- [基础扩展](../README.md#基础扩展)
