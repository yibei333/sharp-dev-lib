###### [主页](./Index.md "主页")

## Excel 类

### 定义

**程序集** : [SharpDevLib.OpenXML.dll](./SharpDevLib.OpenXML.assembly.md "SharpDevLib.OpenXML.dll")

**命名空间** : [SharpDevLib.OpenXML](./SharpDevLib.OpenXML.namespace.md "SharpDevLib.OpenXML")

**继承** : [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object")

``` csharp
public static class Excel : Object
```

**注释**

*excel扩展*


### 方法

|方法|返回类型|Accessor|是否静态|参数|
|---|---|---|---|---|
|[Encrypt(Stream inputStream, Stream outputStream, String password)](./SharpDevLib.OpenXML.Excel.Encrypt.Stream.Stream.String.md "Encrypt(Stream inputStream, Stream outputStream, String password)")|[Void](https://learn.microsoft.com/en-us/dotnet/api/system.void "Void")|`public`|`是`|inputStream:excel文件流<br>outputStream:密码保护的excel文件流<br>password:密码|
|[Decrypt(Stream inputStream, Stream outputStream, String password)](./SharpDevLib.OpenXML.Excel.Decrypt.Stream.Stream.String.md "Decrypt(Stream inputStream, Stream outputStream, String password)")|[Void](https://learn.microsoft.com/en-us/dotnet/api/system.void "Void")|`public`|`是`|inputStream:密码保护的excel文件流<br>outputStream:去除密码的excel文件流<br>password:密码|
|[Read(Stream stream)](./SharpDevLib.OpenXML.Excel.Read.Stream.md "Read(Stream stream)")|[DataSet](https://learn.microsoft.com/en-us/dotnet/api/system.data.dataset "DataSet")|`public`|`是`|stream:标准的Excel流|
|[Read(Stream stream, String[][] columnNames)](./SharpDevLib.OpenXML.Excel.Read.Stream.String.md "Read(Stream stream, String[][] columnNames)")|[DataSet](https://learn.microsoft.com/en-us/dotnet/api/system.data.dataset "DataSet")|`public`|`是`|stream:标准的Excel流<br>columnNames:自定义列名|
|[Write(DataTable dataTable, Stream stream)](./SharpDevLib.OpenXML.Excel.Write.DataTable.Stream.md "Write(DataTable dataTable, Stream stream)")|[Void](https://learn.microsoft.com/en-us/dotnet/api/system.void "Void")|`public`|`是`|dataTable:DataTable<br>stream:一般为Excel的文件流|
|[Write(DataTable dataTable, Stream stream, String[] columnNames)](./SharpDevLib.OpenXML.Excel.Write.DataTable.Stream.String.md "Write(DataTable dataTable, Stream stream, String[] columnNames)")|[Void](https://learn.microsoft.com/en-us/dotnet/api/system.void "Void")|`public`|`是`|dataTable:DataTable<br>stream:一般为Excel的文件流<br>columnNames:自定义列名|
|[Write(DataSet dataSet, Stream stream)](./SharpDevLib.OpenXML.Excel.Write.DataSet.Stream.md "Write(DataSet dataSet, Stream stream)")|[Void](https://learn.microsoft.com/en-us/dotnet/api/system.void "Void")|`public`|`是`|dataSet:DataSet<br>stream:一般为Excel的文件流|
|[Write(DataSet dataSet, Stream stream, String[][] columnNames)](./SharpDevLib.OpenXML.Excel.Write.DataSet.Stream.String.md "Write(DataSet dataSet, Stream stream, String[][] columnNames)")|[Void](https://learn.microsoft.com/en-us/dotnet/api/system.void "Void")|`public`|`是`|dataSet:DataSet<br>stream:一般为Excel的文件流<br>columnNames:自定义列名|
|GetType()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Type](https://learn.microsoft.com/en-us/dotnet/api/system.type "Type")|`public`|`否`|-|
|MemberwiseClone()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object")|`protected`|`否`|-|
|Finalize()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Void](https://learn.microsoft.com/en-us/dotnet/api/system.void "Void")|`protected`|`否`|-|
|ToString()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|`public`|`否`|-|
|Equals(Object obj)&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean "Boolean")|`public`|`否`|-|
|GetHashCode()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 "Int32")|`public`|`否`|-|


