using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.Common;
using System.Reflection;

namespace SharpDevLib;

/// <summary>
/// SQL帮助类，提供数据库连接、查询和执行SQL语句的功能
/// </summary>
public sealed class SqlHelper : IDisposable
{
    #region Gloabl
    static DbProviderFactory? GlobalDbProviderFactory { get; set; }

    static string? GlobalConnectionString { get; set; }

    /// <summary>
    /// 设置全局数据库配置
    /// </summary>
    /// <param name="dbProviderFactory">数据库提供商工厂，支持的工厂如下：
    /// <para>1. 引用Microsoft.Data.Sqlite，则用SqliteFactory.Instance</para>
    /// <para>2. 引用Microsoft.Data.SqlClient，则用SqlClientFactory.Instance</para>
    /// <para>3. 引用Pomelo.EntityFrameworkCore.MySql，则用MySqlConnectorFactory.Instance</para>
    /// </param>
    /// <param name="connectionString">数据库连接字符串，格式如下：
    /// <para>1. SQLite："data source=dbFilePath"</para>
    /// <para>2. SQL Server："Server=server;Database=database;User Id=user;Password=password;"</para>
    /// <para>3. MySQL："server=server;user=user;password=password;database=database"</para>
    /// </param>
    public static void Config(DbProviderFactory dbProviderFactory, string connectionString)
    {
        GlobalDbProviderFactory = dbProviderFactory;
        GlobalConnectionString = connectionString;
    }
    #endregion

    /// <summary>
    /// 初始化SQL帮助类示例，使用指定的数据库提供商工厂和连接字符串
    /// </summary>
    /// <param name="dbProviderFactory">数据库提供商工厂，支持的工厂如下：
    /// <para>1. 引用Microsoft.Data.Sqlite，则用SqliteFactory.Instance</para>
    /// <para>2. 引用Microsoft.Data.SqlClient，则用SqlClientFactory.Instance</para>
    /// <para>3. 引用Pomelo.EntityFrameworkCore.MySql，则用MySqlConnectorFactory.Instance</para>
    /// </param>
    /// <param name="connectionString">数据库连接字符串，格式如下：
    /// <para>1. SQLite："data source=dbFilePath"</para>
    /// <para>2. SQL Server："Server=server;Database=database;User Id=user;Password=password;"</para>
    /// <para>3. MySQL："server=server;user=user;password=password;database=database"</para>
    /// </param>
    public SqlHelper(DbProviderFactory dbProviderFactory, string connectionString)
    {
        DbProviderFactory = dbProviderFactory;
        Connection = DbProviderFactory.CreateConnection();
        Connection.ConnectionString = connectionString;
        Connection.Open();
    }

    /// <summary>
    /// 初始化SQL帮助类示例，使用全局配置的数据库提供商工厂和连接字符串
    /// </summary>
    /// <exception cref="Exception">当未调用SqlHelper.Config()方法设置全局配置时抛出异常</exception>
    public SqlHelper()
    {
        if (GlobalDbProviderFactory is null || GlobalConnectionString.IsNullOrWhiteSpace()) throw new Exception($"please call SqlHelper.Config() method first or provide parameters");

        DbProviderFactory = GlobalDbProviderFactory;
        Connection = DbProviderFactory.CreateConnection();
        Connection.ConnectionString = GlobalConnectionString;
        Connection.Open();
    }

    /// <summary>
    /// 初始化SQL帮助类示例，使用现有的DbContext
    /// </summary>
    /// <param name="dbContext">Entity Framework的DbContext示例</param>
    /// <exception cref="Exception">当无法从DbConnection获取DbProviderFactory时抛出异常</exception>
    public SqlHelper(DbContext dbContext)
    {
        Connection = dbContext.Database.GetDbConnection();
        DbProviderFactory = (Connection.GetType().GetProperties(BindingFlags.NonPublic | BindingFlags.Instance).FirstOrDefault(x => x.Name == nameof(DbProviderFactory))?.GetValue(Connection) as DbProviderFactory) ?? throw new Exception("无法从DbConnection获取DbProviderFactory");
        Connection.Open();
    }

