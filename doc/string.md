# 字符串操作

SharpDevLib 提供了丰富的字符串扩展方法，包括字符串处理、转换和验证功能。

## 字符串处理

##### 删除前缀

```csharp
var source = "  /api/v1/users  ";
var result = source.TrimStart("/api/v1/");
Console.WriteLine(result);
//打印:users
//自动处理前后的空白字符
```

##### 删除后缀

```csharp
var source = "  /api/v1/users  ";
var result = source.TrimEnd("/users");
Console.WriteLine(result);
///api/v1
//自动处理前后的空白字符
```

##### 获取公共前缀

```csharp
var str1 = "HelloWorld";
var str2 = "HelloSharp";
var commonPrefix = str1.GetCommonPrefix(str2);
Console.WriteLine(commonPrefix);
//Hello
```

##### 获取URL公共前缀

```csharp
var url1 = "https://example.com/api/v1/users";
var url2 = "https://example.com/api/v1/products";
var commonPrefix = url1.GetUrlCommonPrefix(url2);
Console.WriteLine(commonPrefix);
//https://example.com/api/v1
```

##### 获取URL相对路径

```csharp
var sourcePath = "https://example.com/api/v1/users";
var targetPath = "https://example.com/api/v1/products";
var relativePath = sourcePath.GetUrlRelativePath(targetPath);
Console.WriteLine(relativePath);
//../products
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
```

##### 转换为Guid（失败时抛出异常）

```csharp
var str = "invalid-guid";
var guid = str.ToGuid(throwException: true);
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
```

## 字符串分割

##### 分割为字符串列表

```csharp
var str = "apple,banana,orange";
var list = str.SplitToList(',');
Console.WriteLine(string.Join(", ", list));
//apple, banana, orange
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

##### 使用多个分隔符分割

```csharp
var str = "apple,banana;orange|pear";
var list = str.SplitToList([',', ';', '|']);
Console.WriteLine(string.Join(", ", list));
//apple, banana, orange, pear
```

##### 分割时不去重

```csharp
var str = "apple,apple,banana,banana";
var list = str.SplitToList(',', distinct: false);
Console.WriteLine(string.Join(", ", list));
//apple, apple, banana, banana
```

##### 分割时保留空项

```csharp
var str = "apple,,banana,";
var list = str.SplitToList(',', removeEmptyEntries: false);
Console.WriteLine(string.Join(", ", list));
//apple, , banana, 
```

## 相关文档

- [集合操作](collection.md)
- [JSON 操作](json.md)
- [基础扩展](../README.md#基础扩展)
