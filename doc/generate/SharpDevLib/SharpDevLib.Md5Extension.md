###### [主页](./Index.md "主页")
## Md5Extension 类
### 定义
**程序集** : [SharpDevLib.dll](./SharpDevLib.assembly.md "SharpDevLib.dll")
**命名空间** : [SharpDevLib](./SharpDevLib.namespace.md "SharpDevLib")
**继承** : [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object")
``` csharp
public static class Md5Extension : Object
```
**注释**
*MD5哈希扩展*

### 方法
|方法|返回类型|Accessor|是否静态|参数|
|---|---|---|---|---|
|[Md5(this Byte[] bytes, Md5OutputLength length)](./SharpDevLib.Md5Extension.Md5.thisByte.Md5OutputLength.md "Md5(this Byte[] bytes, Md5OutputLength length)")|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|`public`|`是`|bytes:字节数组<br>length:长度|
|[Md5(this Stream stream, Md5OutputLength length)](./SharpDevLib.Md5Extension.Md5.thisStream.Md5OutputLength.md "Md5(this Stream stream, Md5OutputLength length)")|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|`public`|`是`|stream:流<br>length:长度|
|[HmacMd5(this Byte[] bytes, Byte[] secret, Md5OutputLength length)](./SharpDevLib.Md5Extension.HmacMd5.thisByte.Byte.Md5OutputLength.md "HmacMd5(this Byte[] bytes, Byte[] secret, Md5OutputLength length)")|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|`public`|`是`|bytes:字节数组<br>secret:密钥<br>length:长度|
|[HmacMd5(this Stream stream, Byte[] secret, Md5OutputLength length)](./SharpDevLib.Md5Extension.HmacMd5.thisStream.Byte.Md5OutputLength.md "HmacMd5(this Stream stream, Byte[] secret, Md5OutputLength length)")|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|`public`|`是`|stream:流<br>secret:密钥<br>length:长度|
|GetType()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Type](https://learn.microsoft.com/en-us/dotnet/api/system.type "Type")|`public`|`否`|-|
|MemberwiseClone()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object")|`protected`|`否`|-|
|Finalize()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Void](https://learn.microsoft.com/en-us/dotnet/api/system.void "Void")|`protected`|`否`|-|
|ToString()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|`public`|`否`|-|
|Equals(Object obj)&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean "Boolean")|`public`|`否`|-|
|GetHashCode()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 "Int32")|`public`|`否`|-|

