## ```1.ToDataTable<T>```
集合转DataTable
* T为非string的引用类型
* 以下元素属性将被忽略
    * 属性不可读
    * 属性类型为Class(string除外)
    * 被new关键字覆盖的基类属性

``` csharp
//示例
public void UserListToTable()
{
    var users=new List<User>
    {
        new User{Id=1,Name="foo",Age=10,ExtraData=new DataDto<int>(100)},
        new User{Id=2,Name="bar",Age=20,ExtraData=new DataDto<int>(100)},
    };
    var dataTable=users.ToDataTable();
    //dataTable结果
    // Id   Name    Age
    // 1    foo     10
    // 2    bar     20
}

public class User
{
    public int Id{get;set;}
    public string? Name{get;set;}
    public int Age{get;set;}
    //属性将被忽略
    public DataDto<int>? ExtraData{get;set;}
}
```

## ```2.ToList<T>```
DataTable转集合
* T为非string的引用类型
* 以下元素属性赋值将被忽略
    * 非公共属性
    * 属性不可写
    * 属性类型为Class(string除外)
    * 被new关键字覆盖的基类属性
    * 属性名称不存在

``` csharp
//示例
public void TableToUserList()
{
    var dataTable=GetTable();
    var users = dataTable.ToList<User>();
    //users结果
    //[
    //  {
    //    "Age": 0,
    //    "ExtraData": null,
    //    "Id": 1,
    //    "Name": "foo"
    //  },
    //  {
    //    "Age": 0,
    //    "ExtraData": null,
    //    "Id": 2,
    //    "Name": "bar"
    //  }
    //]
}

public DataTable GetTable()
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

    return dataTable;
}

public class User
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int Age { get; set; }
    public DataDto<int>? ExtraData { get; set; }
}
```

## ```3.Transfer```
源DataTable转换为目标DataTable

``` csharp
//示例
public void TableTransfer()
{
    var sourceTable=GetSourceTable();
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
    //targetTable结果
    // Name    *是否管理者
    // foo     是
    // bar     否
}

public DataTable GetSourceTable()
{
    var dataTable = new DataTable();
    dataTable.Columns.Add(new DataColumn("Id"));
    dataTable.Columns.Add(new DataColumn("Name"));
    dataTable.Columns.Add(new DataColumn("IsManager",typeof(bool)));

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

```

## ```4.DataTableTransferColumn```

``` csharp
/// <summary>
/// DataTable转换列
/// </summary>
public class DataTableTransferColumn
{
    /// <summary>
    /// 实例化DataTable转换列
    /// </summary>
    /// <param name="name">源DataTable中的列名,完全匹配,注意空格和*号</param>
    public DataTableTransferColumn(string name)
    {
        Name = name;
    }

    /// <summary>
    /// 源DataTable中的列名,完全匹配,注意空格和*号
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 是否必需,如果是则在列名前面加*号
    /// </summary>
    public bool IsRequired { get; set; }

    /// <summary>
    /// 目标列的类型,如果不设置则和源列的类型保持一致,注意和ValueConverter返回的数据类型一致
    /// </summary>
    public Type? TargetType { get; set; }

    /// <summary>
    /// 值转换器,参数说明如下
    /// <para>1.第一个参数为源单元格的值</para>
    /// <para>2.第二个参数为DataRow</para>
    /// <para>3.第三个参数为需返回转换后的结果,注意返回的类型需要和TargetType类型一致</para>
    /// </summary>
    public Func<object, DataRow, object>? ValueConverter { get; set; }

    /// <summary>
    /// 列明转换器,参数说明如下
    /// <para>1.第一个参数为源列名</para>
    /// <para>2.第二个参数为需返回转换后的列名</para>
    /// </summary>
    public Func<string, string>? NameConverter { get; set; }
}
```