###### [主页](./Index.md "主页")
## BaseReply 类
### 定义
**程序集** : [SharpDevLib.dll](./SharpDevLib.assembly.md "SharpDevLib.dll")
**命名空间** : [SharpDevLib](./SharpDevLib.namespace.md "SharpDevLib")
**继承** : [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object")
**派生** : [DataReply](./SharpDevLib.DataReply.1.md "DataReply")\<TData\>, [EmptyReply](./SharpDevLib.EmptyReply.md "EmptyReply"), [PageReply](./SharpDevLib.PageReply.1.md "PageReply")\<TData\>
``` csharp
public abstract class BaseReply : Object
```
**注释**
*响应基类,或许可以在反射或者泛型中用*

### 属性
|名称|类型|是否静态|注释|
|---|---|---|---|
|[Success](./SharpDevLib.BaseReply.Success.md "Success")|[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean "Boolean")|`否`|是否成功|
|[Description](./SharpDevLib.BaseReply.Description.md "Description")|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|`否`|描述|
|[ExtraData](./SharpDevLib.BaseReply.ExtraData.md "ExtraData")|[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object")|`否`|额外字段|

### 方法
|方法|返回类型|Accessor|是否静态|参数|
|---|---|---|---|---|
|GetType()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Type](https://learn.microsoft.com/en-us/dotnet/api/system.type "Type")|`public`|`否`|-|
|MemberwiseClone()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object")|`protected`|`否`|-|
|Finalize()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Void](https://learn.microsoft.com/en-us/dotnet/api/system.void "Void")|`protected`|`否`|-|
|ToString()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|`public`|`否`|-|
|Equals(Object obj)&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean "Boolean")|`public`|`否`|-|
|GetHashCode()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 "Int32")|`public`|`否`|-|

