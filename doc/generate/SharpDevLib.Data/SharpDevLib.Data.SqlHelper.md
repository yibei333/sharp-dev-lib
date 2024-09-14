###### [主页](./Index.md "主页")

## SqlHelper 类

### 定义

**程序集** : [SharpDevLib.Data.dll](./SharpDevLib.Data.assembly.md "SharpDevLib.Data.dll")

**命名空间** : [SharpDevLib.Data](./SharpDevLib.Data.namespace.md "SharpDevLib.Data")

**继承** : [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object")

**实现** : [IDisposable](https://learn.microsoft.com/en-us/dotnet/api/system.idisposable "IDisposable")

``` csharp
public sealed class SqlHelper : Object, IDisposable
```

**注释**

*Sql帮助类*


### 构造函数

|方法|注释|参数|
|---|---|---|
|[SqlHelper(DbProviderFactory dbProviderFactory, String connectionString)](./SharpDevLib.Data.SqlHelper.ctor.DbProviderFactory.String.md "SqlHelper(DbProviderFactory dbProviderFactory, String connectionString)")|实例化Sql帮助类|dbProviderFactory:数据库提供商工厂,例如<br>1.引用Microsoft.Data.Sqlite,则用SqliteFactory.Instance<br>2.引用Microsoft.Data.SqlClient,则用SqlClientFactory.Instance<br>3.引用Pomelo.EntityFrameworkCore.MySql,则用MySqlConnectorFactory.Instance<br>connectionString:连接字符串<br>1.Sqlite,"data source=dbFilePath"<br>2.SqlServer,"Server=server;Database=database;User Id=user;Password=password;"<br>3.MySql,"server=server;user=user;password=password;database=database"|
|[SqlHelper()](./SharpDevLib.Data.SqlHelper.ctor.md "SqlHelper()")|实例化Sql帮助类|-|
|[SqlHelper(DbContext dbContext)](./SharpDevLib.Data.SqlHelper.ctor.DbContext.md "SqlHelper(DbContext dbContext)")|实例化Sql帮助类|dbContext:DbContext|


### 属性

|名称|类型|是否静态|注释|
|---|---|---|---|
|[Connection](./SharpDevLib.Data.SqlHelper.Connection.md "Connection")|[DbConnection](https://learn.microsoft.com/en-us/dotnet/api/system.data.common.dbconnection "DbConnection")|`否`|连接|
|[DbProviderFactory](./SharpDevLib.Data.SqlHelper.DbProviderFactory.md "DbProviderFactory")|[DbProviderFactory](https://learn.microsoft.com/en-us/dotnet/api/system.data.common.dbproviderfactory "DbProviderFactory")|`否`|数据库提供商工厂|


### 方法

|方法|返回类型|Accessor|是否静态|参数|
|---|---|---|---|---|
|[Config(DbProviderFactory dbProviderFactory, String connectionString)](./SharpDevLib.Data.SqlHelper.Config.DbProviderFactory.String.md "Config(DbProviderFactory dbProviderFactory, String connectionString)")|[Void](https://learn.microsoft.com/en-us/dotnet/api/system.void "Void")|`public`|`是`|dbProviderFactory:数据库提供商工厂<br>connectionString:连接字符串|
|[ExecuteScalar\<T\>(String sql, DbParameter[] parameters)](./SharpDevLib.Data.SqlHelper.ExecuteScalar.T.String.DbParameter.md "ExecuteScalar<T>(String sql, DbParameter[] parameters)")|T|`public`|`否`|sql:sql语句<br>parameters:sql参数|
|[ExecuteScalarAsync\<T\>(String sql, DbParameter[] parameters)](./SharpDevLib.Data.SqlHelper.ExecuteScalarAsync.T.String.DbParameter.md "ExecuteScalarAsync<T>(String sql, DbParameter[] parameters)")|[Task](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 "Task")\<T\>|`public`|`否`|sql:sql语句<br>parameters:sql参数|
|[ExecuteScalarAsync\<T\>(String sql, DbParameter[] parameters, CancellationToken cancellationToken)](./SharpDevLib.Data.SqlHelper.ExecuteScalarAsync.T.String.DbParameter.CancellationToken.md "ExecuteScalarAsync<T>(String sql, DbParameter[] parameters, CancellationToken cancellationToken)")|[Task](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 "Task")\<T\>|`public`|`否`|sql:sql语句<br>parameters:sql参数<br>cancellationToken:CancellationToken|
|[ExecuteDataSet(String sql, DbParameter[] parameters)](./SharpDevLib.Data.SqlHelper.ExecuteDataSet.String.DbParameter.md "ExecuteDataSet(String sql, DbParameter[] parameters)")|[DataSet](https://learn.microsoft.com/en-us/dotnet/api/system.data.dataset "DataSet")|`public`|`否`|sql:sql语句<br>parameters:sql参数|
|[ExecuteDataSetAsync(String sql, DbParameter[] parameters)](./SharpDevLib.Data.SqlHelper.ExecuteDataSetAsync.String.DbParameter.md "ExecuteDataSetAsync(String sql, DbParameter[] parameters)")|[Task](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 "Task")\<[DataSet](https://learn.microsoft.com/en-us/dotnet/api/system.data.dataset "DataSet")\>|`public`|`否`|sql:sql语句<br>parameters:sql参数|
|[ExecuteDataSetAsync(String sql, DbParameter[] parameters, CancellationToken cancellationToken)](./SharpDevLib.Data.SqlHelper.ExecuteDataSetAsync.String.DbParameter.CancellationToken.md "ExecuteDataSetAsync(String sql, DbParameter[] parameters, CancellationToken cancellationToken)")|[Task](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 "Task")\<[DataSet](https://learn.microsoft.com/en-us/dotnet/api/system.data.dataset "DataSet")\>|`public`|`否`|sql:sql语句<br>parameters:sql参数<br>cancellationToken:CancellationToken|
|[ExecuteDataTable(String sql, DbParameter[] parameters)](./SharpDevLib.Data.SqlHelper.ExecuteDataTable.String.DbParameter.md "ExecuteDataTable(String sql, DbParameter[] parameters)")|[DataTable](https://learn.microsoft.com/en-us/dotnet/api/system.data.datatable "DataTable")|`public`|`否`|sql:sql语句<br>parameters:sql参数|
|[ExecuteDataTableAsync(String sql, DbParameter[] parameters)](./SharpDevLib.Data.SqlHelper.ExecuteDataTableAsync.String.DbParameter.md "ExecuteDataTableAsync(String sql, DbParameter[] parameters)")|[Task](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 "Task")\<[DataTable](https://learn.microsoft.com/en-us/dotnet/api/system.data.datatable "DataTable")\>|`public`|`否`|sql:sql语句<br>parameters:sql参数|
|[ExecuteDataTableAsync(String sql, DbParameter[] parameters, CancellationToken cancellationToken)](./SharpDevLib.Data.SqlHelper.ExecuteDataTableAsync.String.DbParameter.CancellationToken.md "ExecuteDataTableAsync(String sql, DbParameter[] parameters, CancellationToken cancellationToken)")|[Task](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 "Task")\<[DataTable](https://learn.microsoft.com/en-us/dotnet/api/system.data.datatable "DataTable")\>|`public`|`否`|sql:sql语句<br>parameters:sql参数<br>cancellationToken:CancellationToken|
|[ExecuteNonQuery(String sql, DbParameter[] parameters)](./SharpDevLib.Data.SqlHelper.ExecuteNonQuery.String.DbParameter.md "ExecuteNonQuery(String sql, DbParameter[] parameters)")|[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 "Int32")|`public`|`否`|sql:sql语句<br>parameters:sql参数|
|[ExecuteNonQueryAsync(String sql, DbParameter[] parameters)](./SharpDevLib.Data.SqlHelper.ExecuteNonQueryAsync.String.DbParameter.md "ExecuteNonQueryAsync(String sql, DbParameter[] parameters)")|[Task](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 "Task")\<[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 "Int32")\>|`public`|`否`|sql:sql语句<br>parameters:sql参数|
|[ExecuteNonQueryAsync(String sql, DbParameter[] parameters, CancellationToken cancellationToken)](./SharpDevLib.Data.SqlHelper.ExecuteNonQueryAsync.String.DbParameter.CancellationToken.md "ExecuteNonQueryAsync(String sql, DbParameter[] parameters, CancellationToken cancellationToken)")|[Task](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 "Task")\<[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 "Int32")\>|`public`|`否`|sql:sql语句<br>parameters:sql参数<br>cancellationToken:CancellationToken|
|[Dispose()](./SharpDevLib.Data.SqlHelper.Dispose.md "Dispose()")|[Void](https://learn.microsoft.com/en-us/dotnet/api/system.void "Void")|`public`|`否`|-|
|GetType()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Type](https://learn.microsoft.com/en-us/dotnet/api/system.type "Type")|`public`|`否`|-|
|MemberwiseClone()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object")|`protected`|`否`|-|
|Finalize()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Void](https://learn.microsoft.com/en-us/dotnet/api/system.void "Void")|`protected`|`否`|-|
|ToString()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|`public`|`否`|-|
|Equals(Object obj)&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean "Boolean")|`public`|`否`|-|
|GetHashCode()&nbsp;&nbsp;&nbsp;&nbsp;*(继承自[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object "Object"))*|[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 "Int32")|`public`|`否`|-|


