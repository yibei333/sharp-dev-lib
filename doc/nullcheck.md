# 空值断言

SharpDevLib 提供了字符串的空值和空白字符判断扩展方法。

## 判断为空

##### 判断是否为null或空字符串

```csharp
string str = null;
var result = str.IsNullOrEmpty();
Console.WriteLine(result);
//True
```

```csharp
string str = "";
var result = str.IsNullOrEmpty();
Console.WriteLine(result);
//True
```

```csharp
string str = "Hello";
var result = str.IsNullOrEmpty();
Console.WriteLine(result);
//False
```

##### 判断是否为null或空白字符串

```csharp
string str = null;
var result = str.IsNullOrWhiteSpace();
Console.WriteLine(result);
//True
```

```csharp
string str = "";
var result = str.IsNullOrWhiteSpace();
Console.WriteLine(result);
//True
```

```csharp
string str = "   ";
var result = str.IsNullOrWhiteSpace();
Console.WriteLine(result);
//True
```

```csharp
string str = "Hello";
var result = str.IsNullOrWhiteSpace();
Console.WriteLine(result);
//False
```

## 判断不为空

##### 判断是否不为null且不为空字符串

```csharp
string str = "Hello";
var result = str.NotNullOrEmpty();
Console.WriteLine(result);
//True
```

```csharp
string str = null;
var result = str.NotNullOrEmpty();
Console.WriteLine(result);
//False
```

```csharp
string str = "";
var result = str.NotNullOrEmpty();
Console.WriteLine(result);
//False
```

##### 判断是否不为null且不为空白字符串

```csharp
string str = "Hello";
var result = str.NotNullOrWhiteSpace();
Console.WriteLine(result);
//True
```

```csharp
string str = "   Hello   ";
var result = str.NotNullOrWhiteSpace();
Console.WriteLine(result);
//True
```

```csharp
string str = "   ";
var result = str.NotNullOrWhiteSpace();
Console.WriteLine(result);
//False
```

```csharp
string str = null;
var result = str.NotNullOrWhiteSpace();
Console.WriteLine(result);
//False
```

## 实际应用

##### 条件判断

```csharp
string? input = GetUserInput();

if (input.NotNullOrWhiteSpace())
{
    ProcessInput(input);
}
```

##### 默认值处理

```csharp
string? configValue = GetConfigValue();

var value = configValue.NotNullOrWhiteSpace() ? configValue : "default";
```

##### 验证输入

```csharp
bool ValidateInput(string? input)
{
    if (input.IsNullOrWhiteSpace())
    {
        Console.WriteLine("输入不能为空");
        return false;
    }
    return true;
}
```

##### 集合过滤

```csharp
var list = new List<string?> { "Hello", "", "World", null, "   " };

var validItems = list.Where(item => item.NotNullOrWhiteSpace()).ToList();
Console.WriteLine(string.Join(", ", validItems));
//Hello, World
```

## 相关文档

- [字符串操作](string.md)
- [基础扩展](../README.md#基础扩展)
