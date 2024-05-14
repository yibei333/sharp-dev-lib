namespace SharpDevLib.Standard.Compression.Compress;

internal abstract class CompressHandler
{
    protected CompressHandler(CompressOption option)
    {
        Option = option;
    }

    public CompressOption Option { get; }

    public abstract Task HandleAsync();
}





