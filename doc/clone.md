# 克隆操作

SharpDevLib 提供了对象的深拷贝功能。

## DeepClone

### 基本深拷贝

```csharp
public class User
{
    public string Name { get; set; }
    public int Age { get; set; }
}

var user1 = new User { Name = "张三", Age = 25 };
var user2 = user1.DeepClone();

// user2 是 user1 的深拷贝
Console.WriteLine(user2.Name);  // 输出: 张三
Console.WriteLine(user2.Age);   // 输出: 25

// 修改 user2 不会影响 user1
user2.Name = "李四";
Console.WriteLine(user1.Name);  // 输出: 张三
Console.WriteLine(user2.Name);  // 输出: 李四
```

### 复杂对象深拷贝

```csharp
public class Address
{
    public string City { get; set; }
    public string Street { get; set; }
}

public class Person
{
    public string Name { get; set; }
    public Address Address { get; set; }
    public List<string> Hobbies { get; set; }
}

var person1 = new Person
{
    Name = "张三",
    Address = new Address { City = "北京", Street = "长安街" },
    Hobbies = new List<string> { "阅读", "运动" }
};

var person2 = person1.DeepClone();

// 修改 person2 的嵌套对象
person2.Address.City = "上海";
person2.Hobbies.Add("音乐");

// person1 不受影响
Console.WriteLine(person1.Address.City);  // 输出: 北京
Console.WriteLine(person1.Hobbies.Count);  // 输出: 2

Console.WriteLine(person2.Address.City);  // 输出: 上海
Console.WriteLine(person2.Hobbies.Count);  // 输出: 3
```

### 集合深拷贝

```csharp
var list1 = new List<User>
{
    new User { Name = "张三", Age = 25 },
    new User { Name = "李四", Age = 30 }
};

var list2 = list1.DeepClone();

// 修改 list2 不会影响 list1
list2[0].Name = "王五";
list2.Add(new User { Name = "赵六", Age = 35 });

Console.WriteLine(list1.Count);  // 输出: 2
Console.WriteLine(list1[0].Name);  // 输出: 张三

Console.WriteLine(list2.Count);  // 输出: 3
Console.WriteLine(list2[0].Name);  // 输出: 王五
```

### 字典深拷贝

```csharp
var dict1 = new Dictionary<string, User>
{
    ["user1"] = new User { Name = "张三", Age = 25 },
    ["user2"] = new User { Name = "李四", Age = 30 }
};

var dict2 = dict1.DeepClone();

// 修改 dict2 不会影响 dict1
dict2["user1"].Name = "王五";
dict2.Add("user3", new User { Name = "赵六", Age = 35 });

Console.WriteLine(dict1.Count);  // 输出: 2
Console.WriteLine(dict1["user1"].Name);  // 输出: 张三

Console.WriteLine(dict2.Count);  // 输出: 3
Console.WriteLine(dict2["user1"].Name);  // 输出: 王五
```

## 完整示例

### 备份对象状态

```csharp
public class Config
{
    public string Server { get; set; }
    public int Port { get; set; }
    public bool EnableSsl { get; set; }
}

var currentConfig = new Config
{
    Server = "localhost",
    Port = 8080,
    EnableSsl = true
};

// 创建备份
var backupConfig = currentConfig.DeepClone();

// 修改配置
currentConfig.Server = "192.168.1.1";
currentConfig.Port = 9000;

// 需要时恢复
currentConfig = backupConfig.DeepClone();
```

### 撤销操作

```csharp
public class Document
{
    public string Title { get; set; }
    public string Content { get; set; }
}

var document = new Document
{
    Title = "文档标题",
    Content = "文档内容"
};

// 保存历史记录
var history = new List<Document> { document.DeepClone() };

// 修改文档
document.Content = "新的文档内容";
history.Add(document.DeepClone());

// 再次修改
document.Content = "再次修改的内容";
history.Add(document.DeepClone());

// 撤销到上一步
document = history[^2].DeepClone();

// 撤销到初始状态
document = history[0].DeepClone();
```

## 注意事项

### 深拷贝原理

`DeepClone` 方法使用 JSON 序列化和反序列化实现：

1. 将对象序列化为 JSON 字符串
2. 将 JSON 字符串反序列化为新对象

### 限制

- 对象必须是可序列化的（有公共属性）
- 循环引用会导致错误
- 私有字段不会被拷贝
- 方法、事件等不会被拷贝
- 派生类会被转换为基类类型（如果有）

### 性能考虑

对于大型对象或频繁的拷贝操作，建议考虑其他性能更高的深拷贝实现方式。

## 相关文档

- [Json](json.md)
- [反射](reflection.md)
- [基础扩展](../README.md#基础扩展)
