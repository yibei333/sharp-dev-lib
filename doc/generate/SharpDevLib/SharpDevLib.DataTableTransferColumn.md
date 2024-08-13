###### [主页](./Index.md "主页")
## DataTableTransferColumn 类
### 定义
**程序集** : [SharpDevLib.dll](./SharpDevLib.assembly.md "SharpDevLib.dll")
**命名空间** : [SharpDevLib](./SharpDevLib.namespace.md "SharpDevLib")
**继承** : [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object")
``` csharp
public class DataTableTransferColumn : Object
```
**注释**
*实例化DataTable转换列*

### 构造函数
|方法|注释|参数|
|---|---|---|
|[DataTableTransferColumn(String name)](./SharpDevLib.DataTableTransferColumn.ctor.String.md "DataTableTransferColumn(String name)")|实例化DataTable转换列|name:源DataTable中的列名,完全匹配,注意空格和*号|

### 属性
|名称|类型|是否静态|注释|
|---|---|---|---|
|[Name](./SharpDevLib.DataTableTransferColumn.Name.md "Name")|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|`否`|源DataTable中的列名,完全匹配,注意空格和*号|
|[IsRequired](./SharpDevLib.DataTableTransferColumn.IsRequired.md "IsRequired")|[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean "Boolean")|`否`|是否必需,如果是则在列名前面加*号|
|[TargetType](./SharpDevLib.DataTableTransferColumn.TargetType.md "TargetType")|[Type](https://learn.microsoft.com/en-us/dotnet/api/system.type "Type")|`否`|目标列的类型,如果不设置则和源列的类型保持一致,注意和ValueConverter返回的数据类型一致|
|[ValueConverter](./SharpDevLib.DataTableTransferColumn.ValueConverter.md "ValueConverter")|[Func](https://learn.microsoft.com/en-us/dotnet/api/system.func-3 "Func")\<[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"), [DataRow](https://learn.microsoft.com/en-us/dotnet/api/system.data.datarow "DataRow"), [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object")\>|`否`|值转换器,参数说明如下|
|[NameConverter](./SharpDevLib.DataTableTransferColumn.NameConverter.md "NameConverter")|[Func](https://learn.microsoft.com/en-us/dotnet/api/system.func-2 "Func")\<[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String"), [String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")\>|`否`|列明转换器,参数说明如下|

### 方法
|方法|返回类型|Accessor|是否静态|参数|
|---|---|---|---|---|
|GetType()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Type](https://learn.microsoft.com/en-us/dotnet/api/system.type "Type")|`public`|`否`|-|
|MemberwiseClone()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object")|`protected`|`否`|-|
|Finalize()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Void](https://learn.microsoft.com/en-us/dotnet/api/system.void "Void")|`protected`|`否`|-|
|ToString()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|`public`|`否`|-|
|Equals(Object obj)&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean "Boolean")|`public`|`否`|-|
|GetHashCode()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 "Int32")|`public`|`否`|-|

