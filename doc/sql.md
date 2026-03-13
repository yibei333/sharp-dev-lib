# SqlHelper

提供数据库连接、查询和执行 SQL 语句的功能。

### 常用数据库配置

```csharp
// SQLite
//依赖包Microsoft.Data.Sqlite
SqlHelper.Config(SqliteFactory.Instance, "data source=mydb.db");

// SQL Server
//依赖包Microsoft.Data.SqlClient
SqlHelper.Config(SqlClientFactory.Instance, "Server=localhost;Database=mydb;User Id=sa;Password=yourPassword;");

// MySQL
//依赖包Pomelo.EntityFrameworkCore.MySql
SqlHelper.Config(MySqlConnectorFactory.Instance, "server=localhost;user=root;password=password;database=mydb");
```


##### 示例

```csharp
using Microsoft.Data.Sqlite;
using SharpDevLib;

//初始化db
"test.db".RemoveFileIfExist();
//仅需设置一次
SqlHelper.Config(SqliteFactory.Instance, "data source=test.db");

//1.如果没有调用SqlHelper.Config配置，则需每次传入，否则引发异常
//2.如果使用DbContext，可以传入DbContext示例化
var sqlHelper=new SqlHelper();
var initSql=$@"
    CREATE TABLE [Users](Id INTEGER,[Name] TEXT,Age INTEGER);
    
    INSERT INTO [Users] VALUES(1,'张三',10);
    INSERT INTO [Users] VALUES(2,'李四',20);
    INSERT INTO [Users] VALUES(3,'王五',30);
";
sqlHelper.ExecuteNonQuery(initSql);

//添加
sqlHelper.ExecuteNonQuery("INSERT INTO [Users] VALUES(4,'foo',40)");

//修改
sqlHelper.ExecuteNonQuery("UPDATE [Users] SET Age=Age+10 WHERE Id=4");

//删除
sqlHelper.ExecuteNonQuery("DELETE FROM [Users] WHERE Id=4");

//scalar单个查询
var count=sqlHelper.ExecuteScalar<int>("SELECT COUNT(1) FROM [Users]");
Console.WriteLine(sqlHelper.ExecuteScalar<int>("SELECT COUNT(1) FROM [Users]"));//3

//查询列表
var list=sqlHelper.Query<IdNameDataDto<int,int>>("SELECT Id,Name,Age [Data] FROM [Users]");
Console.WriteLine(list.Serialize());
//[{"Id":1,"Name":"张三","Data":10},{"Id":2,"Name":"李四","Data":20},{"Id":3,"Name":"王五","Data":30}]

//查询DataSet
var dataSet=sqlHelper.ExecuteDataSet("SELECT Id,Name,Age [Data] FROM [Users] WHERE Id<=2;SELECT Id,Name,Age [Data] FROM [Users] WHERE Id>2;");
Console.WriteLine(dataSet.Tables[0].Serialize());
//Id|Name|Data
//1|张三|10
//2|李四|20
Console.WriteLine(dataSet.Tables[1].Serialize());
//Id|Name|Data
//3|王五|30

//查询DataTable
var table=sqlHelper.ExecuteDataTable("SELECT Id,Name,Age [Data] FROM [Users]");
Console.WriteLine(table.Serialize());
//Id|Name|Data
//1|张三|10
//2|李四|20
//3|王五|30

//使用事务
var transaction=sqlHelper.Connection.BeginTransaction();
try
{
    sqlHelper.ExecuteNonQuery("INSERT INTO [Users] VALUES(4,'foo',40)");
    sqlHelper.ExecuteNonQuery("INSERT INTO [Users] VALUES(5,'bar')");
    transaction.Commit();
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
    //SQLite Error 1: 'table Users has 3 columns but 2 values were supplied'.
    transaction.Rollback();
}
```

## 相关文档

- [基础](../README.md#基础)
