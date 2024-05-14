namespace SharpDevLib.Standard.Compression.DeCompress;

internal abstract class DeCompressHandler
{
    protected DeCompressHandler(CompressOption option)
    {
        Option = option;
    }

    public CompressOption Option { get; }

    public abstract Task HandleAsync();
}





