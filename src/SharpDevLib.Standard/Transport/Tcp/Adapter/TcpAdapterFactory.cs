namespace SharpDevLib.Standard;

internal static class TcpAdapterFactory
{
    public static ITcpReceiveAdapter GetReceiveAdapter(this TcpAdapterType type, ITcpReceiveAdapter? customAdapter = null)
    {
        if (type == TcpAdapterType.Custom) return customAdapter ?? throw new NullReferenceException("set adapter first");

        if (type == TcpAdapterType.Default) return TcpReceiveAdapters.Default;
        else if (type == TcpAdapterType.FixHeader) return TcpReceiveAdapters.FixedHeader;
        else throw new NotSupportedException();
    }

    public static ITcpSendAdapter GetSendAdapter(this TcpAdapterType type, ITcpSendAdapter? customAdapter = null)
    {
        if (type == TcpAdapterType.Custom) return customAdapter ?? throw new NullReferenceException("set adapter first");

        if (type == TcpAdapterType.Default) return TcpSendAdapters.Default;
        else if (type == TcpAdapterType.FixHeader) return TcpSendAdapters.FixedHeader;
        else throw new NotSupportedException();
    }
}