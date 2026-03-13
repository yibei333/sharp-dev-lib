namespace SharpDevLib;

/// <summary>
/// 包含标识符、名称和数据的复合数据传输对象
/// 继承自<see cref="DataDto{TData}"/>,在数据基础上增加了标识符和名称字段,适用于需要同时传输 ID、名称和数据的场景
/// </summary>
/// <typeparam name="TId">标识符类型,如 int、long、Guid 等</typeparam>
/// <typeparam name="TData">数据类型,可以是任何可序列化的类型</typeparam>
public class IdNameDataDto<TId, TData> : DataDto<TData>
{
    /// <summary>
    /// 示例化 IdNameDataDto 对象
    /// </summary>
    public IdNameDataDto()
    {
    }

    /// <summary>
    /// 示例化 IdNameDataDto 对象并初始化数据
    /// </summary>
    /// <param name="data">要包装的数据对象</param>
    public IdNameDataDto(TData? data) : base(data)
    {
    }

    /// <summary>
    /// 示例化 IdNameDataDto 对象并初始化标识符
    /// </summary>
    /// <param name="id">标识符</param>
    public IdNameDataDto(TId id)
    {
        Id = id;
    }

    /// <summary>
    /// 示例化 IdNameDataDto 对象并初始化名称
    /// </summary>
    /// <param name="name">名称</param>
    public IdNameDataDto(string name)
    {
        Name = name;
    }

    /// <summary>
    /// 示例化 IdNameDataDto 对象并初始化标识符和数据
    /// </summary>
    /// <param name="id">标识符</param>
    /// <param name="data">要包装的数据对象</param>
    public IdNameDataDto(TId id, TData? data) : base(data)
    {
        Id = id;
    }

    /// <summary>
    /// 示例化 IdNameDataDto 对象并初始化标识符和名称
    /// </summary>
    /// <param name="id">标识符</param>
    /// <param name="name">名称</param>
    public IdNameDataDto(TId id, string name)
    {
        Id = id;
        Name = name;
    }

    /// <summary>
    /// 示例化 IdNameDataDto 对象并初始化名称和数据
    /// </summary>
    /// <param name="name">名称</param>
    /// <param name="data">要包装的数据对象</param>
    public IdNameDataDto(string name, TData? data) : base(data)
    {
        Name = name;
    }

    /// <summary>
    /// 示例化 IdNameDataDto 对象并初始化标识符、名称和数据
    /// </summary>
    /// <param name="id">标识符</param>
    /// <param name="name">名称</param>
    /// <param name="data">要包装的数据对象</param>
    public IdNameDataDto(TId id, string name, TData? data) : base(data)
    {
        Id = id;
        Name = name;
    }
    /// <summary>
    /// 标识符,用于唯一标识数据对象
    /// </summary>
    public TId Id { get; set; } = default!;
    /// <summary>
    /// 名称,用于描述或显示数据对象
    /// </summary>
    public string? Name { get; set; }
}