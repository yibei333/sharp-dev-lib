# 集合操作

SharpDevLib 提供了集合扩展方法，包括链式操作、动态排序、去重等功能。

## 空值检查

### IsNullOrEmpty

```csharp
List<int>? list = null;

// 检查集合是否为 null 或空
var isEmpty = list.IsNullOrEmpty();
// 结果: true

list = new List<int>();
isEmpty = list.IsNullOrEmpty();
// 结果: true

list.Add(1);
isEmpty = list.IsNullOrEmpty();
// 结果: false
```

### NotNullOrEmpty

```csharp
List<int>? list = new List<int> { 1, 2, 3 };

// 检查集合是否不为 null 且不为空
var isNotEmpty = list.NotNullOrEmpty();
// 结果: true
```

## 链式操作

### AddItem (列表)

```csharp
// 添加项并返回列表
var list = new List<int> { 1, 2, 3 };
list.AddItem(4).AddItem(5);
// 结果: [1, 2, 3, 4, 5]
```

### RemoveItem (列表)

```csharp
// 删除项并返回列表
var list = new List<int> { 1, 2, 3, 4 };
list.RemoveItem(3).RemoveItem(4);
// 结果: [1, 2]
```

### AddItem (字典)

```csharp
// 添加键值对并返回字典
var dict = new Dictionary<string, int>
{
    ["a"] = 1,
    ["b"] = 2
};
dict.AddItem("c", 3);
// 结果: { "a": 1, "b": 2, "c": 3 }
```

### RemoveItem (字典)

```csharp
// 删除键并返回字典
var dict = new Dictionary<string, int>
{
    ["a"] = 1,
    ["b"] = 2,
    ["c"] = 3
};
dict.RemoveItem("b").RemoveItem("c");
// 结果: { "a": 1 }
```

## ForEach

### 基本 ForEach

```csharp
var list = new List<int> { 1, 2, 3, 4, 5 };

// 对每个元素执行操作
list.ForEach(x => Console.WriteLine(x));
// 输出:
// 1
// 2
// 3
// 4
// 5
```

### 带索引的 ForEach

```csharp
var list = new List<string> { "a", "b", "c" };

// 对每个元素执行操作（包含索引）
list.ForEach((index, item) =>
{
    Console.WriteLine($"[{index}] {item}");
});
// 输出:
// [0] a
// [1] b
// [2] c
```

## 去重

### DistinctByObjectValue

```csharp
public class User
{
    public string Name { get; set; }
    public int Age { get; set; }
}

var users = new List<User>
{
    new User { Name = "张三", Age = 25 },
    new User { Name = "张三", Age = 25 },  // 重复
    new User { Name = "李四", Age = 30 }
};

// 根据对象的值去重
var distinctUsers = users.DistinctByObjectValue().ToList();
// 结果: 2 个用户（去除了重复的张三）
```

## 动态排序

### OrderByDynamic (集合)

```csharp
public class User
{
    public string Name { get; set; }
    public int Age { get; set; }
}

var users = new List<User>
{
    new User { Name = "张三", Age = 25 },
    new User { Name = "李四", Age = 20 },
    new User { Name = "王五", Age = 30 }
};

// 按属性名升序排序
var sorted = users.OrderByDynamic("Age").ToList();
// 结果: 按年龄升序

// 按属性名降序排序
var sorted = users.OrderByDynamic("Age", descending: true).ToList();
// 结果: 按年龄降序
```

### OrderByDynamic (查询)

```csharp
var users = new List<User> { /* ... */ };

// 按属性名排序查询
var query = users.AsQueryable();
var sorted = query.OrderByDynamic("Name").ToList();
```

## KeyValuePair 转字典

```csharp
var pairs = new List<KeyValuePair<string, int>>
{
    new("a", 1),
    new("b", 2),
    new("c", 3)
};

// 转换为字典
var dict = pairs.ToDictionary();
// 结果: { "a": 1, "b": 2, "c": 3 }
```

## 完整示例

### 链式操作

```csharp
var result = new List<int> { 1, 2, 3 }
    .AddItem(4)
    .AddItem(5)
    .AddItem(6)
    .Where(x => x > 2)
    .ToList();

// 结果: [3, 4, 5, 6]
```

### 数据处理

```csharp
var users = new List<User>
{
    new User { Name = "张三", Age = 25 },
    new User { Name = "李四", Age = 20 },
    new User { Name = "王五", Age = 30 }
};

// 处理每个用户
users.ForEach(user =>
{
    Console.WriteLine($"用户: {user.Name}, 年龄: {user.Age}");
});

// 按年龄排序
var sortedUsers = users.OrderByDynamic("Age", descending: true).ToList();

// 去重
var distinctUsers = users.DistinctByObjectValue().ToList();
```

## 相关文档

- [枚举](enum.md)
- [字符串](string.md)
- [基础扩展](../README.md#基础扩展)
