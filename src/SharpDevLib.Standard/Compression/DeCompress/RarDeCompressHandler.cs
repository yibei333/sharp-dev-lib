
using SharpCompress.Archives.Rar;

namespace SharpDevLib.Standard.Compression.DeCompress;

internal class RarDeCompressHandler : DeCompressHandler
{
    public RarDeCompressHandler(DeCompressOption option) : base(option)
    {
    }

    protected override double GetUnCompressedSize(FileInfo fileInfo)
    {
        var archive = RarArchive.Open(fileInfo);
        var size = archive.TotalUncompressSize;
        archive.Dispose();
        return size;
    }
}
