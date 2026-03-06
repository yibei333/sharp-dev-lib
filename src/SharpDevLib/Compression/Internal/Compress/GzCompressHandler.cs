using ICSharpCode.SharpZipLib.GZip;

namespace SharpDevLib.Compression.Internal.Compress;

internal class GzCompressHandler(CompressRequest request) : CompressHandler(request)
{
    public override async Task HandleAsync()
    {
        if (Request.SourcePaths.IsNullOrEmpty()) throw new InvalidOperationException("source path required");
        if (Request.Password.NotNullOrWhiteSpace()) throw new InvalidDataException("password not supported");
        Request.TargetPath.RemoveFileIfExist();

        var targetFileInfo = new FileInfo(Request.TargetPath);
        var tempFileInfo = new FileInfo($"{targetFileInfo.Name.TrimEnd(".tgz").TrimEnd(".tar.gz")}.tar");
        var pathInfo = await CreateSourcePathInfo(tempFileInfo);
        Request.Total = pathInfo.Size;
        using var targetStream = new FileInfo(Request.TargetPath).OpenOrCreate();
        using var zipOutStream = new GZipOutputStream(targetStream) { FileName = pathInfo.Name };
        zipOutStream.SetLevel(Request.Level.ConvertToSharpZipLibLevel());

        await CopyStream(pathInfo, zipOutStream);

        if (tempFileInfo.Exists)
        {
            tempFileInfo.Delete();
        }
    }

    async Task<FilePathInfo> CreateSourcePathInfo(FileInfo tempFileInfo)
    {
        if (Request.SourcePaths.Count == 1)
        {
            var fileInfo = new FileInfo(Request.SourcePaths.First());
            if (fileInfo.Exists) return new FilePathInfo(fileInfo.FullName, fileInfo.Name, fileInfo.Name, fileInfo.Length);
        }

        await new CompressRequest(Request.SourcePaths, tempFileInfo.FullName) { IncludeSourceDiretory = Request.IncludeSourceDiretory }.CompressAsync();
        return new FilePathInfo(tempFileInfo.FullName, tempFileInfo.Name, tempFileInfo.Name, tempFileInfo.Length);
    }
}
