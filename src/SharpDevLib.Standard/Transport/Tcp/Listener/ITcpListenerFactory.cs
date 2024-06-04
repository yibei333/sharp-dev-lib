﻿using System.Net;

namespace SharpDevLib.Standard;

/// <summary>
/// Tcp监听器创建工厂
/// </summary>
public interface ITcpListenerFactory
{
    /// <summary>
    /// 创建Tcp监听器
    /// </summary>
    /// <typeparam name="TSessionMetadata">会话元数据类型(可以用来绑定会话的身份信息)</typeparam>
    /// <param name="address">地址</param>
    /// <param name="port">端口</param>
    /// <param name="adapterType">接收数据适配器类型</param>
    /// <returns>Tcp监听器</returns>
    TcpListener<TSessionMetadata> Create<TSessionMetadata>(IPAddress address, int port, TransportAdapterType adapterType = TransportAdapterType.Default);
}
