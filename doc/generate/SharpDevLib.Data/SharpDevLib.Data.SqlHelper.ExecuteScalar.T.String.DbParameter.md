###### [主页](./Index.md "主页")

#### ExecuteScalar\<T\>(String sql, DbParameter[] parameters) 方法

**程序集** : [SharpDevLib.Data.dll](./SharpDevLib.Data.assembly.md "SharpDevLib.Data.dll")

**命名空间** : [SharpDevLib.Data](./SharpDevLib.Data.namespace.md "SharpDevLib.Data")

**所属类型** : [SqlHelper](./SharpDevLib.Data.SqlHelper.md "SqlHelper")

``` csharp
public T ExecuteScalar<T>(String sql, DbParameter[] parameters)
```

**注释**

*检索单个值*



**泛型参数**

|名称|注释|约束|
|---|---|---|
|T|返回值的类型|-|




**返回类型** : T


**参数**

|名称|类型|注释|
|---|---|---|
|sql|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|sql语句|
|parameters|[DbParameter\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.data.common.dbparameter[] "DbParameter\[\]")|sql参数|


