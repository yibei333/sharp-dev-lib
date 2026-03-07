# 字符串操作

SharpDevLib 提供了丰富的字符串扩展方法，包括空值检查、类型转换、字符串处理、路径处理等功能。

## 空值检查

### IsNullOrEmpty

```csharp
string? str = "";

// 检查是否为 null 或空
var isEmpty = str.IsNullOrEmpty();
// 结果: true
```

### NotNullOrEmpty

```csharp
string? str = "hello";

// 检查是否不为 null 且不为空
var isNotEmpty = str.NotNullOrEmpty();
// 结果: true
```

### IsNullOrWhiteSpace

```csharp
string? str = "  ";

// 检查是否为 null 或空白
var isEmpty = str.IsNullOrWhiteSpace();
// 结果: true

var isEmpty = "hello".IsNullOrWhiteSpace();
// 结果: false
```

### NotNullOrWhiteSpace

```csharp
string? str = "hello";

// 检查是否不为 null 且不为空白
var isNotEmpty = str.NotNullOrWhiteSpace();
// 结果: true
```

## 类型转换

### ToGuid

```csharp
// 无效 Guid 返回 Empty
var guid = "invalid".ToGuid();
// 结果: 00000000-0000-0000-0000-000000000000

// 有效 Guid
var guid = "b7fe8157-1d89-4e11-8ac1-460ef4ab0cf5".ToGuid();
// 结果: b7fe8157-1d89-4e11-8ac1-460ef4ab0cf5

// 抛出异常（如果无效）
var guid = "invalid".ToGuid(throwException: true);
// 抛出: InvalidCastException
```

### ToBoolean

```csharp
// 转换为 bool
var boolValue = "true".ToBoolean();
// 结果: true

var boolValue = "false".ToBoolean();
// 结果: false

var boolValue = "anything".ToBoolean();
// 结果: false
```

### ToInt

```csharp
// 转换为 int（失败时返回默认值）
var intValue = "123".ToInt();
// 结果: 123

var intValue = "abc".ToInt(10);
// 结果: 10

var intValue = "456".ToInt(0);
// 结果: 456
```

### ToIntThrow

```csharp
// 转换为 int（失败时抛出异常）
var intValue = "123".ToIntThrow();
// 结果: 123

var intValue = "abc".ToIntThrow();
// 抛出: InvalidCastException
```

### ToDecimal

```csharp
// 转换为 decimal（失败时返回默认值）
var decimalValue = "123.45".ToDecimal();
// 结果: 123.45

var decimalValue = "abc".ToDecimal(0);
// 结果: 0
```

### ToDecimalThrow

```csharp
// 转换为 decimal（失败时抛出异常）
var decimalValue = "123.45".ToDecimalThrow();
// 结果: 123.45
```

### ToDouble

```csharp
// 转换为 double（失败时返回默认值）
var doubleValue = "123.45".ToDouble();
// 结果: 123.45

var doubleValue = "abc".ToDouble(0);
// 结果: 0
```

### ToDoubleThrow

```csharp
// 转换为 double（失败时抛出异常）
var doubleValue = "123.45".ToDoubleThrow();
// 结果: 123.45
```

## 分割操作

### SplitToList

```csharp
// 分割为字符串列表
var list = "a,b,c".SplitToList(',');
// 结果: ["a", "b", "c"]

// 使用多个分隔符
var list = "a,b;c,d".SplitToList(new[] { ',', ';' });
// 结果: ["a", "b", "c", "d"]

// 不移除空项
var list = "a,,b,,c".SplitToList(',', removeEmptyEntries: false);
// 结果: ["a", "", "b", "", "c"]

// 不去重
var list = "a,b,a,c,b".SplitToList(',', distinct: false);
// 结果: ["a", "b", "a", "c", "b"]
```

### SplitToGuidList

```csharp
// 分割为 Guid 列表
var list = "b7fe8157-1d89-4e11-8ac1-460ef4ab0cf5,b7fe8157-1d89-4e11-8ac1-460ef4ab0cf5"
    .SplitToGuidList(',', removeEmptyEntries: true, distinct: true);

// 使用多个分隔符
var list = "guid1,guid2;guid3".SplitToGuidList(new[] { ',', ';' });
```

## 字符串处理

### TrimStart

```csharp
// 去除开头指定字符
var result = "foofoobar".TrimStart("foo");
// 结果: "foobar"

var result = "  hello".TrimStart(" ");
// 结果: "hello"
```

### TrimEnd

```csharp
// 去除结尾指定字符
var result = "foofoobar".TrimEnd("bar");
// 结果: "foofoo"

var result = "hello  ".TrimEnd(" ");
// 结果: "hello"
```

### Escape

```csharp
// 转义特殊字符
var escaped = "hello\"world".Escape();
// 结果: "hello\"world"
```

### RemoveEscape

```csharp
// 去除转义
var unescaped = "hello\\\"world".RemoveEscape();
// 结果: "hello"world"
```

### RemoveLineBreak

```csharp
// 删除换行
var result = "line1\nline2".RemoveLineBreak();
// 结果: "line1line2"

var result = "line1\r\nline2".RemoveLineBreak();
// 结果: "line1line2"
```

### RemoveSpace

```csharp
// 删除空格
var result = "hello world".RemoveSpace();
// 结果: "helloworld"
```

## 路径处理

### GetUrlRelativePath

```csharp
// 获取 URL 相对路径
var relativePath = "/a/b/c".GetUrlRelativePath("/a/b/d");
// 结果: "../d"

var relativePath = "/a/b/c/d".GetUrlRelativePath("/a/b/c/e/f");
// 结果: "../e/f"

var relativePath = "/a/b".GetUrlRelativePath("/a/c");
// 结果: "../c"
```

### GetUrlCommonPrefix

```csharp
// 获取 URL 相同前缀
var prefix = "/a/b/c".GetUrlCommonPrefix("/a/b/d");
// 结果: "a/b"

var prefix = "/api/v1/users".GetUrlCommonPrefix("/api/v1/products");
// 结果: "api/v1"
```

### GetCommonPrefix

```csharp
// 获取字符串相同前缀
var prefix = "hello123".GetCommonPrefix("hello456");
// 结果: "hello"

var prefix = "apple".GetCommonPrefix("apricot");
// 结果: "ap"
```

## 相关文档

- [Json](json.md)
- [枚举](enum.md)
- [基础扩展](../README.md#基础扩展)
