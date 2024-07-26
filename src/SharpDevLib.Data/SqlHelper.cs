using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.Common;
using System.Reflection;

namespace SharpDevLib.Data;

/// <summary>
/// Sql帮助类
/// </summary>
public sealed class SqlHelper : IDisposable
{
    #region Gloabl
    static DbProviderFactory? GlobalDbProviderFactory { get; set; }

    static string? GlobalConnectionString { get; set; }

    /// <summary>
    /// 设置全局配置
    /// </summary>
    /// <param name="dbProviderFactory">数据库提供商工厂</param>
    /// <param name="connectionString">连接字符串</param>
    public static void Config(DbProviderFactory dbProviderFactory, string connectionString)
    {
        GlobalDbProviderFactory = dbProviderFactory;
        GlobalConnectionString = connectionString;
    }
    #endregion

    /// <summary>
    /// 实例化Sql帮助类
    /// </summary>
    /// <param name="dbProviderFactory">数据库提供商工厂,例如
    /// <para>1.引用Microsoft.Data.Sqlite,则用SqliteFactory.Instance</para>
    /// <para>2.引用Microsoft.Data.SqlClient,则用SqlClientFactory.Instance</para>
    /// <para>3.引用Pomelo.EntityFrameworkCore.MySql,则用MySqlConnectorFactory.Instance</para>
    /// </param>
    /// <param name="connectionString">连接字符串
    /// <para>1.Sqlite,"data source=dbFilePath"</para>
    /// <para>2.SqlServer,"Server=server;Database=database;User Id=user;Password=password;"</para>
    /// <para>3.MySql,"server=server;user=user;password=password;database=database"</para>
    /// </param>
    public SqlHelper(DbProviderFactory dbProviderFactory, string connectionString)
    {
        DbProviderFactory = dbProviderFactory;
        Connection = DbProviderFactory.CreateConnection();
        Connection.ConnectionString = connectionString;
        Connection.Open();
    }
    /// <summary>
    /// 实例化Sql帮助类
    /// </summary>
    /// <exception cref="Exception">在没有全局配置时引发异常</exception>
    public SqlHelper()
    {
        if (GlobalDbProviderFactory is null || GlobalConnectionString.IsNullOrWhiteSpace()) throw new Exception($"please call SqlHelper.Config() method first or provide parameters");

        DbProviderFactory = GlobalDbProviderFactory;
        Connection = DbProviderFactory.CreateConnection();
        Connection.ConnectionString = GlobalConnectionString;
        Connection.Open();
    }

    /// <summary>
    /// 实例化Sql帮助类
    /// </summary>
    /// <param name="dbContext">DbContext</param>
    /// <exception cref="Exception">当获取DbCproviderFactory失败时引发异常</exception>
    public SqlHelper(DbContext dbContext)
    {
        Connection = dbContext.Database.GetDbConnection();
        DbProviderFactory = (Connection.GetType().GetProperties(BindingFlags.NonPublic | BindingFlags.Instance).FirstOrDefault(x => x.Name == nameof(DbProviderFactory))?.GetValue(Connection) as DbProviderFactory) ?? throw new Exception("unable to get DbProviderFactory from DbConnection");
        Connection.Open();
    }

    /// <summary>
    /// 连接
    /// </summary>
    public DbConnection Connection { get; }

    /// <summary>
    /// 数据库提供商工厂
    /// </summary>
    public DbProviderFactory DbProviderFactory { get; }

    /// <summary>
    /// 检索单个值
    /// </summary>
    /// <typeparam name="T">返回值的类型</typeparam>
    /// <param name="sql">sql语句</param>
    /// <param name="parameters">sql参数</param>
    /// <returns>类型为T的值</returns>
    /// <exception cref="Exception">当T的类型为引用类型时(排除string类型)引发异常</exception>
    public T ExecuteScalar<T>(string sql, params DbParameter[] parameters)
    {
        var type = typeof(T);
        if (!type.IsValueType && type != typeof(string)) throw new Exception($"type of T must be ValueType or String");

        using var command = CreateCommand(sql, parameters);
        var result = command.ExecuteScalar();
        return (T)Convert.ChangeType(result, type);
    }

