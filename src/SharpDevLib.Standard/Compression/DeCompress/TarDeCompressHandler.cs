namespace SharpDevLib.Standard.Compression.DeCompress;

internal class TarDeCompressHandler : DeCompressHandler
{
    public TarDeCompressHandler(CompressOption option) : base(option)
    {
    }

    public override Task HandleAsync()
    {
        throw new NotImplementedException();
    }
}
