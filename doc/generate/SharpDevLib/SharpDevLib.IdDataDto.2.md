###### [主页](./Index.md "主页")
## IdDataDto\<TId, TData\> 类
### 定义
**程序集** : [SharpDevLib.dll](./SharpDevLib.assembly.md "SharpDevLib.dll")
**命名空间** : [SharpDevLib](./SharpDevLib.namespace.md "SharpDevLib")
**继承** : [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object") ↣ [BaseDto](./SharpDevLib.BaseDto.md "BaseDto") ↣ [DataDto](./SharpDevLib.DataDto.1.md "DataDto")\<TData\>
``` csharp
public class IdDataDto<TId, TData> : DataDto<TData>
```
**注释**
*id data dto*

**泛型参数**
|名称|注释|约束|
|---|---|---|
|TId|id type|-|
|TData|data type|-|


### 构造函数
|方法|注释|参数|
|---|---|---|
|[IdDataDto()](./SharpDevLib.IdDataDto.2.ctor.IdDataDto.md "IdDataDto()")|实例化id data dto|-|
|[IdDataDto(TData data)](./SharpDevLib.IdDataDto.2.ctor.IdDataDto.TData.md "IdDataDto(TData data)")|实例化id data dto|data:data|
|[IdDataDto(TId id)](./SharpDevLib.IdDataDto.2.ctor.IdDataDto.TId.md "IdDataDto(TId id)")|实例化id data dto|id:id|
|[IdDataDto(TId id, TData data)](./SharpDevLib.IdDataDto.2.ctor.IdDataDto.TId.TData.md "IdDataDto(TId id, TData data)")|实例化id data dto|id:id<br>data:data|

### 属性
|名称|类型|是否静态|注释|
|---|---|---|---|
|[Id](./SharpDevLib.IdDataDto.2.Id.md "Id")|TId|`否`|id|
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

