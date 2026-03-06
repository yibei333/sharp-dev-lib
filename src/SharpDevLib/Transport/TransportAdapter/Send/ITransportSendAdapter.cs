using System.Net;
using System.Net.Sockets;

namespace SharpDevLib;

/// <summary>
/// 传输发送适配器接口
/// </summary>
/// <remarks>用于自定义TCP/UDP数据的发送逻辑，处理粘包问题等场景</remarks>
public interface ITransportSendAdapter
{
    /// <summary>
    /// 发送数据到套接字（TCP使用）
    /// </summary>
    /// <param name="socket">套接字</param>
    /// <param name="bytes">要发送的字节数组</param>
    void Send(Socket socket, byte[] bytes);

    /// <summary>
    /// 发送数据到指定远程端点（UDP使用）
    /// </summary>
    /// <param name="socket">套接字</param>
    /// <param name="remoteAddress">远程IP地址</param>
    /// <param name="remoteAddressPort">远程端口</param>
    /// <param name="bytes">要发送的字节数组</param>
    void SendTo(Socket socket, IPAddress remoteAddress, int remoteAddressPort, byte[] bytes);
}