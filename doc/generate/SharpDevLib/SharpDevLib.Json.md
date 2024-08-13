###### [主页](./Index.md "主页")
## Json 类
### 定义
**程序集** : [SharpDevLib.dll](./SharpDevLib.assembly.md "SharpDevLib.dll")
**命名空间** : [SharpDevLib](./SharpDevLib.namespace.md "SharpDevLib")
**继承** : [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object")
``` csharp
public static class Json : Object
```
**注释**
*json扩展*

### 方法
|方法|返回类型|Accessor|是否静态|参数|
|---|---|---|---|---|
|[Serialize(this Object obj)](./SharpDevLib.Json.Serialize.thisObject.md "Serialize(this Object obj)")|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|`public`|`是`|obj:需要序列化的对象|
|[Serialize(this Object obj, JsonOption option)](./SharpDevLib.Json.Serialize.thisObject.JsonOption.md "Serialize(this Object obj, JsonOption option)")|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|`public`|`是`|obj:需要序列化的对象<br>option:选项|
|[TrySerialize(this Object obj, String& jsonResult)](./SharpDevLib.Json.TrySerialize.thisObject.String&.md "TrySerialize(this Object obj, String& jsonResult)")|[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean "Boolean")|`public`|`是`|obj:-<br>jsonResult:-|
|[TrySerialize(this Object obj, JsonOption option, String& jsonResult)](./SharpDevLib.Json.TrySerialize.thisObject.JsonOption.String&.md "TrySerialize(this Object obj, JsonOption option, String& jsonResult)")|[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean "Boolean")|`public`|`是`|obj:-<br>option:-<br>jsonResult:-|
|[DeSerialize\<T\>(this String json)](./SharpDevLib.Json.DeSerialize.T.thisString.md "DeSerialize<T>(this String json)")|T|`public`|`是`|json:json|
|[DeSerialize(this String json, Type type)](./SharpDevLib.Json.DeSerialize.thisString.Type.md "DeSerialize(this String json, Type type)")|[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object")|`public`|`是`|json:json<br>type:type|
|[DeSerialize\<T\>(this String json, JsonOption option)](./SharpDevLib.Json.DeSerialize.T.thisString.JsonOption.md "DeSerialize<T>(this String json, JsonOption option)")|T|`public`|`是`|json:json<br>option:选项|
|[TryDeSerialize\<T\>(this String json, T& result)](./SharpDevLib.Json.TryDeSerialize.T.thisString.T&.md "TryDeSerialize<T>(this String json, T& result)")|[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean "Boolean")|`public`|`是`|json:-<br>result:-|
|[TryDeSerialize\<T\>(this String json, JsonOption option, T& result)](./SharpDevLib.Json.TryDeSerialize.T.thisString.JsonOption.T&.md "TryDeSerialize<T>(this String json, JsonOption option, T& result)")|[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean "Boolean")|`public`|`是`|json:-<br>option:-<br>result:-|
|[FormatJson(this String json, Boolean orderByNameProperty)](./SharpDevLib.Json.FormatJson.thisString.Boolean.md "FormatJson(this String json, Boolean orderByNameProperty)")|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|`public`|`是`|json:需要格式化的json<br>orderByNameProperty:是否根据属性名称排序,默认为true|
|[CompressJson(this String json, Boolean orderByNameProperty)](./SharpDevLib.Json.CompressJson.thisString.Boolean.md "CompressJson(this String json, Boolean orderByNameProperty)")|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|`public`|`是`|json:需要压缩的json<br>orderByNameProperty:是否根据属性名称排序,默认为true|
|GetType()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Type](https://learn.microsoft.com/en-us/dotnet/api/system.type "Type")|`public`|`否`|-|
|MemberwiseClone()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object")|`protected`|`否`|-|
|Finalize()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Void](https://learn.microsoft.com/en-us/dotnet/api/system.void "Void")|`protected`|`否`|-|
|ToString()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|`public`|`否`|-|
|Equals(Object obj)&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean "Boolean")|`public`|`否`|-|
|GetHashCode()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 "Int32")|`public`|`否`|-|

