# SqlHelper - SQL 数据库操作

提供数据库连接、查询和执行 SQL 语句的功能。

## 支持的数据库

| 数据库 | 依赖包 | 工厂类 |
| --- | --- | --- |
| SQLite | Microsoft.Data.Sqlite | SqliteFactory.Instance |
| SQL Server | Microsoft.Data.SqlClient | SqlClientFactory.Instance |
| MySQL | Pomelo.EntityFrameworkCore.MySql | MySqlConnectorFactory.Instance |

## 类

### SqlHelper

SQL 帮助类，提供数据库连接、查询和执行 SQL 语句的功能。

#### 构造函数

| 构造函数 | 说明 |
| --- | --- |
| SqlHelper(DbProviderFactory dbProviderFactory, string connectionString) | 初始化 SQL 帮助类实例，使用指定的数据库提供商工厂和连接字符串 |
| SqlHelper() | 初始化 SQL 帮助类实例，使用全局配置的数据库提供商工厂和连接字符串 |
| SqlHelper(DbContext dbContext) | 初始化 SQL 帮助类实例，使用现有的 DbContext |

#### 属性

| 属性 | 类型 | 说明 |
| --- | --- | --- |
| Connection | DbConnection | 数据库连接对象 |
| DbProviderFactory | DbProviderFactory | 数据库提供商工厂 |

## 静态方法

### Config

设置全局数据库配置。

#### 方法签名

```csharp
public static void Config(DbProviderFactory dbProviderFactory, string connectionString)
```

#### 参数

| 参数 | 类型 | 说明 |
| --- | --- | --- |
| dbProviderFactory | DbProviderFactory | 数据库提供商工厂 |
| connectionString | string | 数据库连接字符串 |

## 实例方法

### ExecuteScalar\<T\>

执行 SQL 查询并返回单个值。

#### 方法签名

```csharp
public T ExecuteScalar<T>(string sql, params DbParameter[] parameters)
```

#### 参数

| 参数 | 类型 | 说明 |
| --- | --- | --- |
| sql | string | SQL 查询语句 |
| parameters | params DbParameter[] | SQL 参数数组 |

#### 返回值

查询结果的单个值，类型 T 必须是值类型或 String。

#### 异常

| 异常 | 说明 |
| --- | --- |
| Exception | 当 T 的类型为引用类型（排除 string 类型）时抛出异常 |

### ExecuteScalarAsync\<T\>

异步执行 SQL 查询并返回单个值。

#### 方法签名

```csharp
public Task<T> ExecuteScalarAsync<T>(string sql, params DbParameter[] parameters)
public Task<T?> ExecuteScalarAsync<T>(CancellationToken cancellationToken, string sql, params DbParameter[] parameters)
```

#### 参数

| 参数 | 类型 | 说明 |
| --- | --- | --- |
| cancellationToken | CancellationToken | 取消令牌 |
| sql | string | SQL 查询语句 |
| parameters | params DbParameter[] | SQL 参数数组 |

#### 返回值

查询结果的单个值。

### Query\<T\>

执行 SQL 查询并返回对象列表。

#### 方法签名

```csharp
public IEnumerable<T> Query<T>(string sql, params DbParameter[] parameters)
public async Task<IEnumerable<T>> QueryAsync<T>(string sql, params DbParameter[] parameters)
public async Task<IEnumerable<T>> QueryAsync<T>(CancellationToken cancellationToken, string sql, params DbParameter[] parameters)
```

#### 参数

| 参数 | 类型 | 说明 |
| --- | --- | --- |
| cancellationToken | CancellationToken | 取消令牌 |
| sql | string | SQL 查询语句 |
| parameters | params DbParameter[] | SQL 参数数组 |

#### 返回值

查询结果的对象集合，类型 T 必须是引用类型。

### ExecuteDataSet

执行 SQL 查询并返回数据集。

#### 方法签名

```csharp
public DataSet ExecuteDataSet(string sql, params DbParameter[] parameters)
```

#### 参数

| 参数 | 类型 | 说明 |
| --- | --- | --- |
| sql | string | SQL 查询语句 |
| parameters | params DbParameter[] | SQL 参数数组 |

