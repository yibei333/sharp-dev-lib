using SharpCompress.Compressors.Xz;

namespace SharpDevLib.Standard.Compression.DeCompress;

internal class XzDeCompressHandler : DeCompressHandler
{
    public XzDeCompressHandler(DeCompressOption option, CancellationToken? cancellationToken) : base(option, cancellationToken)
    {
    }

    public override async Task HandleAsync()
    {
        try//tar ball
        {
            await base.HandleAsync();
        }
        catch//single file
        {
            var fileName = Option.SourceFile.GetFileName(false);
            Option.CurrentName = fileName;
            Option.Total = GetUncompressedSize(Option.SourceFile);

            using var sourceStream = new FileInfo(Option.SourceFile).OpenOrCreate();
            using var inputStream = new XZStream(sourceStream);
            using var outputStream = new FileInfo(Option.TargetPath.CombinePath(fileName)).OpenOrCreate();
            await inputStream.CopyToAsync(outputStream, CancellationToken ?? System.Threading.CancellationToken.None, transfered => Option.Transfered += transfered);
        }
    }

    private const int XzHeaderSize = 12;
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
