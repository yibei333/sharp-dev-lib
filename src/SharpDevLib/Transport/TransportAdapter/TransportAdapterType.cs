namespace SharpDevLib;

/// <summary>
/// 传输收发数据适配器类型
/// </summary>
public enum TransportAdapterType
{
    /// <summary>
    /// 默认适配器（按最大64KB接收，存在粘包问题）
    /// </summary>
    Default,
    /// <summary>
    /// 固定头适配器（前四个字节为数据长度，无粘包问题）
    /// </summary>
    FixHeader,
    /// <summary>
    /// 自定义适配器（需自行实现ITransportReceiveAdapter和ITransportSendAdapter）
    /// </summary>
    Custom
}