    /// <summary>
    /// 获取数据库连接对象
    /// </summary>
    public DbConnection Connection { get; }

    /// <summary>
    /// 获取数据库提供商工厂
    /// </summary>
    public DbProviderFactory DbProviderFactory { get; }

    /// <summary>
    /// 执行SQL查询并返回单个值
    /// </summary>
    /// <typeparam name="T">返回值的类型，必须是值类型或String</typeparam>
    /// <param name="sql">SQL查询语句</param>
    /// <param name="parameters">SQL参数数组</param>
    /// <returns>查询结果的单个值</returns>
    /// <exception cref="Exception">当T的类型为引用类型（排除string类型）时抛出异常</exception>
    /// <exception cref="InvalidCastException">当T的类型为值类型且返回结果为null时抛出异常</exception>
    public T ExecuteScalar<T>(string sql, params DbParameter[] parameters)
    {
        var type = typeof(T);
        if (!type.IsValueType && type != typeof(string)) throw new Exception($"当前只支持值类型和string类型");

        using var command = CreateCommand(sql, parameters);
        var result = command.ExecuteScalar();
        if ((result is null || result == DBNull.Value) && type.IsValueType) throw new InvalidCastException($"不能将NULL转换为类型'{type.FullName}'");
        return (T)Convert.ChangeType(result, type);
    }

    /// <summary>
    /// 异步执行SQL查询并返回单个值
    /// </summary>
    /// <typeparam name="T">返回值的类型，必须是值类型或String</typeparam>
    /// <param name="sql">SQL查询语句</param>
    /// <param name="parameters">SQL参数数组</param>
    /// <returns>查询结果的单个值</returns>
    /// <exception cref="Exception">当T的类型为引用类型（排除string类型）时抛出异常</exception>
    /// <exception cref="InvalidCastException">当T的类型为值类型且返回结果为null时抛出异常</exception>
    public async Task<T> ExecuteScalarAsync<T>(string sql, params DbParameter[] parameters)
    {
        return await ExecuteScalarAsync<T>(CancellationToken.None, sql, parameters);
    }

    /// <summary>
    /// 异步执行SQL查询并返回单个值，支持取消操作
    /// </summary>
    /// <typeparam name="T">返回值的类型，必须是值类型或String</typeparam>
    /// <param name="cancellationToken">取消令牌</param>
    /// <param name="sql">SQL查询语句</param>
    /// <param name="parameters">SQL参数数组</param>
    /// <returns>查询结果的单个值</returns>
    /// <exception cref="Exception">当T的类型为引用类型（排除string类型）时抛出异常</exception>
    /// <exception cref="InvalidCastException">当T的类型为值类型且返回结果为null时抛出异常</exception>
    public async Task<T> ExecuteScalarAsync<T>(CancellationToken cancellationToken, string sql, params DbParameter[] parameters)
    {
        var type = typeof(T);
        if (!type.IsValueType && type != typeof(string)) throw new Exception($"当前只支持值类型和string类型");

        using var command = CreateCommand(sql, parameters);
        var result = await command.ExecuteScalarAsync(cancellationToken);
        if ((result is null || result == DBNull.Value) && type.IsValueType) throw new InvalidCastException($"不能将NULL转换为类型'{type.FullName}'");
        return (T)Convert.ChangeType(result, type);
    }

    /// <summary>
    /// 执行SQL查询并返回对象列表
    /// </summary>
    /// <typeparam name="T">返回对象的类型，必须是引用类型</typeparam>
    /// <param name="sql">SQL查询语句</param>
    /// <param name="parameters">SQL参数数组</param>
    /// <returns>查询结果的对象集合</returns>
    public IEnumerable<T> Query<T>(string sql, params DbParameter[] parameters) where T : class
    {
        var table = ExecuteDataTable(sql, parameters);
        return table.ToList<T>();
    }

