# 反射操作

SharpDevLib 提供了类型、方法和构造函数的反射信息获取功能。

## 类型信息

### GetTypeDefinitionName

```csharp
// 获取类型定义名称（简单名称）
var type = typeof(List<string>);
var name = type.GetTypeDefinitionName();
// 结果: "List<String>"

type = typeof(Dictionary<int, string>);
name = type.GetTypeDefinitionName();
// 结果: "Dictionary<Int32, String>"

type = typeof(A<B<int>, C<string>>);
name = type.GetTypeDefinitionName();
// 结果: "A<Int32, C<String>>"
```

### GetTypeDefinitionName (完全限定名)

```csharp
// 获取类型定义名称（完全限定名）
var type = typeof(List<string>);
var name = type.GetTypeDefinitionName(isFullName: true);
// 结果: "System.Collections.Generic.List<System.String>"

type = typeof(A<B<int>, C<string>>);
name = type.GetTypeDefinitionName(isFullName: true);
// 结果: "Namespace.A<Namespace.B<System.Int32>, Namespace.C<System.String>>"
```

### 对象的 GetTypeDefinitionName

```csharp
// 获取对象的类型定义名称
var list = new List<int>();
var name = list.GetTypeDefinitionName();
// 结果: "List<Int32>"

var obj = new { Name = "张三", Age = 25 };
name = obj.GetTypeDefinitionName();
// 结果: 匿名类型的名称

var obj = null;
name = obj.GetTypeDefinitionName();
// 结果: "" (空字符串)
```

## 方法信息

### GetMethodDefinitionName

```csharp
// 获取方法定义名称（不包含参数名）
var method = typeof(string).GetMethod("ToUpper");
var name = method.GetMethodDefinitionName(containParameterName: false);
// 结果: "ToUpper()"

// 获取方法定义名称（包含参数名）
name = method.GetMethodDefinitionName(containParameterName: true);
// 结果: "ToUpper()"

// 泛型方法
var method = typeof(Enumerable).GetMethod("FirstOrDefault");
name = method.GetMethodDefinitionName(containParameterName: false, isFullName: false);
// 结果: "FirstOrDefault(IEnumerable<TSource>)"

name = method.GetMethodDefinitionName(containParameterName: true, isFullName: false);
// 结果: "FirstOrDefault(IEnumerable<TSource> source)"
```

### GetMethodDefinitionName (完全限定名)

```csharp
// 获取方法定义名称（完全限定名）
var method = typeof(string).GetMethod("Compare");
var name = method.GetMethodDefinitionName(containParameterName: false, isFullName: true);
// 结果: "Compare(String, String)"

name = method.GetMethodDefinitionName(containParameterName: false, isFullName: true);
// 结果: "Compare(System.String, System.String)"
```

### 扩展方法

```csharp
// 获取扩展方法定义名称
var method = typeof(Enumerable).GetMethod("Select");
var name = method.GetMethodDefinitionName(containParameterName: true, isFullName: false);
// 结果: "Select(IEnumerable<TSource> source, Func<TSource, TResult> selector)"
```

## 构造函数信息

### GetConstructorDefinitionName

```csharp
// 获取构造函数定义名称（不包含参数名）
var constructor = typeof(List<int>).GetConstructor(new[] { typeof(int) });
var name = constructor.GetConstructorDefinitionName(containParameterName: false);
// 结果: "List(Int32)"

// 获取构造函数定义名称（包含参数名）
name = constructor.GetConstructorDefinitionName(containParameterName: true);
// 结果: "List(Int32 capacity)"

// 无参构造函数
constructor = typeof(List<int>).GetConstructor(Type.EmptyTypes);
name = constructor.GetConstructorDefinitionName(containParameterName: false);
// 结果: "List()"
```

### GetConstructorDefinitionName (完全限定名)

```csharp
// 获取构造函数定义名称（完全限定名）
var constructor = typeof(List<int>).GetConstructor(new[] { typeof(int) });
var name = constructor.GetConstructorDefinitionName(containParameterName: false, isFullName: true);
// 结果: "List(System.Int32)"
```

## 完整示例

### 获取类型信息

```csharp
var type = typeof(Dictionary<string, List<int>>);

var simpleName = type.GetTypeDefinitionName();
Console.WriteLine($"简单名称: {simpleName}");

var fullName = type.GetTypeDefinitionName(isFullName: true);
Console.WriteLine($"完全限定名: {fullName}");
```

### 获取方法信息

```csharp
var type = typeof(string);
var method = type.GetMethod("Concat", new[] { typeof(string), typeof(string) });

var simpleName = method.GetMethodDefinitionName(containParameterName: false);
Console.WriteLine($"简单名称: {simpleName}");

var withParams = method.GetMethodDefinitionName(containParameterName: true);
Console.WriteLine($"带参数名: {withParams}");
```

### 获取构造函数信息

```csharp
var type = typeof(List<string>);

// 无参构造函数
var constructor = type.GetConstructor(Type.EmptyTypes);
var name = constructor.GetConstructorDefinitionName(containParameterName: false);
Console.WriteLine($"无参构造函数: {name}");

// 带参数构造函数
constructor = type.GetConstructor(new[] { typeof(int) });
name = constructor.GetConstructorDefinitionName(containParameterName: true);
Console.WriteLine($"带参数构造函数: {name}");
```

## 相关文档

- [克隆](clone.md)
- [基础扩展](../README.md#基础扩展)
