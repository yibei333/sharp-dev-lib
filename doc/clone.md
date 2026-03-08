# 克隆操作

SharpDevLib 提供了对象的深拷贝功能。

## 深拷贝

##### 简单对象深拷贝

```csharp
class User
{
    public string Name { get; set; }
    public int Age { get; set; }
}

var original = new User { Name = "Alice", Age = 25 };
var cloned = original.DeepClone();

Console.WriteLine(cloned.Name);
//Alice
Console.WriteLine(cloned.Age);
//25

Console.WriteLine(ReferenceEquals(original, cloned));
//False
```

##### 修改克隆对象不影响原对象

```csharp
class User
{
    public string Name { get; set; }
    public int Age { get; set; }
}

var original = new User { Name = "Alice", Age = 25 };
var cloned = original.DeepClone();

cloned.Name = "Bob";
cloned.Age = 30;

Console.WriteLine(original.Name);
//Alice
Console.WriteLine(original.Age);
//25

Console.WriteLine(cloned.Name);
//Bob
Console.WriteLine(cloned.Age);
//30
```

##### 复杂对象深拷贝

```csharp
class Address
{
    public string City { get; set; }
    public string Street { get; set; }
}

class User
{
    public string Name { get; set; }
    public Address Address { get; set; }
}

var original = new User
{
    Name = "Alice",
    Address = new Address { City = "Beijing", Street = "Main Street" }
};

var cloned = original.DeepClone();

cloned.Address.City = "Shanghai";

Console.WriteLine(original.Address.City);
//Beijing

Console.WriteLine(cloned.Address.City);
//Shanghai
```

##### 集合深拷贝

```csharp
var original = new List<int> { 1, 2, 3, 4, 5 };
var cloned = original.DeepClone();

cloned.Add(6);

Console.WriteLine(string.Join(", ", original));
//1, 2, 3, 4, 5

Console.WriteLine(string.Join(", ", cloned));
//1, 2, 3, 4, 5, 6
```

##### 字典深拷贝

```csharp
var original = new Dictionary<string, int>
{
    ["one"] = 1,
    ["two"] = 2,
    ["three"] = 3
};

var cloned = original.DeepClone();

cloned["four"] = 4;

Console.WriteLine(original.Count);
//3

Console.WriteLine(cloned.Count);
//4
```

## 注意事项

- 深拷贝使用序列化和反序列化实现，要求对象必须可序列化
- 对于复杂对象图，深拷贝可能会产生性能开销
- 确保对象的所有属性都是可序列化的

## 相关文档

- [JSON 操作](json.md)
- [基础扩展](../README.md#基础扩展)
