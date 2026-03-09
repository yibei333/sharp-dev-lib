# 字符串操作

SharpDevLib 提供了丰富的字符串扩展方法，包括字符串处理、转换和验证功能。

## 字符串处理

##### 删除前缀

```csharp
var source = "/api/v1/users";
var result = source.TrimStart("/api/v1/");
Console.WriteLine(result);
//打印:users
```

##### 删除后缀

```csharp
var source = "/api/v1/users";
var result = source.TrimEnd("/users");
Console.WriteLine(result);
///api/v1
```

##### 转义字符串

```csharp
var str = "Hello \"World\"";
var escaped = str.Escape();
Console.WriteLine(escaped);
//Hello \"World\"
```

##### 去除转义

```csharp
var str = "Hello \\\"World\\\"";
var unescaped = str.RemoveEscape();
Console.WriteLine(unescaped);
//Hello "World"
```

##### 将输入字符串中的任何转义字符进行转换

```csharp
var str = "\\u5F20\\u4E09";
var unescaped = str.RegexUnescape();
Console.WriteLine(unescaped);
//张三
```

##### 删除换行

```csharp
var str = "Line1\r\nLine2\rLine3\nLine4";
var result = str.RemoveLineBreak();
Console.WriteLine(result);
//Line1Line2Line3Line4
```

##### 删除空格

```csharp
var str = "Hello World";
var result = str.RemoveSpace();
Console.WriteLine(result);
//HelloWorld
```

## 类型转换

##### 转换为Guid

```csharp
var str = "12345678-1234-1234-1234-123456789012";
var guid = str.ToGuid();
Console.WriteLine(guid);
//12345678-1234-1234-1234-123456789012

str = "invalid-guid";
guid = str.ToGuid();
Console.WriteLine(guid);
//00000000-0000-0000-0000-000000000000

str.ToGuid(true);
//抛出异常
```

##### 转换为bool

```csharp
var str = "true";
var result = str.ToBoolean();
Console.WriteLine(result);
//True

var str2 = "false";
var result2 = str2.ToBoolean();
Console.WriteLine(result2);
//False
```

##### 转换为int

```csharp
var str = "123";
var result = str.ToInt();
Console.WriteLine(result);
//123

var str2 = "invalid";
var result2 = str2.ToInt(defaultValue: -1);
Console.WriteLine(result2);
//-1

var str3 = "456";
var result3 = str3.ToIntThrow();
Console.WriteLine(result3);
//456

var str4 = "foo";
str4.ToIntThrow();
//引发异常
```

##### 转换为decimal

```csharp
var str = "123.45";
var result = str.ToDecimal();
Console.WriteLine(result);
//123.45

var str2 = "invalid";
var result2 = str2.ToDecimal(defaultValue: -1);
Console.WriteLine(result2);
//-1

var str3 = "456.78";
var result3 = str3.ToDecimalThrow();
Console.WriteLine(result3);
//456.78

var str4 = "456..78";
str4.ToDecimalThrow();
//引发异常
```

##### 转换为double

```csharp
var str = "123.45";
var result = str.ToDouble();
Console.WriteLine(result);
//123.45

var str2 = "invalid";
var result2 = str2.ToDouble(defaultValue: -1);
Console.WriteLine(result2);
//-1

var str3 = "456.78";
var result3 = str3.ToDoubleThrow();
Console.WriteLine(result3);
//456.78

var str4 = "456..78";
str4.ToDoubleThrow();
//引发异常
```

## 字符串分割

##### 分割为字符串列表

```csharp
var str = "apple,banana,orange";
var list = str.SplitToList(',');
Console.WriteLine(string.Join(", ", list));
//apple, banana, orange

str = "apple,banana;orange|pear";
list = str.SplitToList([',', ';', '|']);
Console.WriteLine(string.Join(", ", list));
//apple, banana, orange, pear

str = "apple,apple,banana,banana";
list = str.SplitToList(',', distinct: false);
Console.WriteLine(string.Join(", ", list));
//apple, apple, banana, banana

str = "apple,,banana,";
list = str.SplitToList(',', removeEmptyEntries: false);
Console.WriteLine(string.Join(", ", list));
//apple, , banana, 
```

##### 分割为Guid列表

```csharp
var str = "12345678-1234-1234-1234-123456789012,87654321-4321-4321-4321-210987654321";
var list = str.SplitToGuidList(',');
foreach (var guid in list)
{
    Console.WriteLine(guid);
}
//12345678-1234-1234-1234-123456789012
//87654321-4321-4321-4321-210987654321
```

## 相关文档

- [集合操作](collection.md)
- [JSON 操作](json.md)
- [基础扩展](../README.md#基础扩展)
