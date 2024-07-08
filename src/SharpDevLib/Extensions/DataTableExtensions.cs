using System.Data;

namespace SharpDevLib;

/// <summary>
/// DataTable扩展
/// </summary>
public static class DataTableExtensions
{
    /// <summary>
    /// 将集合转换为DataTable,以下元素属性将被忽略:
    /// <para>1.属性不可读</para>
    /// <para>2.属性类型为Class(string除外)</para>
    /// <para>3.被new关键字覆盖的基类属性</para>
    /// </summary>
    /// <typeparam name="T">集合元素类型</typeparam>
    /// <param name="source">集合</param>
    /// <returns>DataTable</returns>
    public static DataTable ToDataTable<T>(this IEnumerable<T> source) where T : class
    {
        var table = new DataTable();
        var index = 0;
        var properties = typeof(T)
            .GetProperties()
            .Where(x => x.CanRead && (!x.PropertyType.IsClass || (x.PropertyType.IsClass && x.PropertyType == typeof(string))))
            .Select(x => new { Property = x, Index = index++ })
            .GroupBy(x => x.Property.Name)//handle property with new keyword
            .Select(x => x.OrderBy(x => x.Index).First().Property)
            .ToList();

        //column
        properties.ForEach(property =>
        {
            var columnType = GetNonGenericPropertyTypeToColumnType(property.PropertyType);
            table.Columns.Add(new DataColumn(property.Name, columnType));
        });

        //values
        foreach (var item in source)
        {
            var row = table.NewRow();
            properties.ForEach(property =>
            {
                var value = property.GetValue(item);
                row[property.Name] = ConvertPropertyValueToRowValue(property.PropertyType, property.GetValue(item));
            });
            table.Rows.Add(row);
        }
        return table;
    }

    /// <summary>
    /// 将DataTable转换为列表,以下元素属性复制将被忽略:
    /// <para>1.非公共属性</para>
    /// <para>2.属性不可写</para>
    /// <para>3.属性类型为Class(string除外)</para>
    /// <para>4.被new关键字覆盖的基类属性</para>
    /// <para>5.属性名称不存在</para>
    /// </summary>
    /// <typeparam name="T">列表元素类型</typeparam>
    /// <param name="table">DataTable</param>
    /// <returns>列表</returns>
    public static List<T> ToList<T>(this DataTable table) where T : class
    {
        var result = new List<T>();
        var index = 0;
        var properties = typeof(T)
            .GetProperties()
            .Where(x => x.CanWrite && (!x.PropertyType.IsClass || (x.PropertyType.IsClass && x.PropertyType == typeof(string))))
            .Select(x => new { Property = x, Index = index++ })
            .GroupBy(x => x.Property.Name)//handle property with new keyword
            .Select(x => x.OrderBy(x => x.Index).First().Property)
            .ToList();

        var hasNoParameterConstructor = typeof(T).GetConstructors().All(x => x.GetParameters().Count() != 0);
        foreach (DataRow row in table.Rows)
        {
            if (hasNoParameterConstructor)
            {
                var args = new List<object?>();
                foreach (DataColumn column in table.Columns)
                {
                    var columnName = column.ColumnName;
                    var property = properties.FirstOrDefault(x => x.Name == columnName);
                    if (property is null) continue;
                    var value = ConvertRowValueToPropertyValue(property.PropertyType, row[columnName]);
                    args.Add(value);
                }
                var instance = (T)Activator.CreateInstance(typeof(T),args.ToArray());
                result.Add(instance);
            }
            else
            {
                var instance = Activator.CreateInstance<T>();
                foreach (DataColumn column in table.Columns)
                {
                    var columnName = column.ColumnName;
                    var property = properties.FirstOrDefault(x => x.Name == columnName);
                    if (property is null) continue;
                    property.SetValue(instance, ConvertRowValueToPropertyValue(property.PropertyType, row[columnName]));
                }
                result.Add(instance);
            }
        }

        return result;
    }

    /// <summary>
    /// 根据提供的列对DataTable进行转换,并返回新的DataTable
    /// </summary>
    /// <param name="sourceTable">源DataTable</param>
    /// <param name="columns">要转换的列</param>
    /// <returns>目标DataTable</returns>
    public static DataTable Transfer(this DataTable sourceTable, params DataTableTransferColumn[] columns)
    {
        var table = new DataTable();

        //columns
        foreach (var column in columns)
        {
            var columnNamePrefix = column.IsRequired ? "* " : "";
            var columnName = (column.NameConverter ?? DefaultNameConverter).Invoke(column.Name);
            var columnType = column.TargetType ?? sourceTable.Columns[column.Name]?.DataType ?? typeof(string);
            table.Columns.Add(new DataColumn($"{columnNamePrefix}{columnName}", columnType));
        }

        //values
        foreach (DataRow sourceRow in sourceTable.Rows)
        {
            var row = table.NewRow();
            for (int i = 0; i < columns.Length; i++)
            {
                var column = columns[i];
                var value = sourceRow[column.Name];
                row[i] = (column.ValueConverter ?? DefaultValueConverter).Invoke(value, row);
            }
            table.Rows.Add(row);
        }

        return table;
    }

    static Type GetNonGenericPropertyTypeToColumnType(Type propertyType)
    {
        if (propertyType.IsGenericType)
        {
            if (propertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                return GetNonGenericPropertyTypeToColumnType(propertyType.GetGenericArguments()[0]);
            }
            else throw new NotSupportedException($"property type '{propertyType.FullName}' not supported");
        }

        return propertyType;
    }

    static object ConvertPropertyValueToRowValue(Type propertyType, object propertyValue)
    {
        if (propertyValue is null) return DBNull.Value;
        return Convert.ChangeType(propertyValue, GetNonGenericPropertyTypeToColumnType(propertyType));
    }

    static object? ConvertRowValueToPropertyValue(Type propertyType, object rowValue)
    {
        if (rowValue is null || rowValue == DBNull.Value || rowValue.ToString().IsNullOrWhiteSpace()) return null;
        var type = GetNonGenericPropertyTypeToColumnType(propertyType);

        if (type.IsEnum)
        {
            if (int.TryParse(rowValue.ToString(), out var intValue))
            {
                var enumValue = Convert.ChangeType(Enum.ToObject(type, intValue), type);
                return enumValue;
            }
            else
            {
                return Enum.Parse(type, rowValue.ToString());
            }
        }
        else if (type == typeof(bool))
        {
            if (bool.TryParse(rowValue.ToString(), out var boolValue)) return boolValue;
            else
            {
                if (int.TryParse(rowValue.ToString(), out var intValue)) return intValue != 0;
                else throw new InvalidCastException($"unable to cast value '{rowValue}' to boolean");
            }
        }
        return Convert.ChangeType(rowValue, type);
    }

    static readonly Func<string, string> DefaultNameConverter = key => key;

    static readonly Func<object, DataRow, object> DefaultValueConverter = (value, row) => value;
}

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
    /// 是否必需,如果是则在列名签名加*号
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