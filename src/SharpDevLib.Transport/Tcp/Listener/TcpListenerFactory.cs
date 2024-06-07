using System.Net;

namespace SharpDevLib.Transport;

/// <summary>
/// Tcp监听器创建工厂
/// </summary>
public class TcpListenerFactory : ITcpListenerFactory
{
    /// <summary>
    /// 实例化Tcp监听器创建工厂
    /// </summary>
    /// <param name="serviceProvider">serviceProvider(用于获取Logger)</param>
    public TcpListenerFactory(IServiceProvider? serviceProvider = null)
    {
        ServiceProvider = serviceProvider;
    }

    IServiceProvider? ServiceProvider { get; }

    /// <summary>
    /// 创建Tcp监听器
    /// </summary>
    /// <typeparam name="TSessionMetadata">会话元数据类型(可以用来绑定会话的身份信息)</typeparam>
    /// <param name="address">地址</param>
    /// <param name="port">端口</param>
    /// <param name="initSessionMetadata">初始化会话元数据</param>
    /// <param name="adapterType">接收数据适配器类型</param>
    /// <returns>Tcp监听器</returns>
    public TcpListener<TSessionMetadata> Create<TSessionMetadata>(IPAddress address, int port, Func<TSessionMetadata> initSessionMetadata, TransportAdapterType adapterType = TransportAdapterType.Default) => new(address, port, initSessionMetadata, ServiceProvider, adapterType);

    /// <summary>
    /// 创建Tcp监听器
    /// </summary>
    /// <param name="address">地址</param>
    /// <param name="port">端口</param>
    /// <param name="adapterType">接收数据适配器类型</param>
    /// <returns>Tcp监听器</returns>
    public TcpListener Create(IPAddress address, int port, TransportAdapterType adapterType = TransportAdapterType.Default) => new(address, port, ServiceProvider, adapterType);
}
