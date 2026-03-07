namespace SharpDevLib;

/// <summary>
/// 包含名称的数据传输对象
/// 用于需要通过名称进行操作或查询的场景,如按名称搜索、获取名称列表等
/// </summary>
public class NameDto : BaseDto
{
    /// <summary>
    /// 实例化 NameDto 对象
    /// </summary>
    public NameDto()
    {

    }

    /// <summary>
    /// 实例化 NameDto 对象并初始化名称
    /// </summary>
    /// <param name="name">名称</param>
    public NameDto(string? name)
    {
        Name = name;
    }

    /// <summary>
    /// 名称,用于描述或标识对象
    /// </summary>
    public string? Name { get; set; }
}