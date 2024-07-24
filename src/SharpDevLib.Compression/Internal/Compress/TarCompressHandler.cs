using ICSharpCode.SharpZipLib.Tar;
using SharpDevLib.Compression.Internal.References;
using System.Text;

namespace SharpDevLib.Compression.Internal.Compress;

internal class TarCompressHandler : CompressHandler<TarOutputStream, TarEntry>
{
    public TarCompressHandler(CompressOption option, CancellationToken? cancellationToken) : base(option, cancellationToken)
    {
    }

    public override TarOutputStream CreateStream(Stream targetStream)
    {
        return new TarOutputStream(targetStream, Encoding.ASCII) { IsStreamOwner = false };
    }

    public override async Task WriteNextEntryAsync(TarOutputStream outputStream, FilePathInfo pathInfo)
    {
        var entry = TarEntry.CreateTarEntry(pathInfo.SelfName);
        entry.Name = pathInfo.Name.FormatPath();
        entry.Size = pathInfo.Size;

        outputStream.PutNextEntry(entry);
        await CopyStream(pathInfo, outputStream);
        outputStream.CloseEntry();
    }
}