using ICSharpCode.SharpZipLib.Zip;

namespace SharpDevLib.Compression.Internal.Compress;

internal class ZipCompressHandler(CompressRequest request) : CompressHandler<ZipOutputStream, ZipEntry>(request)
{
    public override bool SupportPassword => true;

    public override ZipOutputStream CreateStream(Stream targetStream)
    {
        var outputStream = new ZipOutputStream(targetStream)
        {
            Password = Request.Password,
            UseZip64 = UseZip64.Dynamic,
        };
        outputStream.SetLevel(Request.Level.ConvertToSharpZipLibLevel());
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
