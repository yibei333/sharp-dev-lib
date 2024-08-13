###### [主页](./Index.md "主页")

#### ExecuteNonQueryAsync(String sql, DbParameter[] parameters) 方法

**程序集** : [SharpDevLib.Data.dll](./SharpDevLib.Data.assembly.md "SharpDevLib.Data.dll")

**命名空间** : [SharpDevLib.Data](./SharpDevLib.Data.namespace.md "SharpDevLib.Data")

**所属类型** : [SqlHelper](./SharpDevLib.Data.SqlHelper.md "SqlHelper")

``` csharp
[System.Runtime.CompilerServices.AsyncStateMachineAttribute(typeof(SharpDevLib.Data.SqlHelper+<ExecuteNonQueryAsync>d__28))]
[System.Diagnostics.DebuggerStepThroughAttribute()]
public Task<Int32> ExecuteNonQueryAsync(String sql, DbParameter[] parameters)
```

**注释**

*执行非查询sql语句*



**返回类型** : [Task](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 "Task")\<[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 "Int32")\>


**参数**

|名称|类型|注释|
|---|---|---|
|sql|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|sql语句|
|parameters|[DbParameter\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.data.common.dbparameter[] "DbParameter\[\]")|sql参数|


