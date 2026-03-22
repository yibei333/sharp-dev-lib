namespace SharpDevLib;

/// <summary>
/// 包含名称和数据的复合数据传输对象
/// 继承自<see cref="DataDto{TData}"/>,在数据基础上增加了名称字段,适用于需要同时传输名称和数据的场景
/// </summary>
/// <typeparam name="TData">数据类型,可以是任何可序列化的类型</typeparam>
public class NameDataDto<TData> : DataDto<TData>
{
    /// <summary>
    /// 示例化 NameDataDto 对象
    /// </summary>
    public NameDataDto()
    {
    }

    /// <summary>
    /// 示例化 NameDataDto 对象并初始化名称和数据
    /// </summary>
    /// <param name="name">名称</param>
    /// <param name="data">要包装的数据对象</param>
    public NameDataDto(string name, TData? data) : base(data)
    {
        Name = name;
    }

    /// <summary>
    /// 名称,用于描述或显示数据对象
    /// </summary>
    public string? Name { get; set; }
}