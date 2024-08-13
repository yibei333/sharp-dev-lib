###### [主页](./Index.md "主页")
## PageRequest 类
### 定义
**程序集** : [SharpDevLib.dll](./SharpDevLib.assembly.md "SharpDevLib.dll")
**命名空间** : [SharpDevLib](./SharpDevLib.namespace.md "SharpDevLib")
**继承** : [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object") ↣ [BaseRequest](./SharpDevLib.BaseRequest.md "BaseRequest")
``` csharp
public class PageRequest : BaseRequest
```
**注释**
*分页request*

### 构造函数
|方法|注释|参数|
|---|---|---|
|[PageRequest()](./SharpDevLib.PageRequest.ctor.md "PageRequest()")|实例化分页request|-|
|[PageRequest(Int32 index, Int32 size)](./SharpDevLib.PageRequest.ctor.Int32.Int32.md "PageRequest(Int32 index, Int32 size)")|实例化分页request|index:索引(当前位置),默认为0<br>size:每页数据条数|

### 属性
|名称|类型|是否静态|注释|
|---|---|---|---|
|[Index](./SharpDevLib.PageRequest.Index.md "Index")|[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 "Int32")|`否`|索引(当前位置),默认为0|
|[Size](./SharpDevLib.PageRequest.Size.md "Size")|[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 "Int32")|`否`|每页数据条数|

### 方法
|方法|返回类型|Accessor|是否静态|参数|
|---|---|---|---|---|
|GetType()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Type](https://learn.microsoft.com/en-us/dotnet/api/system.type "Type")|`public`|`否`|-|
|MemberwiseClone()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object")|`protected`|`否`|-|
|Finalize()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Void](https://learn.microsoft.com/en-us/dotnet/api/system.void "Void")|`protected`|`否`|-|
|ToString()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|`public`|`否`|-|
|Equals(Object obj)&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean "Boolean")|`public`|`否`|-|
|GetHashCode()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 "Int32")|`public`|`否`|-|
