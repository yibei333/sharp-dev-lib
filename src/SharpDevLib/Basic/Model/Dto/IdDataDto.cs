namespace SharpDevLib;

/// <summary>
/// 包含标识符和数据的复合数据传输对象
/// 继承自<see cref="DataDto{TData}"/>,在数据基础上增加了标识符字段,适用于需要同时传输 ID 和数据的场景
/// </summary>
/// <typeparam name="TId">标识符类型,如 int、long、Guid 等</typeparam>
/// <typeparam name="TData">数据类型,可以是任何可序列化的类型</typeparam>
public class IdDataDto<TId, TData> : DataDto<TData>
{
    /// <summary>
    /// 示例化 IdDataDto 对象
    /// </summary>
    public IdDataDto()
    {
    }

    /// <summary>
    /// 示例化 IdDataDto 对象并初始化数据
    /// </summary>
    /// <param name="data">要包装的数据对象</param>
    public IdDataDto(TData? data) : base(data)
    {
    }

    /// <summary>
    /// 示例化 IdDataDto 对象并初始化标识符
    /// </summary>
    /// <param name="id">标识符</param>
    public IdDataDto(TId id)
    {
        Id = id;
    }

    /// <summary>
    /// 示例化 IdDataDto 对象并初始化标识符和数据
    /// </summary>
    /// <param name="id">标识符</param>
    /// <param name="data">要包装的数据对象</param>
    public IdDataDto(TId id, TData? data) : base(data)
    {
        Id = id;
    }

    /// <summary>
    /// 标识符,用于唯一标识数据对象
    /// </summary>
    public TId Id { get; set; } = default!;
}