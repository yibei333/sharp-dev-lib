###### [主页](./Index.md "主页")

## IdDataRequest\<TId, TData\> 类

### 定义

**程序集** : [SharpDevLib.dll](./SharpDevLib.assembly.md "SharpDevLib.dll")

**命名空间** : [SharpDevLib](./SharpDevLib.namespace.md "SharpDevLib")

**继承** : [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object") ↣ [BaseRequest](./SharpDevLib.BaseRequest.md "BaseRequest") ↣ [IdRequest](./SharpDevLib.IdRequest.1.md "IdRequest")\<TId\>

**派生** : [IdNameDataRequest](./SharpDevLib.IdNameDataRequest.2.md "IdNameDataRequest")\<TId, TData\>

``` csharp
public class IdDataRequest<TId, TData> : IdRequest<TId>
```

**注释**

*id data requesst*


**泛型参数**

|名称|注释|约束|
|---|---|---|
|TId|id type|-|
|TData|data type|-|




### 构造函数

|方法|注释|参数|
|---|---|---|
|[IdDataRequest()](./SharpDevLib.IdDataRequest.2.ctor.IdDataRequest.md "IdDataRequest()")|实例化id data requesst|-|
|[IdDataRequest(TData data)](./SharpDevLib.IdDataRequest.2.ctor.IdDataRequest.TData.md "IdDataRequest(TData data)")|实例化id data requesst|data:data|
|[IdDataRequest(TId id)](./SharpDevLib.IdDataRequest.2.ctor.IdDataRequest.TId.md "IdDataRequest(TId id)")|实例化id data requesst|id:id|
|[IdDataRequest(TId id, TData data)](./SharpDevLib.IdDataRequest.2.ctor.IdDataRequest.TId.TData.md "IdDataRequest(TId id, TData data)")|实例化id data requesst|id:id<br>data:data|


### 属性

|名称|类型|是否静态|注释|
|---|---|---|---|
|[Data](./SharpDevLib.IdDataRequest.2.Data.md "Data")|TData|`否`|data|
|[Id](./SharpDevLib.IdRequest.1.Id.md "Id")&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[IdRequest](./SharpDevLib.IdRequest.1.md "IdRequest")\<TId\>)*|TId|`否`|id|


### 方法

|方法|返回类型|Accessor|是否静态|参数|
|---|---|---|---|---|
|GetType()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Type](https://learn.microsoft.com/en-us/dotnet/api/system.type "Type")|`public`|`否`|-|
|MemberwiseClone()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object")|`protected`|`否`|-|
|Finalize()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Void](https://learn.microsoft.com/en-us/dotnet/api/system.void "Void")|`protected`|`否`|-|
|ToString()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|`public`|`否`|-|
|Equals(Object obj)&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean "Boolean")|`public`|`否`|-|
|GetHashCode()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 "Int32")|`public`|`否`|-|


