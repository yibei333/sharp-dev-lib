using SharpCompress.Compressors.Xz;

namespace SharpDevLib.Compression.Internal.DeCompress;

internal class XzDeCompressHandler(DeCompressRequest request) : DeCompressHandler(request)
{
    public override async Task HandleAsync()
    {
        try//tar ball
        {
            await base.HandleAsync();
        }
        catch//single file
        {
            var fileName = Request.SourceFile.GetFileName(false);
            Request.CurrentName = fileName;
            Request.Total = GetUncompressedSize(Request.SourceFile);

            using var sourceStream = new FileInfo(Request.SourceFile).OpenOrCreate();
            using var inputStream = new XZStream(sourceStream);
            using var outputStream = new FileInfo(Request.TargetPath.CombinePath(fileName)).OpenOrCreate();
            await inputStream.CopyToAsync(outputStream, Request.CancellationToken ?? CancellationToken.None, transfered => Request.Transfered += transfered);
        }
    }

    const int XzHeaderSize = 12;
    static long GetUncompressedSize(string filePath)
    {
        using var file = File.Open(filePath, FileMode.Open);
        file.Seek(-XzHeaderSize, SeekOrigin.End);
        var footer = XZFooter.FromStream(file);
        file.Seek(-(XzHeaderSize + footer.BackwardSize), SeekOrigin.End);
        var index = XZIndex.FromStream(file, false);
        var size = (long)index.Records.Select(r => r.UncompressedSize).Aggregate((acc, x) => acc + x);
        return size;
    }
}
