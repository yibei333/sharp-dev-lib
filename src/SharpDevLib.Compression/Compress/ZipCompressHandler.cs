using ICSharpCode.SharpZipLib.Zip;

namespace SharpDevLib.Compression;

internal class ZipCompressHandler : CompressHandler<ZipOutputStream, ZipEntry>
{
    public ZipCompressHandler(CompressOption option, CancellationToken? cancellationToken) : base(option, cancellationToken)
    {
    }

    public override bool SupportPassword => true;

    public override ZipOutputStream CreateStream(Stream targetStream)
    {
        var outputStream = new ZipOutputStream(targetStream)
        {
            Password = Option.Password,
            UseZip64 = UseZip64.Dynamic,
        };
        outputStream.SetLevel(Option.Level.ConvertToSharpZipLibLevel());
        return outputStream;
    }

    public override async Task WriteNextEntryAsync(ZipOutputStream outputStream, FilePathInfo pathInfo)
    {
        var entry = new ZipEntry(pathInfo.Name);
        outputStream.PutNextEntry(entry);
        await CopyStream(pathInfo, outputStream);
        outputStream.CloseEntry();
    }
}
