using ICSharpCode.SharpZipLib.Tar;
using System.Text;

namespace SharpDevLib.Standard.Compression.Compress;

internal class TarCompressHandler : CompressHandler<TarOutputStream, TarEntry>
{
    public TarCompressHandler(CompressOption option) : base(option)
    {
    }

    public override TarOutputStream CreateStream(Stream targetStream)
    {
        return new TarOutputStream(targetStream, Encoding.ASCII);
    }

    public override async Task WriteNextEntryAsync(TarOutputStream outputStream, FilePathInfo pathInfo)
    {
        var entry = TarEntry.CreateEntryFromFile(pathInfo.Path);
        entry.Name = pathInfo.Name;
        outputStream.PutNextEntry(entry);
        using var sourceStream = new FileInfo(entry.File).OpenOrCreate();
        await sourceStream.CopyToAsync(outputStream, Option.CancellationToken, (_, _, transfered) =>
        {
            Option.CurrentName = pathInfo.Path;
            Option.Transfered += transfered;
        });
    }
}