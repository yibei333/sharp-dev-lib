namespace SharpDevLib;

/// <summary>
/// 响应基类,为所有响应对象提供基础类型支持
/// 包含操作是否成功、描述信息和额外数据等通用字段
/// 适用于 API 响应或方法返回值等场景
/// </summary>
public abstract class BaseReply
{
    /// <summary>
    /// 操作是否成功,用于判断业务逻辑是否正确执行
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// 描述信息,用于说明操作结果或错误原因
    /// </summary>
    public string? Message { get; set; }

    /// <summary>
    /// 额外数据,用于传递非结构化的额外信息
    /// </summary>
    public object? ExtraData { get; set; }
}