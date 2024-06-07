namespace SharpDevLib.Transport;

/// <summary>
/// 传输收发数据适配器类型
/// </summary>
public enum TransportAdapterType
{
    /// <summary>
    /// 默认
    /// </summary>
    Default,
    /// <summary>
    /// 固定前四个字节为数据长度
    /// </summary>
    FixHeader,
    /// <summary>
    /// 自定义
    /// </summary>
    Custom
}