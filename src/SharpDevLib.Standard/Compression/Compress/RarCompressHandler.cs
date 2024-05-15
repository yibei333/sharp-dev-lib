namespace SharpDevLib.Standard.Compression.Compress;

internal class RarCompressHandler : CompressHandler
{
    public RarCompressHandler(CompressOption option) : base(option)
    {
    }

    public override Task HandleAsync()
    {

        throw new NotSupportedException("rar format not supported yet");
    }
}
