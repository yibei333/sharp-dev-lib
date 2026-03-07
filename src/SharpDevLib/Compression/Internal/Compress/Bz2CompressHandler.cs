using ICSharpCode.SharpZipLib.BZip2;

namespace SharpDevLib.Compression.Internal.Compress;

internal class Bz2CompressHandler(CompressRequest request) : CompressHandler(request)
{
    public override async Task HandleAsync()
    {
        if (Request.SourcePaths.IsNullOrEmpty()) throw new InvalidOperationException("源路径不能为空");
        if (Request.Password.NotNullOrWhiteSpace()) throw new InvalidDataException("不支持密码");
        Request.TargetPath.RemoveFileIfExist();

        var targetFileInfo = new FileInfo(Request.TargetPath);
        var tempFileInfo = new FileInfo($"{targetFileInfo.Name.TrimEnd(".bz2").TrimEnd(".tar.bz2")}.tar");
        var pathInfo = await CreateSourcePathInfo(tempFileInfo);
        Request.Total = pathInfo.Size;
        using var targetStream = new FileInfo(Request.TargetPath).OpenOrCreate();
        using var zipOutStream = new BZip2OutputStream(targetStream);

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
