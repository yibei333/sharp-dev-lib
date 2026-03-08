# 集合操作

SharpDevLib 提供了集合操作的扩展方法，包括字典转换、动态排序、去重等功能。

## 列表操作

##### 添加项

```csharp
var list = new List<int> { 1, 2, 3 };
list.AddItem(4)
    .AddItem(5);
Console.WriteLine(string.Join(", ", list));
//1, 2, 3, 4, 5
```

##### 删除项

```csharp
var list = new List<int> { 1, 2, 3, 4 };
list.RemoveItem(2)
    .RemoveItem(3);
Console.WriteLine(string.Join(", ", list));
//1, 4
```

## 字典操作

##### 添加键值对

```csharp
var dictionary = new Dictionary<int, string>
{
    [1] = "One"
};

dictionary.AddItem(2, "Two")
          .AddItem(3, "Three");
Console.WriteLine(string.Join(", ", dictionary.Values));
//One, Two, Three
```
##### 删除键

```csharp
var dictionary = new Dictionary<int, string>
{
    [1] = "One",
    [2] = "Two",
    [3] = "Three"
};

dictionary.RemoveItem(1)
          .RemoveItem(2);
Console.WriteLine(string.Join(", ", dictionary.Values));
//Three
```

## 遍历操作

##### ForEach遍历

```csharp
IEnumerable<int> list = [1, 2, 3];
list.ForEach(Console.WriteLine);
//1
//2
//3

list.ForEach((index, item) => Console.WriteLine($"index:{index},item:{item}"));
//index:0,item:1
//index:1,item:2
//index:2,item:3
```

## 去重

##### 按对象值去重

```csharp
var list = new List<IdNameDto<int>>
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
var list = new List<IdNameDataDto<int,int>>
{
    new() { Id = 1, Name = "Alice", Data = 25 },
    new() { Id = 2, Name = "Bob", Data = 30 },
    new() { Id = 3, Name = "Charlie", Data = 20 }
};

var sortedList = list.OrderByDynamic("Data");
Console.WriteLine(string.Join(", ", sortedList.Select(x => x.Name)));
//Charlie, Alice, Bob
```

##### 降序排序

```csharp
var list = new List<IdNameDataDto<int,int>>
{
    new() { Id = 1, Name = "Alice", Data = 25 },
    new() { Id = 2, Name = "Bob", Data = 30 },
    new() { Id = 3, Name = "Charlie", Data = 20 }
};

var sortedList = list.OrderByDynamic("Data", true);
Console.WriteLine(string.Join(", ", sortedList.Select(x => x.Name)));
//Bob, Alice, Charlie
```

## 相关文档

- [字符串操作](string.md)
- [基础扩展](../README.md#基础扩展)
