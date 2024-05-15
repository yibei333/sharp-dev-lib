using SharpCompress.Archives.Zip;

namespace SharpDevLib.Standard.Compression.DeCompress;

internal class ZipDeCompressHandler : DeCompressHandler
{
    public ZipDeCompressHandler(DeCompressOption option) : base(option)
    {
    }

    protected override double GetUnCompressedSize(FileInfo fileInfo)
    {
        var archive = ZipArchive.Open(fileInfo);
        var size = archive.TotalUncompressSize;
        archive.Dispose();
        return size;
    }
}
