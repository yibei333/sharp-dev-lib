###### [主页](./Index.md "主页")

## DataReply\<TData\> 类

### 定义

**程序集** : [SharpDevLib.dll](./SharpDevLib.assembly.md "SharpDevLib.dll")

**命名空间** : [SharpDevLib](./SharpDevLib.namespace.md "SharpDevLib")

**继承** : [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object") ↣ [BaseReply](./SharpDevLib.BaseReply.md "BaseReply")

``` csharp
public class DataReply<TData> : BaseReply
```

**注释**

*响应*


**泛型参数**

|名称|注释|约束|
|---|---|---|
|TData|数据类型|-|




### 构造函数

|方法|注释|参数|
|---|---|---|
|[DataReply()](./SharpDevLib.DataReply.1.ctor.DataReply.md "DataReply()")|默认构造函数|-|


### 属性

|名称|类型|是否静态|注释|
|---|---|---|---|
|[Data](./SharpDevLib.DataReply.1.Data.md "Data")|TData|`否`|data|
|[Success](./SharpDevLib.BaseReply.Success.md "Success")&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[BaseReply](./SharpDevLib.BaseReply.md "BaseReply"))*|[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean "Boolean")|`否`|是否成功|
|[Description](./SharpDevLib.BaseReply.Description.md "Description")&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[BaseReply](./SharpDevLib.BaseReply.md "BaseReply"))*|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|`否`|描述|
|[ExtraData](./SharpDevLib.BaseReply.ExtraData.md "ExtraData")&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[BaseReply](./SharpDevLib.BaseReply.md "BaseReply"))*|[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object")|`否`|额外字段|


### 方法

|方法|返回类型|Accessor|是否静态|参数|
|---|---|---|---|---|
|[Succeed(TData data, String description)](./SharpDevLib.DataReply.1.Succeed.TData.String.md "Succeed(TData data, String description)")|[DataReply](./SharpDevLib.DataReply.1.md "DataReply")\<TData\>|`public`|`是`|data:data<br>description:描述|
|[Failed(String description)](./SharpDevLib.DataReply.1.Failed.String.md "Failed(String description)")|[DataReply](./SharpDevLib.DataReply.1.md "DataReply")\<TData\>|`public`|`是`|description:描述|
|GetType()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Type](https://learn.microsoft.com/en-us/dotnet/api/system.type "Type")|`public`|`否`|-|
|MemberwiseClone()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object")|`protected`|`否`|-|
|Finalize()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Void](https://learn.microsoft.com/en-us/dotnet/api/system.void "Void")|`protected`|`否`|-|
|ToString()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|`public`|`否`|-|
|Equals(Object obj)&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean "Boolean")|`public`|`否`|-|
|GetHashCode()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 "Int32")|`public`|`否`|-|


