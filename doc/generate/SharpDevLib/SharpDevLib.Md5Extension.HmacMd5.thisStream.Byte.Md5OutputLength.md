###### [主页](./Index.md "主页")

#### HmacMd5(this Stream stream, Byte[] secret, Md5OutputLength length) 方法

**程序集** : [SharpDevLib.dll](./SharpDevLib.assembly.md "SharpDevLib.dll")

**命名空间** : [SharpDevLib](./SharpDevLib.namespace.md "SharpDevLib")

**所属类型** : [Md5Extension](./SharpDevLib.Md5Extension.md "Md5Extension")

``` csharp
public static String HmacMd5(this Stream stream, Byte[] secret, Md5OutputLength length)
```

**注释**

*流HmacMd5哈希*



**返回类型** : [String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")


**参数**

|名称|类型|注释|
|---|---|---|
|stream|[Stream](https://learn.microsoft.com/en-us/dotnet/api/system.io.stream "Stream")|流|
|secret|[Byte\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.byte[] "Byte\[\]")|密钥|
|length|[Md5OutputLength](./SharpDevLib.Md5OutputLength.md "Md5OutputLength")|长度|


