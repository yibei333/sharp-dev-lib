namespace SharpDevLib.Transport;

internal static class TransportAdapterFactory
{
    public static ITransportReceiveAdapter GetReceiveAdapter(this TransportAdapterType type, ITransportReceiveAdapter? customAdapter = null)
    {
        if (type == TransportAdapterType.Custom) return customAdapter ?? throw new NullReferenceException("set adapter first");

        if (type == TransportAdapterType.Default) return TransportReceiveAdapters.Default;
        else if (type == TransportAdapterType.FixHeader) return TransportReceiveAdapters.FixedHeader;
        else throw new NotSupportedException();
    }

    public static ITransportSendAdapter GetSendAdapter(this TransportAdapterType type, ITransportSendAdapter? customAdapter = null)
    {
        if (type == TransportAdapterType.Custom) return customAdapter ?? throw new NullReferenceException("set adapter first");

        if (type == TransportAdapterType.Default) return TransportSendAdapters.Default;
        else if (type == TransportAdapterType.FixHeader) return TransportSendAdapters.FixedHeader;
        else throw new NotSupportedException();
    }
}