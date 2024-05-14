namespace SharpDevLib.Standard;

/// <summary>
/// 压缩等级
/// </summary>
public enum CompressionLevel
{
    /// <summary>
    /// 正常
    /// </summary>
    Normal,
    /// <summary>
    /// 最快
    /// </summary>
    Fastest,
    /// <summary>
    /// 最小尺寸(最高压缩比)
    /// </summary>
    MinimumSize,
}