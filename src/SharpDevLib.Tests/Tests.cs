using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpDevLib.Data;
using SharpDevLib.Tests.TestData;
using SharpDevLib.Tests.TestData.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace SharpDevLib.Tests.Data;

[TestClass]
public class Tests
{
    [TestMethod]
    public void UserListToTable()
    {
        var users = new List<User>
        {
            new User{Id=1,Name="foo",Age=10,ExtraData=new DataDto<int>(100)},
            new User{Id=2,Name="bar",Age=20,ExtraData=new DataDto<int>(100)},
        };
        var dataTable = users.ToDataTable();

    }

    [TestMethod]
    public void TableToUserList()
    {
        var dataTable = new DataTable();
        dataTable.Columns.Add(new DataColumn("Id"));
        dataTable.Columns.Add(new DataColumn("Name"));
        dataTable.Columns.Add(new DataColumn("Favorite"));

        var fooRow = dataTable.NewRow();
        fooRow["Id"] = 1;
        fooRow["Name"] = "foo";
        fooRow["Favorite"] = "Sing";
        dataTable.Rows.Add(fooRow);

        var barRow = dataTable.NewRow();
        barRow["Id"] = 2;
        barRow["Name"] = "bar";
        barRow["Favorite"] = "Dance,Rap,Basketball";
        dataTable.Rows.Add(barRow);


        var users = dataTable.ToList<User>();
        Console.WriteLine(users.Serialize(new JsonOption { FormatJson = true }));
    }

    public class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Age { get; set; }
        public DataDto<int>? ExtraData { get; set; }
    }

    [TestMethod]
    public void TableTransfer()
    {
        var sourceTable = GetSourceTable();
        //sourceTable结果
        // Id   Name    Age
        // 1    foo     10
        // 2    bar     20

        var columns = new DataTableTransferColumn[]
        {
            new DataTableTransferColumn("Name"),
            new DataTableTransferColumn("IsManager")
            {
                IsRequired = true,
                NameConverter=(name)=>"是否管理者",
                TargetType=typeof(string),
                ValueConverter=(value,row)=>bool.Parse(value.ToString()!)?"是":"否"
            },
        };
        var targetTable = sourceTable.Transfer(columns);
    }

    public DataTable GetSourceTable()
    {
        var dataTable = new DataTable();
        dataTable.Columns.Add(new DataColumn("Id"));
        dataTable.Columns.Add(new DataColumn("Name"));
        dataTable.Columns.Add(new DataColumn("IsManager", typeof(bool)));

        var fooRow = dataTable.NewRow();
        fooRow["Id"] = 1;
        fooRow["Name"] = "foo";
        fooRow["IsManager"] = true;
        dataTable.Rows.Add(fooRow);

        var barRow = dataTable.NewRow();
        barRow["Id"] = 2;
        barRow["Name"] = "bar";
        barRow["IsManager"] = false;
        dataTable.Rows.Add(barRow);

        return dataTable;
    }
}