#### 返回值

包含查询结果的数据集。

### ExecuteDataTable

执行 SQL 查询并返回数据表。

#### 方法签名

```csharp
public DataTable ExecuteDataTable(string sql, params DbParameter[] parameters)
```

#### 参数

| 参数 | 类型 | 说明 |
| --- | --- | --- |
| sql | string | SQL 查询语句 |
| parameters | params DbParameter[] | SQL 参数数组 |

#### 返回值

包含查询结果的数据表。

### ExecuteNonQuery

执行 SQL 命令并返回受影响的行数。

#### 方法签名

```csharp
public int ExecuteNonQuery(string sql, params DbParameter[] parameters)
```

#### 参数

| 参数 | 类型 | 说明 |
| --- | --- | --- |
| sql | string | SQL 命令语句 |
| parameters | params DbParameter[] | SQL 参数数组 |

#### 返回值

受影响的行数。

### BeginTransaction

开始数据库事务。

#### 方法签名

```csharp
public DbTransaction BeginTransaction()
```

#### 返回值

数据库事务对象。

## 示例

### 配置全局数据库

```csharp
// SQLite
SqlHelper.Config(SqliteFactory.Instance, "data source=mydb.db");

// SQL Server
SqlHelper.Config(SqlClientFactory.Instance, "Server=localhost;Database=mydb;User Id=sa;Password=yourPassword;");

// MySQL
SqlHelper.Config(MySqlConnectorFactory.Instance, "server=localhost;user=root;password=password;database=mydb");
```

### 使用连接字符串创建实例

```csharp
// SQLite
var sqliteHelper = new SqlHelper(SqliteFactory.Instance, "data source=mydb.db");

// SQL Server
var sqlServerHelper = new SqlHelper(SqlClientFactory.Instance, "Server=localhost;Database=mydb;User Id=sa;Password=yourPassword;");

// MySQL
var mysqlHelper = new SqlHelper(MySqlConnectorFactory.Instance, "server=localhost;user=root;password=password;database=mydb");
```

### 使用 DbContext 创建实例

```csharp
using var dbContext = new MyDbContext();
var sqlHelper = new SqlHelper(dbContext);
```

### 使用全局配置创建实例

```csharp
SqlHelper.Config(SqliteFactory.Instance, "data source=mydb.db");

using var sqlHelper = new SqlHelper();
// 使用 sqlHelper 进行操作
```

### 执行 ExecuteScalar

```csharp
// 查询用户数量
int count = sqlHelper.ExecuteScalar<int>("SELECT COUNT(*) FROM Users");

// 查询用户名
string name = sqlHelper.ExecuteScalar<string>("SELECT Name FROM Users WHERE Id = @Id",
    sqlHelper.CreateParameter("@Id", 1));

// 使用参数化查询
var count2 = sqlHelper.ExecuteScalar<int>("SELECT COUNT(*) FROM Users WHERE Age > @Age",
    sqlHelper.CreateParameter("@Age", 18));
```

### 执行 Query

```csharp
// 查询所有用户
var users = sqlHelper.Query<User>("SELECT * FROM Users");

// 带条件查询
var activeUsers = sqlHelper.Query<User>("SELECT * FROM Users WHERE IsActive = @IsActive",
    sqlHelper.CreateParameter("@IsActive", true));

// 使用多个参数
var filteredUsers = sqlHelper.Query<User>("SELECT * FROM Users WHERE Age > @MinAge AND Age < @MaxAge",
    sqlHelper.CreateParameter("@MinAge", 18),
    sqlHelper.CreateParameter("@MaxAge", 60));
```

### 执行 ExecuteNonQuery

```csharp
// 插入数据
int rowsAffected = sqlHelper.ExecuteNonQuery(
    "INSERT INTO Users (Name, Age, IsActive) VALUES (@Name, @Age, @IsActive)",
    sqlHelper.CreateParameter("@Name", "张三"),
    sqlHelper.CreateParameter("@Age", 25),
    sqlHelper.CreateParameter("@IsActive", true)
);

// 更新数据
int updatedRows = sqlHelper.ExecuteNonQuery(
    "UPDATE Users SET Age = @Age WHERE Id = @Id",
    sqlHelper.CreateParameter("@Age", 26),
    sqlHelper.CreateParameter("@Id", 1)
);

// 删除数据
int deletedRows = sqlHelper.ExecuteNonQuery(
    "DELETE FROM Users WHERE Id = @Id",
    sqlHelper.CreateParameter("@Id", 1)
);
```

