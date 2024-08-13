###### [主页](./Index.md "主页")
#### SqlHelper(DbProviderFactory dbProviderFactory, String connectionString) 构造函数
**程序集** : [SharpDevLib.Data.dll](./SharpDevLib.Data.assembly.md "SharpDevLib.Data.dll")
**命名空间** : [SharpDevLib.Data](./SharpDevLib.Data.namespace.md "SharpDevLib.Data")
**所属类型** : [SqlHelper](./SharpDevLib.Data.SqlHelper.md "SqlHelper")
``` csharp
public SqlHelper(DbProviderFactory dbProviderFactory, String connectionString)
```
**注释**
*实例化Sql帮助类*

**参数**
|名称|类型|注释|
|---|---|---|
|dbProviderFactory|[DbProviderFactory](https://learn.microsoft.com/en-us/dotnet/api/system.data.common.dbproviderfactory "DbProviderFactory")|数据库提供商工厂,例如
            1.引用Microsoft.Data.Sqlite,则用SqliteFactory.Instance2.引用Microsoft.Data.SqlClient,则用SqlClientFactory.Instance3.引用Pomelo.EntityFrameworkCore.MySql,则用MySqlConnectorFactory.Instance|
|connectionString|[String](https://learn.microsoft.com/en-us/dotnet/api/system.string "String")|连接字符串
            1.Sqlite,"data source=dbFilePath"2.SqlServer,"Server=server;Database=database;User Id=user;Password=password;"3.MySql,"server=server;user=user;password=password;database=database"|

