namespace SharpDevLib;

/// <summary>
/// 包含名称的请求对象
/// 用于需要通过名称进行操作或查询的请求场景,如按名称搜索、获取名称列表等
/// </summary>
public class NameRequest : BaseRequest
{
    /// <summary>
    /// 示例化 NameRequest 对象
    /// </summary>
    public NameRequest()
    {
    }

    /// <summary>
    /// 示例化 NameRequest 对象并初始化名称
    /// </summary>
    /// <param name="name">名称</param>
    public NameRequest(string? name)
    {
        Name = name;
    }

    /// <summary>
    /// 名称,用于描述或标识对象
    /// </summary>
    public string? Name { get; set; }
}