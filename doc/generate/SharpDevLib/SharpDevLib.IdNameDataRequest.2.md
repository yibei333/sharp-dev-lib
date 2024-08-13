###### [主页](./Index.md "主页")

## IdNameDataRequest\<TId, TData\> 类

### 定义

**程序集** : [SharpDevLib.dll](./SharpDevLib.assembly.md "SharpDevLib.dll")

**命名空间** : [SharpDevLib](./SharpDevLib.namespace.md "SharpDevLib")

**继承** : [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object") ↣ [BaseRequest](./SharpDevLib.BaseRequest.md "BaseRequest") ↣ [IdRequest](./SharpDevLib.IdRequest.1.md "IdRequest")\<TId\> ↣ [IdDataRequest](./SharpDevLib.IdDataRequest.2.md "IdDataRequest")\<TId, TData\>

``` csharp
public class IdNameDataRequest<TId, TData> : IdDataRequest<TId, TData>
```

**注释**

*id name data request*


**泛型参数**

|名称|注释|约束|
|---|---|---|
|TId|id type|-|
|TData|data type|-|




### 构造函数

|方法|注释|参数|
|---|---|---|
|[IdNameDataRequest()](./SharpDevLib.IdNameDataRequest.2.ctor.IdNameDataRequest.md "IdNameDataRequest()")|实例化id name data requesst|-|
|[IdNameDataRequest(TData data)](./SharpDevLib.IdNameDataRequest.2.ctor.IdNameDataRequest.TData.md "IdNameDataRequest(TData data)")|实例化id name data requesst|data:data|
|[IdNameDataRequest(TId id)](./SharpDevLib.IdNameDataRequest.2.ctor.IdNameDataRequest.TId.md "IdNameDataRequest(TId id)")|实例化id name data requesst|id:id|
|[IdNameDataRequest(String name)](./SharpDevLib.IdNameDataRequest.2.ctor.IdNameDataRequest.String.md "IdNameDataRequest(String name)")|实例化id name data requesst|name:name|
|[IdNameDataRequest(TId id, TData data)](./SharpDevLib.IdNameDataRequest.2.ctor.IdNameDataRequest.TId.TData.md "IdNameDataRequest(TId id, TData data)")|实例化id name data requesst|id:id<br>data:data|
|[IdNameDataRequest(TId id, String name)](./SharpDevLib.IdNameDataRequest.2.ctor.IdNameDataRequest.TId.String.md "IdNameDataRequest(TId id, String name)")|实例化id name data requesst|id:id<br>name:name|
|[IdNameDataRequest(String name, TData data)](./SharpDevLib.IdNameDataRequest.2.ctor.IdNameDataRequest.String.TData.md "IdNameDataRequest(String name, TData data)")|实例化id name data requesst|name:name<br>data:data|
|[IdNameDataRequest(TId id, String name, TData data)](./SharpDevLib.IdNameDataRequest.2.ctor.IdNameDataRequest.TId.String.TData.md "IdNameDataRequest(TId id, String name, TData data)")|实例化id name data requesst|id:id<br>name:name<br>data:data|


### 属性

|名称|类型|是否静态|注释|
|---|---|---|---|
|[Name](./SharpDevLib.IdNameDataRequest.2.Name.md "Name")|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|`否`|name|
|[Data](./SharpDevLib.IdDataRequest.2.Data.md "Data")&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[IdDataRequest](./SharpDevLib.IdDataRequest.2.md "IdDataRequest")\<TId, TData\>)*|TData|`否`|data|
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


