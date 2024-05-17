using ICSharpCode.SharpZipLib.BZip2;

namespace SharpDevLib.Standard.Compression.Compress;

internal class Bz2CompressHandler : CompressHandler
{
    public Bz2CompressHandler(CompressOption option) : base(option)
    {
    }

    public override async Task HandleAsync()
    {
        if (Option.SourcePaths.IsNullOrEmpty()) throw new InvalidOperationException("source path required");
        if (Option.Password.NotNullOrWhiteSpace()) throw new InvalidDataException("password not supported");
        Option.TargetPath.RemoveFileIfExist();

        var targetFileInfo = new FileInfo(Option.TargetPath);
        var tempFileInfo = new FileInfo($"{targetFileInfo.Name.TrimEnd(".bz2").TrimEnd(".tar.bz2")}.tar");
        var pathInfo = await CreateSourcePathInfo(tempFileInfo);
        Option.Total = pathInfo.Size;
        using var targetStream = new FileInfo(Option.TargetPath).OpenOrCreate();
        using var zipOutStream = new BZip2OutputStream(targetStream);

        await CopyStream(pathInfo, zipOutStream);

        if (tempFileInfo.Exists)
        {
            tempFileInfo.Delete();
        }
    }

    async Task<FilePathInfo> CreateSourcePathInfo(FileInfo tempFileInfo)
    {
        if (Option.SourcePaths.Count == 1)
        {
            var fileInfo = new FileInfo(Option.SourcePaths.First());
            if (fileInfo.Exists) return new FilePathInfo(fileInfo.FullName, fileInfo.Name, fileInfo.Name, fileInfo.Length);
        }

        await new CompressOption(Option.SourcePaths, tempFileInfo.FullName) { CancellationToken = Option.CancellationToken, IncludeSourceDiretory = Option.IncludeSourceDiretory }.CompressAsync();
        return new FilePathInfo(tempFileInfo.FullName, tempFileInfo.Name, tempFileInfo.Name, tempFileInfo.Length);
    }
}
