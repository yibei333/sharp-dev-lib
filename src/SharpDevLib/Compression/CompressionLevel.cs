namespace SharpDevLib;

/// <summary>
/// 压缩级别枚举，控制压缩速度和压缩率的权衡
/// </summary>
public enum CompressionLevel
{
    /// <summary>
    /// 正常压缩级别，平衡压缩速度和压缩率
    /// </summary>
    Normal,
    /// <summary>
    /// 最快压缩级别，压缩速度最快但压缩率较低
    /// </summary>
    Fastest,
    /// <summary>
    /// 最小尺寸压缩级别，压缩率最高但压缩速度最慢
    /// </summary>
    MinimumSize,
}