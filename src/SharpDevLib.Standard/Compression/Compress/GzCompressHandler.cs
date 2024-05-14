namespace SharpDevLib.Standard.Compression.Compress;

internal class GzCompressHandler : CompressHandler
{
    public GzCompressHandler(CompressOption option) : base(option)
    {
    }

    public override Task HandleAsync()
    {
        throw new NotImplementedException();
    }
}
