namespace SharpDevLib;

/// <summary>
/// 包含标识符的数据传输对象
/// 用于需要通过标识符进行操作或查询的场景,如删除、更新、获取等操作
/// </summary>
/// <typeparam name="TId">标识符类型,如 int、long、Guid 等</typeparam>
public class IdDto<TId> : BaseDto
{
    /// <summary>
    /// 实例化 IdDto 对象
    /// </summary>
    public IdDto()
    {

    }

    /// <summary>
    /// 实例化 IdDto 对象并初始化标识符
    /// </summary>
    /// <param name="id">标识符</param>
    public IdDto(TId id)
    {
        Id = id;
    }

    /// <summary>
    /// 标识符,用于唯一标识对象
    /// </summary>
    public TId Id { get; set; } = default!;
}