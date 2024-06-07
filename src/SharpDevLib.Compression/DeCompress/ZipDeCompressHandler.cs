namespace SharpDevLib.Compression;

internal class ZipDeCompressHandler : DeCompressHandler
{
    public ZipDeCompressHandler(DeCompressOption option, CancellationToken? cancellationToken) : base(option, cancellationToken)
    {
    }
}
