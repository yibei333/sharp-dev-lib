namespace SharpDevLib;

/// <summary>
/// 包含标识符和名称的复合数据传输对象
/// 继承自<see cref="IdDto{TId}"/>,在标识符基础上增加了名称字段,适用于需要同时传输 ID 和名称的场景
/// </summary>
/// <typeparam name="TId">标识符类型,如 int、long、Guid 等</typeparam>
public class IdNameDto<TId> : IdDto<TId>
{
    /// <summary>
    /// 实例化 IdNameDto 对象
    /// </summary>
    public IdNameDto()
    {

    }

    /// <summary>
    /// 实例化 IdNameDto 对象并初始化标识符
    /// </summary>
    /// <param name="id">标识符</param>
    public IdNameDto(TId id) : base(id)
    {
    }

    /// <summary>
    /// 实例化 IdNameDto 对象并初始化名称
    /// </summary>
    /// <param name="name">名称</param>
    public IdNameDto(string? name)
    {
        Name = name;
    }

    /// <summary>
    /// 实例化 IdNameDto 对象并初始化标识符和名称
    /// </summary>
    /// <param name="id">标识符</param>
    /// <param name="name">名称</param>
    public IdNameDto(TId id, string? name) : base(id)
    {
        Name = name;
    }

    /// <summary>
    /// 名称,用于描述或显示对象
    /// </summary>
    public string? Name { get; set; }
}