    /// <summary>
    /// 异步执行SQL查询并返回对象列表
    /// </summary>
    /// <typeparam name="T">返回对象的类型，必须是引用类型</typeparam>
    /// <param name="sql">SQL查询语句</param>
    /// <param name="parameters">SQL参数数组</param>
    /// <returns>查询结果的对象集合</returns>
    public async Task<IEnumerable<T>> QueryAsync<T>(string sql, params DbParameter[] parameters) where T : class
    {
        var table = await ExecuteDataTableAsync(sql, parameters);
        return table.ToList<T>();
    }

    /// <summary>
    /// 异步执行SQL查询并返回对象列表，支持取消操作
    /// </summary>
    /// <typeparam name="T">返回对象的类型，必须是引用类型</typeparam>
    /// <param name="cancellationToken">取消令牌</param>
    /// <param name="sql">SQL查询语句</param>
    /// <param name="parameters">SQL参数数组</param>
    /// <returns>查询结果的对象集合</returns>
    public async Task<IEnumerable<T>> QueryAsync<T>(CancellationToken cancellationToken, string sql, params DbParameter[] parameters) where T : class
    {
        var table = await ExecuteDataTableAsync(cancellationToken, sql, parameters);
        return table.ToList<T>();
    }

    /// <summary>
    /// 执行SQL查询并返回数据集
    /// </summary>
    /// <param name="sql">SQL查询语句</param>
    /// <param name="parameters">SQL参数数组</param>
    /// <returns>包含查询结果的数据集</returns>
    public DataSet ExecuteDataSet(string sql, params DbParameter[] parameters)
    {
        using var command = CreateCommand(sql, parameters);
        using var adapter = DbProviderFactory.CreateDataAdapter();
        var dataset = new DataSet();

        if (adapter is not null)
        {
            adapter.SelectCommand = command;
            adapter.Fill(dataset);
        }
        else
        {
            using var reader = command.ExecuteReader();

            do
            {
                var dataTable = new DataTable();
                dataset.Tables.Add(dataTable);

                var columns = reader.GetColumnSchema();
                if (columns.GroupBy(x => x.BaseTableName).Count() == 1)
                {
                    var tableName = columns.FirstOrDefault()?.BaseTableName;
                    if (tableName.NotNullOrWhiteSpace())
                    {
                        if (dataset.Tables[tableName] is null) dataTable.TableName = tableName;
                    }
                }

                foreach (var item in columns)
                {
                    dataTable.Columns.Add(new DataColumn(item.ColumnName, item.DataType));
                }

                while (reader.Read())
                {
                    var row = dataTable.NewRow();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        row[i] = reader[i];
                    }
                    dataTable.Rows.Add(row);
                }
            }
            while (reader.NextResult());
        }
        return dataset;
    }

    /// <summary>
    /// 异步执行SQL查询并返回数据集
    /// </summary>
    /// <param name="sql">SQL查询语句</param>
    /// <param name="parameters">SQL参数数组</param>
    /// <returns>包含查询结果的数据集</returns>
    public async Task<DataSet> ExecuteDataSetAsync(string sql, params DbParameter[] parameters)
    {
        return await Task.Run(() => ExecuteDataSet(sql, parameters));
    }

    /// <summary>
    /// 异步执行SQL查询并返回数据集，支持取消操作
    /// </summary>
    /// <param name="cancellationToken">取消令牌</param>
    /// <param name="sql">SQL查询语句</param>
    /// <param name="parameters">SQL参数数组</param>
    /// <returns>包含查询结果的数据集</returns>
    public async Task<DataSet> ExecuteDataSetAsync(CancellationToken cancellationToken, string sql, params DbParameter[] parameters)
    {
        return await Task.Run(() => ExecuteDataSet(sql, parameters), cancellationToken);
    }

    /// <summary>
    /// 执行SQL查询并返回数据表
    /// </summary>
    /// <param name="sql">SQL查询语句</param>
    /// <param name="parameters">SQL参数数组</param>
    /// <returns>包含查询结果的数据表</returns>
    public DataTable ExecuteDataTable(string sql, params DbParameter[] parameters)
    {
        var set = ExecuteDataSet(sql, parameters);
        var table = set.Tables[0];
        set.Tables.Remove(table);
        return table;
    }

    /// <summary>
    /// 异步执行SQL查询并返回数据表
    /// </summary>
    /// <param name="sql">SQL查询语句</param>
    /// <param name="parameters">SQL参数数组</param>
    /// <returns>包含查询结果的数据表</returns>
    public async Task<DataTable> ExecuteDataTableAsync(string sql, params DbParameter[] parameters)
    {
        return await Task.Run(() => ExecuteDataTable(sql, parameters));
    }

    /// <summary>
    /// 异步执行SQL查询并返回数据表，支持取消操作
    /// </summary>
    /// <param name="cancellationToken">取消令牌</param>
    /// <param name="sql">SQL查询语句</param>
    /// <param name="parameters">SQL参数数组</param>
    /// <returns>包含查询结果的数据表</returns>
    public async Task<DataTable> ExecuteDataTableAsync(CancellationToken cancellationToken, string sql, params DbParameter[] parameters)
    {
        return await Task.Run(() => ExecuteDataTable(sql, parameters), cancellationToken);
    }

    /// <summary>
    /// 执行非查询SQL语句（如INSERT、UPDATE、DELETE）
    /// </summary>
    /// <param name="sql">SQL语句</param>
    /// <param name="parameters">SQL参数数组</param>
    /// <returns>受影响的行数</returns>
    public int ExecuteNonQuery(string sql, params DbParameter[] parameters)
    {
        using var command = CreateCommand(sql, parameters);
        return command.ExecuteNonQuery();
    }

    /// <summary>
    /// 异步执行非查询SQL语句（如INSERT、UPDATE、DELETE）
    /// </summary>
    /// <param name="sql">SQL语句</param>
    /// <param name="parameters">SQL参数数组</param>
    /// <returns>受影响的行数</returns>
    public async Task<int> ExecuteNonQueryAsync(string sql, params DbParameter[] parameters)
    {
        using var command = CreateCommand(sql, parameters);
        return await command.ExecuteNonQueryAsync();
    }

    /// <summary>
    /// 异步执行非查询SQL语句（如INSERT、UPDATE、DELETE），支持取消操作
    /// </summary>
    /// <param name="cancellationToken">取消令牌</param>
    /// <param name="sql">SQL语句</param>
    /// <param name="parameters">SQL参数数组</param>
    /// <returns>受影响的行数</returns>
    public async Task<int> ExecuteNonQueryAsync(CancellationToken cancellationToken, string sql, params DbParameter[] parameters)
    {
        using var command = CreateCommand(sql, parameters);
        return await command.ExecuteNonQueryAsync(cancellationToken);
    }

    /// <summary>
    /// 开启事务
    /// </summary>
    public DbTransaction BeginTransaction()
    {
        CurrentTransaction = Connection.BeginTransaction();
        return CurrentTransaction;
    }

    /// <summary>
    /// 使用事务
    /// </summary>
    /// <param name="transaction">事务</param>
    public void UseTransaction(DbTransaction transaction)
    {
        if (transaction.Connection != Connection) throw new Exception("事务不属于当前连接");
        CurrentTransaction = transaction;
    }

    /// <summary>
    /// 获取当前事务
    /// </summary>
    public DbTransaction? CurrentTransaction { get; private set; }

    /// <summary>
    /// 释放数据库连接资源
    /// </summary>
    public void Dispose()
    {
        Connection?.Dispose();
    }

    DbCommand CreateCommand(string sql, params DbParameter[] parameters)
    {
        var command = Connection.CreateCommand();
        if (CurrentTransaction is not null && CurrentTransaction.Connection == Connection) command.Transaction = CurrentTransaction;
        command.CommandText = sql;
        command.Parameters.AddRange(parameters);
        return command;
    }
}
