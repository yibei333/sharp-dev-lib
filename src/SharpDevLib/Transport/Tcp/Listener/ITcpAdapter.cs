using System.Net.Sockets;

namespace SharpDevLib;

/// <summary>
/// TCP接收适配器接口
/// </summary>
/// <remarks>用于自定义TCP数据的接收逻辑，处理粘包问题等场景</remarks>
public interface ITcpAdapter
{
    /// <summary>
    /// 发送数据到套接字
    /// </summary>
    /// <param name="socket">套接字</param>
    /// <param name="bytes">要发送的字节数组</param>
    void Send(Socket socket, byte[] bytes);

    /// <summary>
    /// 套接字开始接收数据
    /// </summary>
    /// <param name="socket">套接字</param>
    /// <param name="bufferSize">缓存区大小</param>
    /// <param name="asyncCallback">回调</param>
    void BeginReceive(Socket socket, int bufferSize, AsyncCallback asyncCallback);

    /// <summary>
    /// 从套接字接收数据
    /// </summary>
    /// <param name="socket">套接字</param>
    /// <param name="result">异步结果</param>
    /// <returns>接收到的字节数组</returns>
    byte[] EndReceive(Socket socket, IAsyncResult result);
}