###### [主页](./Index.md "主页")

## IdNameDataDto\<TId, TData\> 类

### 定义

**程序集** : [SharpDevLib.dll](./SharpDevLib.assembly.md "SharpDevLib.dll")

**命名空间** : [SharpDevLib](./SharpDevLib.namespace.md "SharpDevLib")

**继承** : [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object") ↣ [BaseDto](./SharpDevLib.BaseDto.md "BaseDto") ↣ [DataDto](./SharpDevLib.DataDto.1.md "DataDto")\<TData\>

``` csharp
public class IdNameDataDto<TId, TData> : DataDto<TData>
```

**注释**

*id name data dto*


**泛型参数**

|名称|注释|约束|
|---|---|---|
|TId|id type|-|
|TData|data type|-|




### 构造函数

|方法|注释|参数|
|---|---|---|
|[IdNameDataDto()](./SharpDevLib.IdNameDataDto.2.ctor.IdNameDataDto.md "IdNameDataDto()")|实例化id name data dto|-|
|[IdNameDataDto(TData data)](./SharpDevLib.IdNameDataDto.2.ctor.IdNameDataDto.TData.md "IdNameDataDto(TData data)")|实例化id name data dto|data:data|
|[IdNameDataDto(TId id)](./SharpDevLib.IdNameDataDto.2.ctor.IdNameDataDto.TId.md "IdNameDataDto(TId id)")|实例化id name data dto|id:id|
|[IdNameDataDto(String name)](./SharpDevLib.IdNameDataDto.2.ctor.IdNameDataDto.String.md "IdNameDataDto(String name)")|实例化id name data dto|name:name|
|[IdNameDataDto(TId id, TData data)](./SharpDevLib.IdNameDataDto.2.ctor.IdNameDataDto.TId.TData.md "IdNameDataDto(TId id, TData data)")|实例化id name data dto|id:id<br>data:data|
|[IdNameDataDto(TId id, String name)](./SharpDevLib.IdNameDataDto.2.ctor.IdNameDataDto.TId.String.md "IdNameDataDto(TId id, String name)")|实例化id name data dto|id:id<br>name:name|
|[IdNameDataDto(String name, TData data)](./SharpDevLib.IdNameDataDto.2.ctor.IdNameDataDto.String.TData.md "IdNameDataDto(String name, TData data)")|实例化id name data dto|name:name<br>data:data|
|[IdNameDataDto(TId id, String name, TData data)](./SharpDevLib.IdNameDataDto.2.ctor.IdNameDataDto.TId.String.TData.md "IdNameDataDto(TId id, String name, TData data)")|实例化id name data dto|id:id<br>name:name<br>data:data|


### 属性

|名称|类型|是否静态|注释|
|---|---|---|---|
|[Id](./SharpDevLib.IdNameDataDto.2.Id.md "Id")|TId|`否`|id|
|[Name](./SharpDevLib.IdNameDataDto.2.Name.md "Name")|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|`否`|name|
|[Data](./SharpDevLib.DataDto.1.Data.md "Data")&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[DataDto](./SharpDevLib.DataDto.1.md "DataDto")\<TData\>)*|TData|`否`|data|


### 方法

|方法|返回类型|Accessor|是否静态|参数|
|---|---|---|---|---|
|GetType()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Type](https://learn.microsoft.com/en-us/dotnet/api/system.type "Type")|`public`|`否`|-|
|MemberwiseClone()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object")|`protected`|`否`|-|
|Finalize()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Void](https://learn.microsoft.com/en-us/dotnet/api/system.void "Void")|`protected`|`否`|-|
|ToString()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|`public`|`否`|-|
|Equals(Object obj)&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean "Boolean")|`public`|`否`|-|
|GetHashCode()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 "Int32")|`public`|`否`|-|


