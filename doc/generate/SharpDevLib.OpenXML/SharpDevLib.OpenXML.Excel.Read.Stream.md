###### [主页](./Index.md "主页")

#### Read(Stream stream) 方法

**程序集** : [SharpDevLib.OpenXML.dll](./SharpDevLib.OpenXML.assembly.md "SharpDevLib.OpenXML.dll")

**命名空间** : [SharpDevLib.OpenXML](./SharpDevLib.OpenXML.namespace.md "SharpDevLib.OpenXML")

**所属类型** : [Excel](./SharpDevLib.OpenXML.Excel.md "Excel")

``` csharp
public static DataSet Read(Stream stream)
```

**注释**

*读取标准的Excel流,标准的定义为*

* 1.第一行为表头

* 2.每行的列不能超出表头的长度范围

* 3.读取的结果中所有列的类型都为string类型



**返回类型** : [DataSet](https://learn.microsoft.com/en-us/dotnet/api/system.data.dataset "DataSet")


**参数**

|名称|类型|注释|
|---|---|---|
|stream|[Stream](https://learn.microsoft.com/en-us/dotnet/api/system.io.stream "Stream")|标准的Excel流|


