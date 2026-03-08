# 反射操作

SharpDevLib 提供了类型、方法和构造函数的反射信息获取功能。

## 类型信息

##### 获取类型定义名称

```csharp
var type = typeof(List<string>);
var name = type.GetTypeDefinitionName();
Console.WriteLine(name);
//List<String>
```

##### 获取完整类型名称

```csharp
var type = typeof(List<string>);
var fullName = type.GetTypeDefinitionName(isFullName: true);
Console.WriteLine(fullName);
//System.Collections.Generic.List<System.String>
```

##### 从对象获取类型名称

```csharp
var list = new List<int>();
var typeName = list.GetTypeDefinitionName();
Console.WriteLine(typeName);
//List<Int32>
```

##### 多个泛型参数

```csharp
var type = typeof(Dictionary<string, int>);
var name = type.GetTypeDefinitionName();
Console.WriteLine(name);
//Dictionary<String, Int32>
```

##### 嵌套泛型

```csharp
var type = typeof(List<List<string>>);
var name = type.GetTypeDefinitionName();
Console.WriteLine(name);
//List<List<String>>
```

## 方法信息

##### 获取方法定义名称（不含参数名）

```csharp
var method = typeof(string).GetMethod("Substring", new[] { typeof(int), typeof(int) });
var name = method.GetMethodDefinitionName(containParameterName: false);
Console.WriteLine(name);
//Substring(Int32, Int32)
```

##### 获取方法定义名称（含参数名）

```csharp
var method = typeof(string).GetMethod("Substring", new[] { typeof(int), typeof(int) });
var name = method.GetMethodDefinitionName(containParameterName: true);
Console.WriteLine(name);
//Substring(Int32 startIndex, Int32 length)
```

##### 泛型方法

```csharp
var method = typeof(Enumerable).GetMethod("Empty");
var name = method.GetMethodDefinitionName(containParameterName: false, isFullName: true);
Console.WriteLine(name);
//Empty<TElement>()
```

##### 扩展方法

```csharp
var method = typeof(StringHelper).GetMethod("TrimStart", new[] { typeof(string), typeof(string) });
var name = method.GetMethodDefinitionName(containParameterName: false);
Console.WriteLine(name);
//TrimStart(this String, String)
```

## 构造函数信息

##### 获取构造函数定义名称（不含参数名）

```csharp
var constructor = typeof(List<string>).GetConstructor(new[] { typeof(int) });
var name = constructor.GetConstructorDefinitionName(containParameterName: false);
Console.WriteLine(name);
//List(Int32)
```

##### 获取构造函数定义名称（含参数名）

```csharp
var constructor = typeof(List<string>).GetConstructor(new[] { typeof(int) });
var name = constructor.GetConstructorDefinitionName(containParameterName: true);
Console.WriteLine(name);
//List(Int32 capacity)
```

##### 无参构造函数

```csharp
var constructor = typeof(string).GetConstructor(Type.EmptyTypes);
var name = constructor.GetConstructorDefinitionName(containParameterName: false);
Console.WriteLine(name);
//String()
```

##### 多个参数的构造函数

```csharp
var constructor = typeof(DateTime).GetConstructor(new[] { typeof(int), typeof(int), typeof(int) });
var name = constructor.GetConstructorDefinitionName(containParameterName: true);
Console.WriteLine(name);
//DateTime(Int32 year, Int32 month, Int32 day)
```

## 完整类型名称

##### 使用完整类型名称

```csharp
var method = typeof(string).GetMethod("Substring", new[] { typeof(int), typeof(int) });
var name = method.GetMethodDefinitionName(containParameterName: false, isFullName: true);
Console.WriteLine(name);
//Substring(System.Int32, System.Int32)
```

##### 混合使用

```csharp
var type = typeof(Dictionary<Dictionary<string, int>, List<bool>>);
var name = type.GetTypeDefinitionName(isFullName: true);
Console.WriteLine(name);
//System.Collections.Generic.Dictionary<System.Collections.Generic.Dictionary<System.String, System.Int32>, System.Collections.Generic.List<System.Boolean>>
```

## 相关文档

- [字符串操作](string.md)
- [基础扩展](../README.md#基础扩展)