### 使用事务

```csharp
using var sqlHelper = new SqlHelper(SqliteFactory.Instance, "data source=mydb.db");
using var transaction = sqlHelper.BeginTransaction();

try
{
    // 执行多个操作
    sqlHelper.ExecuteNonQuery("UPDATE Account SET Balance = Balance - 100 WHERE Id = 1");
    sqlHelper.ExecuteNonQuery("UPDATE Account SET Balance = Balance + 100 WHERE Id = 2");

    // 提交事务
    transaction.Commit();
    Console.WriteLine("转账成功");
}
catch (Exception ex)
{
    // 回滚事务
    transaction.Rollback();
    Console.WriteLine($"转账失败: {ex.Message}");
}
```

### 异步操作

```csharp
// 异步查询单个值
int count = await sqlHelper.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM Users");

// 异步查询对象列表
var users = await sqlHelper.QueryAsync<User>("SELECT * FROM Users");

// 异步带取消令牌
var cts = new CancellationTokenSource();
var users = await sqlHelper.QueryAsync<User>(cts.Token, "SELECT * FROM Users");
```

### 执行存储过程

```csharp
// 执行存储过程
var parameters = new[]
{
    sqlHelper.CreateParameter("@UserId", 1),
    sqlHelper.CreateParameter("@UserName", "张三")
};

var result = sqlHelper.Query<User>("EXEC UpdateUser @UserId, @UserName", parameters);
```

### 创建参数

```csharp
// 创建参数
var param1 = sqlHelper.CreateParameter("@Id", 1);
var param2 = sqlHelper.CreateParameter("@Name", "张三");
var param3 = sqlHelper.CreateParameter("@Age", 25);

// 在查询中使用
var user = sqlHelper.Query<User>("SELECT * FROM Users WHERE Id = @Id AND Name = @Name AND Age = @Age",
    param1, param2, param3);
```

### 完整示例

```csharp
// 配置数据库
SqlHelper.Config(SqliteFactory.Instance, "data source=mydb.db");

using var sqlHelper = new SqlHelper();

// 创建表
sqlHelper.ExecuteNonQuery(@"
    CREATE TABLE IF NOT EXISTS Users (
        Id INTEGER PRIMARY KEY AUTOINCREMENT,
        Name TEXT NOT NULL,
        Age INTEGER,
        IsActive INTEGER
    )");

// 插入数据
sqlHelper.ExecuteNonQuery(
    "INSERT INTO Users (Name, Age, IsActive) VALUES (@Name, @Age, @IsActive)",
    sqlHelper.CreateParameter("@Name", "张三"),
    sqlHelper.CreateParameter("@Age", 25),
    sqlHelper.CreateParameter("@IsActive", 1)
);

// 查询数据
var users = sqlHelper.Query<User>("SELECT * FROM Users");
foreach (var user in users)
{
    Console.WriteLine($"Id: {user.Id}, Name: {user.Name}, Age: {user.Age}");
}

// 更新数据
sqlHelper.ExecuteNonQuery(
    "UPDATE Users SET Age = @Age WHERE Id = @Id",
    sqlHelper.CreateParameter("@Age", 26),
    sqlHelper.CreateParameter("@Id", 1)
);

// 删除数据
sqlHelper.ExecuteNonQuery("DELETE FROM Users WHERE Id = @Id",
    sqlHelper.CreateParameter("@Id", 1));
```

## 特性

- 支持 SQLite、SQL Server、MySQL 等多种数据库
- 支持全局配置和实例配置
- 支持从 DbContext 创建实例
- 支持参数化查询，防止 SQL 注入
- 支持同步和异步操作
- 支持取消操作
- 支持事务
- 自动管理数据库连接
- 支持查询结果自动映射到对象
