using System.Data;
using System.Text;

namespace SharpDevLib;

/// <summary>
/// DataTable扩展
/// </summary>
public static class DataTableHelper
{
    /// <summary>
    /// 将DataTable序列化为字符串
    /// </summary>
    /// <param name="table">table</param>
    /// <param name="predicate">筛选条件</param>
    /// <param name="takeCount">获取行数,默认最多10行</param>
    /// <returns>字符串</returns>
    public static string Serialize(this DataTable table, Func<DataRow, bool>? predicate = null, uint takeCount = 10)
    {
        var builder = new StringBuilder();
        builder.AppendLine(string.Join("|", table.Columns.Cast<DataColumn>().Select(x => x.ColumnName)));
        var rows = table.Rows.Cast<DataRow>();
        if (predicate is not null) rows = rows.Where(predicate);
        rows = rows.Take((int)takeCount);
        foreach (DataRow row in rows)
        {
            builder.AppendLine(string.Join("|", row.ItemArray.Select(x => x.ToString())));
        }
        return builder.ToString();
    }

    #region List->Table
    /// <summary>
    /// 将集合转换为DataTable，以下元素属性将被忽略:
    /// <para>1.属性不为public</para>
    /// <para>2.属性不可读</para>
    /// <para>3.属性类型为Class(string除外)</para>
    /// <para>4.被new关键字覆盖的基类属性</para>
    /// </summary>
    /// <typeparam name="T">集合元素类型，必须为引用类型</typeparam>
    /// <param name="source">要转换的集合</param>
    /// <returns>转换后的DataTable对象</returns>
    /// <exception cref="InvalidDataException">当找不到属性时抛出</exception>
    /// <exception cref="NotSupportedException">当属性类型不支持时抛出</exception>
    public static DataTable ToDataTable<T>(this IEnumerable<T> source) where T : class
    {
        var index = 0;
        var mappings = typeof(T)
            .GetProperties()
            .Where(x => x.CanRead && (!x.PropertyType.IsClass || x.PropertyType.IsClass && x.PropertyType == typeof(string)))
            .Select(x => new { Property = x, Index = index++ })
            .GroupBy(x => x.Property.Name)//handle property with new keyword
            .Select(x => new ListToTableMapping(x.OrderBy(x => x.Index).First().Property.Name))
            .ToArray();
        return source.ToDataTable(mappings);
    }

    /// <summary>
    /// 将集合转换为DataTable
    /// </summary>
    /// <typeparam name="T">集合元素类型，必须为引用类型</typeparam>
    /// <param name="source">要转换的集合</param>
    /// <param name="names">要导出到DataTable的属性名称</param>
    /// <returns>转换后的DataTable对象</returns>
    /// <exception cref="InvalidDataException">当找不到属性时抛出</exception>
    /// <exception cref="NotSupportedException">当属性类型不支持时抛出</exception>
    public static DataTable ToDataTable<T>(this IEnumerable<T> source, string[] names) where T : class
    {
        return source.ToDataTable(names.Select(x => new ListToTableMapping(x)).ToArray());
    }

    /// <summary>
    /// 将集合转换为DataTable
    /// </summary>
    /// <typeparam name="T">集合元素类型，必须为引用类型</typeparam>
    /// <param name="source">要转换的集合</param>
    /// <param name="mappings">映射</param>
    /// <returns>转换后的DataTable对象</returns>
    /// <exception cref="InvalidDataException">当找不到属性时抛出</exception>
    /// <exception cref="NotSupportedException">当属性类型不支持时抛出</exception>
    public static DataTable ToDataTable<T>(this IEnumerable<T> source, ListToTableMapping[] mappings) where T : class
    {
        var table = new DataTable();
        if (mappings.IsNullOrEmpty()) return table;
        var type = typeof(T);
        var properties = type.GetProperties()
            .Where(p => p.CanRead && (!p.PropertyType.IsClass || p.PropertyType == typeof(string)))
            .Select((p, index) => (Property: p, Index: index))
            .GroupBy(t => t.Property.Name)
            .Select(g => g.Aggregate((min, curr) => curr.Index < min.Index ? curr : min).Property)
            .ToList();
        mappings.ForEach(x =>
        {
            x.Property = properties.FirstOrDefault(p => p.Name == x.Name) ?? throw new InvalidDataException($"在类型'{type.FullName}'找不到名称为'{x.Name}'的属性");
            x.ColumnName = (x.NameConverter ?? ListToTableMapping.InternalDefaultNameConventer).Invoke(x.Name);
            table.Columns.Add(new DataColumn(x.ColumnName));
        });

        //values
        foreach (var item in source)
        {
            var row = table.NewRow();
            mappings.ForEach(x =>
            {
                var value = (x.ValueConverter ?? ListToTableMapping.InternalValueConventer).Invoke(x.Property.GetValue(item), item);
                value ??= DBNull.Value;
                row[x.ColumnName] = value;
            });
            table.Rows.Add(row);
        }
        return table;
    }
    #endregion

