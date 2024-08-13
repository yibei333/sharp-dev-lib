###### [主页](./Index.md "主页")
## DataDto\<TData\> 类
### 定义
**程序集** : [SharpDevLib.dll](./SharpDevLib.assembly.md "SharpDevLib.dll")
**命名空间** : [SharpDevLib](./SharpDevLib.namespace.md "SharpDevLib")
**继承** : [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object") ↣ [BaseDto](./SharpDevLib.BaseDto.md "BaseDto")
**派生** : [IdDataDto](./SharpDevLib.IdDataDto.2.md "IdDataDto")\<TId, TData\>, [IdNameDataDto](./SharpDevLib.IdNameDataDto.2.md "IdNameDataDto")\<TId, TData\>
``` csharp
public class DataDto<TData> : BaseDto
```
**注释**
*data dto*

**泛型参数**
|名称|注释|约束|
|---|---|---|
|TData|data type|-|


### 构造函数
|方法|注释|参数|
|---|---|---|
|[DataDto()](./SharpDevLib.DataDto.1.ctor.DataDto.md "DataDto()")|实例化data dto|-|
|[DataDto(TData data)](./SharpDevLib.DataDto.1.ctor.DataDto.TData.md "DataDto(TData data)")|实例化data dto|data:data|

### 属性
|名称|类型|是否静态|注释|
|---|---|---|---|
|[Data](./SharpDevLib.DataDto.1.Data.md "Data")|TData|`否`|data|

### 方法
|方法|返回类型|Accessor|是否静态|参数|
|---|---|---|---|---|
|GetType()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Type](https://learn.microsoft.com/en-us/dotnet/api/system.type "Type")|`public`|`否`|-|
|MemberwiseClone()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object")|`protected`|`否`|-|
|Finalize()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Void](https://learn.microsoft.com/en-us/dotnet/api/system.void "Void")|`protected`|`否`|-|
|ToString()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|`public`|`否`|-|
|Equals(Object obj)&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean "Boolean")|`public`|`否`|-|
|GetHashCode()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 "Int32")|`public`|`否`|-|

