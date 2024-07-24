namespace SharpDevLib.Compression.Internal.DeCompress;

internal class TarDeCompressHandler : DeCompressHandler
{
    public TarDeCompressHandler(DeCompressOption option, CancellationToken? cancellationToken) : base(option, cancellationToken)
    {
    }
}
