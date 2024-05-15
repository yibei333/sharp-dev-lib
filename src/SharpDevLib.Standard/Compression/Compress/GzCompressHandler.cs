using ICSharpCode.SharpZipLib.GZip;

namespace SharpDevLib.Standard.Compression.Compress;

internal class GzCompressHandler : CompressHandler
{
    public GzCompressHandler(CompressOption option) : base(option)
    {
    }

    public override async Task HandleAsync()
    {
        if (Option.SourcePaths.IsNullOrEmpty()) throw new InvalidOperationException("source path required");
        if (Option.Password.NotNullOrWhiteSpace()) throw new InvalidDataException("password not supported");
        Option.TargetPath.RemoveFileIfExist();

        var tempFileInfo = new FileInfo($"{Option.TargetPath}.temp.tar");
        using var sourceStream = await CreateSourceStream(tempFileInfo);
        using var outStream = new FileInfo(Option.TargetPath).OpenOrCreate();
        using var zipStream = new GZipOutputStream(outStream);
        zipStream.SetLevel(Option.Level.ConvertToSharpZipLibLevel());

        var buffer = new byte[Statics.BufferSize];
        var length = 0;
        sourceStream.Seek(0, SeekOrigin.Begin);
        var progress = Option.OnProgress is null ? null : new CompressionProgressArgs { CurrentName = tempFileInfo.Exists ? tempFileInfo.FullName : Option.SourcePaths.First(), Total = sourceStream.Length };
        while ((length = sourceStream.Read(buffer, 0, buffer.Length)) > 0)
        {
            zipStream.Write(buffer, 0, length);
            if (Option.OnProgress is not null)
            {
                progress!.Handled += length;
                Option.OnProgress.Invoke(progress);
            }
        }
        await outStream.FlushAsync(Option.CancellationToken);

        if (tempFileInfo.Exists)
        {
            sourceStream.Dispose();
            tempFileInfo.Delete();
        }
    }

    async Task<Stream> CreateSourceStream(FileInfo tempFileInfo)
    {
        if (Option.SourcePaths.Count == 1)
        {
            var fileInfo = new FileInfo(Option.SourcePaths.First());
            if (fileInfo.Exists) return fileInfo.OpenOrCreate();
        }

        await new CompressOption(Option.SourcePaths, tempFileInfo.FullName) { CancellationToken = Option.CancellationToken, IncludeSourceDiretory = Option.IncludeSourceDiretory }.CompressAsync();
        return tempFileInfo.OpenOrCreate();
    }
}
