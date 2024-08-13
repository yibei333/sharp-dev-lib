###### [主页](./Index.md "主页")
#### DeSerialize\<T\>(this String json, JsonOption option) 方法
**程序集** : [SharpDevLib.dll](./SharpDevLib.assembly.md "SharpDevLib.dll")
**命名空间** : [SharpDevLib](./SharpDevLib.namespace.md "SharpDevLib")
**所属类型** : [Json](./SharpDevLib.Json.md "Json")
``` csharp
public static T DeSerialize<T>(this String json, JsonOption option)
    where T : class
```
**注释**
*Json反序列化*

**泛型参数**
|名称|注释|约束|
|---|---|---|
|T|要反序列化的类型|`class`|


**返回类型** : T

**参数**
|名称|类型|注释|
|---|---|---|
|json|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|json|
|option|[JsonOption](./SharpDevLib.JsonOption.md "JsonOption")|选项|