    #region Table->List
    /// <summary>
    /// 将DataTable转换为指定类型的列表
    /// </summary>
    /// <typeparam name="T">列表元素类型，必须为引用类型</typeparam>
    /// <param name="table">要转换的DataTable对象</param>
    /// <returns>转换后的对象列表</returns>
    /// <exception cref="InvalidDataException">当没有任何映射时抛出</exception>
    /// <exception cref="MissingMethodException">当类型没有无参构造函数或参数不匹配时抛出</exception>
    public static List<T> ToList<T>(this DataTable table) where T : class
    {
        List<TableToListMapping> mappings = [];
        foreach (DataColumn column in table.Columns)
        {
            mappings.Add(new TableToListMapping(column.ColumnName));
        }
        return table.ToList<T>([.. mappings]);
    }

    /// <summary>
    /// 将DataTable转换为指定类型的列表
    /// </summary>
    /// <typeparam name="T">列表元素类型，必须为引用类型</typeparam>
    /// <param name="table">要转换的DataTable对象</param>
    /// <param name="names">列名</param>
    /// <returns>转换后的对象列表</returns>
    /// <exception cref="InvalidDataException">当没有任何映射时抛出</exception>
    /// <exception cref="MissingMethodException">当类型没有无参构造函数或参数不匹配时抛出</exception>
    public static List<T> ToList<T>(this DataTable table, string[] names) where T : class
    {
        return table.ToList<T>([.. names.Select(x => new TableToListMapping(x))]);
    }

    /// <summary>
    /// 将DataTable转换为指定类型的列表
    /// </summary>
    /// <typeparam name="T">列表元素类型，必须为引用类型</typeparam>
    /// <param name="table">要转换的DataTable对象</param>
    /// <param name="mappings">映射</param>
    /// <returns>转换后的对象列表</returns>
    /// <exception cref="InvalidDataException">当没有任何映射时抛出</exception>
    /// <exception cref="MissingMethodException">当类型没有无参构造函数或参数不匹配时抛出</exception>
    public static List<T> ToList<T>(this DataTable table, TableToListMapping[] mappings) where T : class
    {
        if (mappings.IsNullOrEmpty()) throw new InvalidDataException("至少应该有一个映射");
        var result = new List<T>();
        var type = typeof(T);
        var properties = type.GetProperties()
           .Where(p => p.CanRead && (!p.PropertyType.IsClass || p.PropertyType == typeof(string)))
           .Select((p, index) => (Property: p, Index: index))
           .GroupBy(t => t.Property.Name)
           .Select(g => g.Aggregate((min, curr) => curr.Index < min.Index ? curr : min).Property)
           .ToList();
        mappings.ForEach(x =>
        {
            var propertyName = (x.NameConverter ?? TableToListMapping.InternalDefaultNameConventer).Invoke(x.Name);
            x.Property = properties.FirstOrDefault(p => p.Name == propertyName) ?? throw new InvalidDataException($"在类型'{type.FullName}'找不到名称为'{x.Name}'的属性");
        });

        var hasDefaultConstructor = typeof(T).GetConstructors().Any(x => x.GetParameters().Count() == 0);
        foreach (DataRow row in table.Rows)
        {
            if (hasDefaultConstructor)
            {
                var instance = Activator.CreateInstance<T>();
                foreach (var mapping in mappings)
                {
                    var value = (mapping.ValueConverter ?? TableToListMapping.InternalDefaultValueConverter).Invoke(row[mapping.Name], row);
                    value = ConvertRowValueToPropertyValue(mapping.Property.PropertyType, value);
                    mapping.Property.SetValue(instance, value);
                }
                result.Add(instance);
            }
            else
            {
                var args = new List<object?>();
                foreach (var mapping in mappings)
                {
                    var value = (mapping.ValueConverter ?? TableToListMapping.InternalDefaultValueConverter).Invoke(row[mapping.Name], row);
                    args.Add(value);
                }
                var instance = (T)Activator.CreateInstance(typeof(T), [.. args]);
                result.Add(instance);
            }
        }
        return result;
    }
    #endregion

    static Type GetNonGenericPropertyTypeToColumnType(Type propertyType)
    {
        if (propertyType.IsGenericType)
        {
            if (propertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                return GetNonGenericPropertyTypeToColumnType(propertyType.GetGenericArguments()[0]);
            }
            else throw new NotSupportedException($"不支持的属性类型: '{propertyType.FullName}'");
        }
        return propertyType;
    }

    static object? ConvertRowValueToPropertyValue(Type propertyType, object? rowValue)
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
                else throw new InvalidCastException($"无法将值'{rowValue}'转换为布尔类型");
            }
        }
        return Convert.ChangeType(rowValue, type);
    }
}