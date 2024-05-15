using SharpCompress.Archives.Tar;

namespace SharpDevLib.Standard.Compression.DeCompress;

internal class TarDeCompressHandler : DeCompressHandler
{
    public TarDeCompressHandler(DeCompressOption option) : base(option)
    {
    }

    protected override double GetUnCompressedSize(FileInfo fileInfo)
    {
        var archive = TarArchive.Open(fileInfo);
        var size = archive.TotalUncompressSize;
        archive.Dispose();
        return size;
    }
}