    /// <summary>
    /// 检索单个值
    /// </summary>
    /// <typeparam name="T">返回值的类型</typeparam>
    /// <param name="sql">sql语句</param>
    /// <param name="parameters">sql参数</param>
    /// <returns>类型为T的值</returns>
    /// <exception cref="Exception">当T的类型为引用类型时(排除string类型)引发异常</exception>
    public async Task<T> ExecuteScalarAsync<T>(string sql, params DbParameter[] parameters)
    {
        var type = typeof(T);
        if (!type.IsValueType && type != typeof(string)) throw new Exception($"type of T must be ValueType or String");

        using var command = CreateCommand(sql, parameters);
        var result = await command.ExecuteScalarAsync();
        return (T)Convert.ChangeType(result, type);
    }

    /// <summary>
    /// 检索单个值
    /// </summary>
    /// <typeparam name="T">返回值的类型</typeparam>
    /// <param name="sql">sql语句</param>
    /// <param name="parameters">sql参数</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>类型为T的值</returns>
    /// <exception cref="Exception">当T的类型为引用类型时(排除string类型)引发异常</exception>
    public async Task<T?> ExecuteScalarAsync<T>(string sql, DbParameter[] parameters, CancellationToken cancellationToken)
    {
        var type = typeof(T);
        if (!type.IsValueType && type != typeof(string)) throw new Exception($"type of T must be ValueType or String");

        using var command = CreateCommand(sql, parameters);
        var result = await command.ExecuteScalarAsync(cancellationToken);
        return (T)Convert.ChangeType(result, type);
    }

    /// <summary>
    /// 检索数据集
    /// </summary>
    /// <param name="sql">sql语句</param>
    /// <param name="parameters">sql参数</param>
    /// <returns>数据集</returns>
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
    /// 检索数据集
    /// </summary>
    /// <param name="sql">sql语句</param>
    /// <param name="parameters">sql参数</param>
    /// <returns>数据集</returns>
    public async Task<DataSet> ExecuteDataSetAsync(string sql, params DbParameter[] parameters)
    {
        return await Task.Run(() => ExecuteDataSet(sql, parameters));
    }

    /// <summary>
    /// 检索数据集
    /// </summary>
    /// <param name="sql">sql语句</param>
    /// <param name="parameters">sql参数</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>数据集</returns>
    public async Task<DataSet> ExecuteDataSetAsync(string sql, DbParameter[] parameters, CancellationToken cancellationToken)
    {
        return await Task.Run(() => ExecuteDataSet(sql, parameters), cancellationToken);
    }

    /// <summary>
    /// 执行非查询sql语句
    /// </summary>
    /// <param name="sql">sql语句</param>
    /// <param name="parameters">sql参数</param>
    /// <returns>受影响的行数</returns>
    public int ExecuteNonQuery(string sql, params DbParameter[] parameters)
    {
        using var command = CreateCommand(sql, parameters);
        return command.ExecuteNonQuery();
    }

    /// <summary>
    /// 执行非查询sql语句
    /// </summary>
    /// <param name="sql">sql语句</param>
    /// <param name="parameters">sql参数</param>
    /// <returns>受影响的行数</returns>
    public async Task<int> ExecuteNonQueryAsync(string sql, params DbParameter[] parameters)
    {
        using var command = CreateCommand(sql, parameters);
        return await command.ExecuteNonQueryAsync();
    }

    /// <summary>
    /// 执行非查询sql语句
    /// </summary>
    /// <param name="sql">sql语句</param>
    /// <param name="parameters">sql参数</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>受影响的行数</returns>
    public async Task<int> ExecuteNonQueryAsync(string sql, DbParameter[] parameters, CancellationToken cancellationToken)
    {
        using var command = CreateCommand(sql, parameters);
        return await command.ExecuteNonQueryAsync(cancellationToken);
    }

    /// <summary>
    /// dispose the connection
    /// </summary>
    public void Dispose()
    {
        Connection?.Dispose();
    }

    DbCommand CreateCommand(string sql, params DbParameter[] parameters)
    {
        var command = Connection.CreateCommand();
        command.CommandText = sql;
        command.Parameters.AddRange(parameters);
        return command;
    }
}
