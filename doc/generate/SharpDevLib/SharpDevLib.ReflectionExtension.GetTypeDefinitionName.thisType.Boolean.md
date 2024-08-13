###### [主页](./Index.md "主页")
#### GetTypeDefinitionName(this Type type, Boolean isFullName) 方法
**程序集** : [SharpDevLib.dll](./SharpDevLib.assembly.md "SharpDevLib.dll")
**命名空间** : [SharpDevLib](./SharpDevLib.namespace.md "SharpDevLib")
**所属类型** : [ReflectionExtension](./SharpDevLib.ReflectionExtension.md "ReflectionExtension")
``` csharp
public static String GetTypeDefinitionName(this Type type, Boolean isFullName)
```
**注释**
*获取类型定义名称(支持泛型,不支持嵌套类型)*

**返回类型** : [String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")

**参数**
|名称|类型|注释|
|---|---|---|
|type|[Type](https://learn.microsoft.com/en-us/dotnet/api/system.type "Type")|类型|
|isFullName|[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean "Boolean")|是否全名|
