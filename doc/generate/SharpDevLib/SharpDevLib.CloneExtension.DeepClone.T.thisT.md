###### [主页](./Index.md "主页")

#### DeepClone\<T\>(this T source) 方法

**程序集** : [SharpDevLib.dll](./SharpDevLib.assembly.md "SharpDevLib.dll")

**命名空间** : [SharpDevLib](./SharpDevLib.namespace.md "SharpDevLib")

**所属类型** : [CloneExtension](./SharpDevLib.CloneExtension.md "CloneExtension")

``` csharp
public static T DeepClone<T>(this T source)
    where T : class
```

**注释**

*通过序列化的方式深拷贝对象*



**泛型参数**

|名称|注释|约束|
|---|---|---|
|T|拷贝对象泛型类型|`class`|




**返回类型** : T


**参数**

|名称|类型|注释|
|---|---|---|
|source|T|需要拷贝的对象|


