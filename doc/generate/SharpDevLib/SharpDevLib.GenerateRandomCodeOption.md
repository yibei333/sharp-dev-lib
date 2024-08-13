###### [主页](./Index.md "主页")

## GenerateRandomCodeOption 类

### 定义

**程序集** : [SharpDevLib.dll](./SharpDevLib.assembly.md "SharpDevLib.dll")

**命名空间** : [SharpDevLib](./SharpDevLib.namespace.md "SharpDevLib")

**继承** : [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object")

``` csharp
public class GenerateRandomCodeOption : Object
```

**注释**

*生成随机码选项*


### 构造函数

|方法|注释|参数|
|---|---|---|
|[GenerateRandomCodeOption()](./SharpDevLib.GenerateRandomCodeOption.ctor.md "GenerateRandomCodeOption()")|默认构造函数|-|


### 字段

|名称|类型|是否静态|注释|
|---|---|---|---|
|[NumberSeed](./SharpDevLib.GenerateRandomCodeOption.NumberSeed.md "NumberSeed")|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|`是`|数字种子数据|
|[LowerLetterSeed](./SharpDevLib.GenerateRandomCodeOption.LowerLetterSeed.md "LowerLetterSeed")|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|`是`|小写字母种子数据|
|[UpperLetterSeed](./SharpDevLib.GenerateRandomCodeOption.UpperLetterSeed.md "UpperLetterSeed")|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|`是`|大写字母种子数据|
|[SpecialSymbolSeed](./SharpDevLib.GenerateRandomCodeOption.SpecialSymbolSeed.md "SpecialSymbolSeed")|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|`是`|特殊字符种子数据|


### 属性

|名称|类型|是否静态|注释|
|---|---|---|---|
|[Length](./SharpDevLib.GenerateRandomCodeOption.Length.md "Length")|[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 "Int32")|`否`|长度，默认为6|
|[UseNumber](./SharpDevLib.GenerateRandomCodeOption.UseNumber.md "UseNumber")|[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean "Boolean")|`否`|是否生成带数字的随机码,默认为true|
|[UseLowerLetter](./SharpDevLib.GenerateRandomCodeOption.UseLowerLetter.md "UseLowerLetter")|[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean "Boolean")|`否`|是否生成带小写字母的随机码,默认为true|
|[UseUpperLetter](./SharpDevLib.GenerateRandomCodeOption.UseUpperLetter.md "UseUpperLetter")|[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean "Boolean")|`否`|是否生成带大写字母的随机码,默认为true|
|[UseSpecialSymbol](./SharpDevLib.GenerateRandomCodeOption.UseSpecialSymbol.md "UseSpecialSymbol")|[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean "Boolean")|`否`|是否生成带特殊字符的随机码,默认为false|
|[CustomSeed](./SharpDevLib.GenerateRandomCodeOption.CustomSeed.md "CustomSeed")|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|`否`|自定义种子数据|
|[UseCustomSeed](./SharpDevLib.GenerateRandomCodeOption.UseCustomSeed.md "UseCustomSeed")|[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean "Boolean")|`否`|是否生成自定义字符的随机码,默认为false,如果为true,UseNumber、UseLowerLetter、UseUpperLetter、UseSpecialSymbol将忽略完全按照自定义字符生成|
|[Seed](./SharpDevLib.GenerateRandomCodeOption.Seed.md "Seed")|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|`否`|种子数据|


### 方法

|方法|返回类型|Accessor|是否静态|参数|
|---|---|---|---|---|
|GetType()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Type](https://learn.microsoft.com/en-us/dotnet/api/system.type "Type")|`public`|`否`|-|
|MemberwiseClone()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object")|`protected`|`否`|-|
|Finalize()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Void](https://learn.microsoft.com/en-us/dotnet/api/system.void "Void")|`protected`|`否`|-|
|ToString()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|`public`|`否`|-|
|Equals(Object obj)&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean "Boolean")|`public`|`否`|-|
|GetHashCode()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 "Int32")|`public`|`否`|-|


