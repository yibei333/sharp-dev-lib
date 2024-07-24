namespace SharpDevLib.Compression.Internal.DeCompress;

internal class ZipDeCompressHandler : DeCompressHandler
{
    public ZipDeCompressHandler(DeCompressOption option, CancellationToken? cancellationToken) : base(option, cancellationToken)
    {
    }
}
