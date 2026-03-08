# 集合操作

SharpDevLib 提供了集合操作的扩展方法，包括字典转换、动态排序、去重等功能。

## 字典转换

##### 转换为字典

```csharp
var keyValues = new List<KeyValuePair<int, string>>
{
    new(1, "One"),
    new(2, "Two"),
    new(3, "Three")
};

var dictionary = keyValues.ToDictionary();
Console.WriteLine(dictionary[1]);
//One
```

## 列表操作

##### 添加项

```csharp
var list = new List<int> { 1, 2, 3 };
list.AddItem(4);
Console.WriteLine(string.Join(", ", list));
//1, 2, 3, 4
```

##### 删除项

```csharp
var list = new List<int> { 1, 2, 3, 2 };
list.RemoveItem(2);
Console.WriteLine(string.Join(", ", list));
//1, 3, 2
```

## 字典操作

##### 添加键值对

```csharp
var dictionary = new Dictionary<int, string>
{
    [1] = "One"
};

dictionary.AddItem(2, "Two");
Console.WriteLine(dictionary[2]);
//Two
```

##### 删除键

```csharp
var dictionary = new Dictionary<int, string>
{
    [1] = "One",
    [2] = "Two"
};

dictionary.RemoveItem(1);
Console.WriteLine(dictionary.ContainsKey(1));
//False
```

## 遍历操作

##### ForEach遍历

```csharp
var list = new List<int> { 1, 2, 3, 4, 5 };
list.ForEach(item => Console.WriteLine(item));
//1
//2
//3
//4
//5
```

##### ForEach带索引遍历

```csharp
var list = new List<int> { 10, 20, 30, 40, 50 };
list.ForEach((index, item) => Console.WriteLine($"[{index}] {item}"));
//[0] 10
//[1] 20
//[2] 30
//[3] 40
//[4] 50
```

## 去重

##### 按对象值去重

```csharp
class User
{
    public int Id { get; set; }
    public string Name { get; set; }
}

var list = new List<User>
{
    new() { Id = 1, Name = "Alice" },
    new() { Id = 1, Name = "Alice" },
    new() { Id = 2, Name = "Bob" }
};

var distinctList = list.DistinctByObjectValue().ToList();
Console.WriteLine(distinctList.Count);
//2
```

## 动态排序

##### 按属性名动态排序

```csharp
class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
}

var list = new List<User>
{
    new() { Id = 1, Name = "Alice", Age = 25 },
    new() { Id = 2, Name = "Bob", Age = 30 },
    new() { Id = 3, Name = "Charlie", Age = 20 }
};

var sortedList = list.OrderByDynamic("Age");
Console.WriteLine(string.Join(", ", sortedList.Select(u => u.Name)));
//Charlie, Alice, Bob
```

##### 降序排序

```csharp
class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
}

var list = new List<User>
{
    new() { Id = 1, Name = "Alice", Age = 25 },
    new() { Id = 2, Name = "Bob", Age = 30 },
    new() { Id = 3, Name = "Charlie", Age = 20 }
};

var sortedList = list.OrderByDynamic("Age", descending: true);
Console.WriteLine(string.Join(", ", sortedList.Select(u => u.Name)));
//Bob, Alice, Charlie
```

## 相关文档

- [字符串操作](string.md)
- [基础扩展](../README.md#基础扩展)
