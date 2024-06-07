namespace SharpDevLib.Compression;

internal class RarDeCompressHandler : DeCompressHandler
{
    public RarDeCompressHandler(DeCompressOption option, CancellationToken? cancellationToken) : base(option, cancellationToken)
    {
    }
}
