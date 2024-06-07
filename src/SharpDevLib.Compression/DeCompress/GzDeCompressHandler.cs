namespace SharpDevLib.Compression;

internal class GzDeCompressHandler : DeCompressHandler
{
    public GzDeCompressHandler(DeCompressOption option, CancellationToken? cancellationToken) : base(option, cancellationToken)
    {
    }
}
