namespace SharpDevLib.Compression;

internal class TarDeCompressHandler : DeCompressHandler
{
    public TarDeCompressHandler(DeCompressOption option, CancellationToken? cancellationToken) : base(option, cancellationToken)
    {
    }
}
