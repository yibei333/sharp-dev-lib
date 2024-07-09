using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpDevLib.Data;
using SharpDevLib.Tests.TestData;
using SharpDevLib.Tests.TestData.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace SharpDevLib.Tests.Data;

[TestClass]
public class SqlHelperTests
{
    #region Data
    static readonly string SourceFile = AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/Data/sample.db");
    static readonly string SourceConnectionString = $"data source={SourceFile}";

    static string GetFileConnectionString(string name)
    {
        var targetPath = AppDomain.CurrentDomain.BaseDirectory.CombinePath($"TestData/Tests/{name}.db");
        new FileInfo(targetPath).Directory?.FullName.EnsureDirectoryExist();
        targetPath.RemoveFileIfExist();
        using var sourceStream = new FileStream(SourceFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        using var targetStream = new FileStream(targetPath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
        sourceStream.CopyTo(targetStream);
        targetStream.Flush();
        return $"data source={targetPath}";
    }

    public static readonly List<User> Users =
    [
        new User("Foo",10),
        new User("Bar",20),
    ];

    public static readonly List<UserFavorite> UserFavorites =
    [
        new UserFavorite("Foo","Sing"),
        new UserFavorite("Foo","Dance"),
        new UserFavorite("Foo","Rap"),
        new UserFavorite("Foo","Basketball"),
        new UserFavorite("Bar","Program"),
    ];
    #endregion

    [TestMethod]
    public void ExecuteScalarTest()
    {
        SqlHelper.Config(SqliteFactory.Instance, SourceConnectionString);
        using var sqlHelper = new SqlHelper();

        Assert.AreEqual(2, sqlHelper.ExecuteScalar<int>("SELECT COUNT(1) FROM [User]"));
        Assert.AreEqual(5, sqlHelper.ExecuteScalar<int>("SELECT COUNT(1) FROM [UserFavorite]"));
        Assert.AreEqual(5, sqlHelper.ExecuteScalar<int>("SELECT COUNT(1) FROM [User] a INNER JOIN [UserFavorite] b ON a.Name=b.Name"));
        Assert.AreEqual(1, sqlHelper.ExecuteScalar<int>("SELECT COUNT(1) FROM [User] a INNER JOIN [UserFavorite] b ON a.Name=b.Name GROUP BY a.Name"));
        Assert.AreEqual("Program", sqlHelper.ExecuteScalar<string>("SELECT Favorite FROM [UserFavorite] WHERE [Name]='Bar'"));
        Assert.IsNull(sqlHelper.ExecuteScalar<string>("SELECT Favorite FROM [UserFavorite] WHERE [Name]='Baz'"));
    }

    [TestMethod]
    public async Task ExecuteScalarAsyncTest()
    {
        SqlHelper.Config(SqliteFactory.Instance, SourceConnectionString);
        using var sqlHelper = new SqlHelper();

        Assert.AreEqual(2, await sqlHelper.ExecuteScalarAsync<int>("SELECT COUNT(1) FROM [User]"));
        Assert.AreEqual(5, await sqlHelper.ExecuteScalarAsync<int>("SELECT COUNT(1) FROM [UserFavorite]"));
        Assert.AreEqual(5, await sqlHelper.ExecuteScalarAsync<int>("SELECT COUNT(1) FROM [User] a INNER JOIN [UserFavorite] b ON a.Name=b.Name"));
        Assert.AreEqual(1, await sqlHelper.ExecuteScalarAsync<int>("SELECT COUNT(1) FROM [User] a INNER JOIN [UserFavorite] b ON a.Name=b.Name GROUP BY a.Name"));
        Assert.AreEqual("Program", await sqlHelper.ExecuteScalarAsync<string>("SELECT Favorite FROM [UserFavorite] WHERE [Name]='Bar'"));
        Assert.IsNull(await sqlHelper.ExecuteScalarAsync<string>("SELECT Favorite FROM [UserFavorite] WHERE [Name]='Baz'"));
    }

    [TestMethod]
    public void ExecuteDataSetTest()
    {
        SqlHelper.Config(SqliteFactory.Instance, SourceConnectionString);
        using var sqlHelper = new SqlHelper();

        var sql = $@"
            SELECT [Name],Age FROM [User];
            SELECT [Name],Favorite FROM [UserFavorite];
            SELECT a.[Name],a.Age,b.Favorite FROM [User] a INNER JOIN [UserFavorite] b ON a.Name=b.Name;
        ";
        var dataSet = sqlHelper.ExecuteDataSet(sql);
        Assert.AreEqual(3, dataSet.Tables.Count);
        Assert.AreEqual(Users.Serialize(), dataSet.Tables[0].ToList<User>().Serialize());
        Assert.AreEqual(UserFavorites.Serialize(), dataSet.Tables[1].ToList<UserFavorite>().Serialize());
    }

    [TestMethod]
    public async Task ExecuteDataSetAsyncTest()
    {
        SqlHelper.Config(SqliteFactory.Instance, SourceConnectionString);
        using var sqlHelper = new SqlHelper();

        var sql = $@"
            SELECT [Name],Age FROM [User];
            SELECT [Name],Favorite FROM [UserFavorite];
            SELECT a.[Name],a.Age,b.Favorite FROM [User] a INNER JOIN [UserFavorite] b ON a.Name=b.Name;
        ";
        var dataSet = await sqlHelper.ExecuteDataSetAsync(sql);
        Assert.AreEqual(3, dataSet.Tables.Count);
        Assert.AreEqual(Users.Serialize(), dataSet.Tables[0].ToList<User>().Serialize());
        Assert.AreEqual(UserFavorites.Serialize(), dataSet.Tables[1].ToList<UserFavorite>().Serialize());
    }

    [TestMethod]
    public void ExecuteNoneQueryTest()
    {
        var path = GetFileConnectionString("nonquery");
        SqlHelper.Config(SqliteFactory.Instance, path);
        using var sqlHelper = new SqlHelper();

        var affectRowsCount = sqlHelper.ExecuteNonQuery($"INSERT INTO [User]([Name],Age) Values('Baz',30),('Qux',40)");
        Assert.AreEqual(4, sqlHelper.ExecuteScalar<int>("SELECT COUNT(1) FROM [User]"));
        Assert.AreEqual(2, affectRowsCount);
    }

    [TestMethod]
    public async Task ExecuteNoneQueryAsyncTest()
    {
        var path = GetFileConnectionString("nonquery-async");
        SqlHelper.Config(SqliteFactory.Instance, path);
        using var sqlHelper = new SqlHelper();

        var affectRowsCount = await sqlHelper.ExecuteNonQueryAsync($"INSERT INTO [User]([Name],Age) Values('Baz',30),('Qux',40);INSERT INTO [UserFavorite]([Name],Favorite) Values('Baz','Game');");
        Assert.AreEqual(4, sqlHelper.ExecuteScalar<int>("SELECT COUNT(1) FROM [User]"));
        Assert.AreEqual(6, sqlHelper.ExecuteScalar<int>("SELECT COUNT(1) FROM [UserFavorite]"));
        Assert.AreEqual(3, affectRowsCount);
    }

    [TestMethod]
    public void TransactionRollbackTest()
    {
        var path = GetFileConnectionString("transaction-rollback");
        SqlHelper.Config(SqliteFactory.Instance, path);
        using var sqlHelper = new SqlHelper();

        using var fooTransaction = sqlHelper.Connection.BeginTransaction();
        try
        {
            sqlHelper.ExecuteNonQuery("INSERT INTO [User]([Name],Age) Values('Baz',30),('Qux',40);");
            sqlHelper.ExecuteNonQuery("INSERT INTO [User]([Name],Age) Values('Baz',30),('Qux',40);");
            sqlHelper.ExecuteNonQuery("INSERT INTO [UserFavorite]([Name],Favorite) Values('Baz','Game');");
            fooTransaction.Commit();
        }
        catch
        {
            fooTransaction.Rollback();
        }

        Assert.AreEqual(2, sqlHelper.ExecuteScalar<int>("SELECT COUNT(1) FROM [User]"));
        Assert.AreEqual(5, sqlHelper.ExecuteScalar<int>("SELECT COUNT(1) FROM [UserFavorite]"));
    }

    [TestMethod]
    public void TransactionCommitTest()
    {
        var path = GetFileConnectionString("transaction-commit");
        SqlHelper.Config(SqliteFactory.Instance, path);
        using var sqlHelper = new SqlHelper();

        using var fooTransaction = sqlHelper.Connection.BeginTransaction();
        try
        {
            sqlHelper.ExecuteNonQuery("INSERT INTO [User]([Name],Age) Values('Baz',30),('Qux',40);");
            sqlHelper.ExecuteNonQuery("INSERT INTO [UserFavorite]([Name],Favorite) Values('Baz','Game');");
            fooTransaction.Commit();
        }
        catch
        {
            fooTransaction.Rollback();
        }

        Assert.AreEqual(4, sqlHelper.ExecuteScalar<int>("SELECT COUNT(1) FROM [User]"));
        Assert.AreEqual(6, sqlHelper.ExecuteScalar<int>("SELECT COUNT(1) FROM [UserFavorite]"));
    }

    [TestMethod]
    public void DbContextTransactionRollbackTest()
    {
        var path = GetFileConnectionString("dbcontext-transaction-rollback");
        IServiceCollection services = new ServiceCollection();
        services.AddDbContext<SampleDbContext>(x => x.UseSqlite(path));
        using var dbContext = services.BuildServiceProvider().GetRequiredService<SampleDbContext>();

        using var sqlHelper = new SqlHelper(dbContext);
        SqlHelper.Config(SqliteFactory.Instance, path);

        using var fooTransaction = sqlHelper.Connection.BeginTransaction();
        try
        {
            dbContext.User.Add(new User("Baz", 30));
            dbContext.SaveChanges();
            sqlHelper.ExecuteNonQuery("INSERT INTO [User]([Name],Age) Values('Qux',40);");
            Assert.AreEqual(4, dbContext.User.Count());
            Assert.AreEqual(4, sqlHelper.ExecuteScalar<int>("SELECT COUNT(1) FROM [User]"));

            sqlHelper.ExecuteNonQuery("INSERT INTO [User]([Name],Age) Values('Qux',40);");
            sqlHelper.ExecuteNonQuery("INSERT INTO [UserFavorite]([Name],Favorite) Values('Baz','Game');");
            fooTransaction.Commit();
        }
        catch
        {
            fooTransaction.Rollback();
        }

        Assert.AreEqual(2, sqlHelper.ExecuteScalar<int>("SELECT COUNT(1) FROM [User]"));
        Assert.AreEqual(5, sqlHelper.ExecuteScalar<int>("SELECT COUNT(1) FROM [UserFavorite]"));

        Assert.AreEqual(2, dbContext.User.Count());
        Assert.AreEqual(5, dbContext.UserFavorite.Count());
    }

    [TestMethod]
    public void DbContextTransactionCommitTest()
    {
        var path = GetFileConnectionString("dbcontext-transaction-commit");
        IServiceCollection services = new ServiceCollection();
        services.AddDbContext<SampleDbContext>(x => x.UseSqlite(path));
        using var dbContext = services.BuildServiceProvider().GetRequiredService<SampleDbContext>();

        using var sqlHelper = new SqlHelper(dbContext);
        SqlHelper.Config(SqliteFactory.Instance, path);

        using var fooTransaction = sqlHelper.Connection.BeginTransaction();
        try
        {
            dbContext.User.Add(new User("Baz", 30));
            dbContext.SaveChanges();
            sqlHelper.ExecuteNonQuery("INSERT INTO [User]([Name],Age) Values('Qux',40);");
            Assert.AreEqual(4, dbContext.User.Count());
            Assert.AreEqual(4, sqlHelper.ExecuteScalar<int>("SELECT COUNT(1) FROM [User]"));

            sqlHelper.ExecuteNonQuery("INSERT INTO [UserFavorite]([Name],Favorite) Values('Baz','Game');");
            fooTransaction.Commit();
        }
        catch
        {
            fooTransaction.Rollback();
        }

        Assert.AreEqual(4, sqlHelper.ExecuteScalar<int>("SELECT COUNT(1) FROM [User]"));
        Assert.AreEqual(6, sqlHelper.ExecuteScalar<int>("SELECT COUNT(1) FROM [UserFavorite]"));

        Assert.AreEqual(4, dbContext.User.Count());
        Assert.AreEqual(6, dbContext.UserFavorite.Count());

        //temptable
        sqlHelper.ExecuteNonQuery("CREATE TEMP TABLE TempTable([Name] TEXT,Age INT);INSERT INTO TempTable VALUES('foo',10),('bar',70)");
        var table = sqlHelper.ExecuteDataSet("SELECT * FROM TempTable").Tables[0];
        var users = dbContext.Database.SqlQuery<User>(FormattableStringFactory.Create("SELECT * FROM TempTable")).ToList();
        Console.WriteLine(users.Serialize());
    }
}
