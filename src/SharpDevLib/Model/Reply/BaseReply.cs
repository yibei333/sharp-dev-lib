namespace SharpDevLib;

/// <summary>
/// 响应基类,或许可以在反射或者泛型中用
/// </summary>
public abstract class BaseReply
{
    /// <summary>
    /// 是否成功
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// 描述
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// 额外字段
    /// </summary>
    public object? ExtraData { get; set; }
}