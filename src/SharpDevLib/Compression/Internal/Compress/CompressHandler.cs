namespace SharpDevLib.Compression.Internal.Compress;

internal abstract class CompressHandler(CompressRequest request)
{
    public CompressRequest Request { get; } = request;
    public virtual bool SupportPassword { get; } = false;

    public abstract Task HandleAsync();

    protected async Task CopyStream(FilePathInfo pathInfo, Stream outputStream)
    {
        using var sourceStream = new FileInfo(pathInfo.Path).OpenOrCreate();
        await sourceStream.CopyToAsync(outputStream, Request.CancellationToken ?? CancellationToken.None, transfered =>
        {
            Request.CurrentName = pathInfo.Path;
            Request.Transfered += transfered;
        });
    }

    protected List<FilePathInfo> GetPathList()
    {
        return [.. Request.SourcePaths.SelectMany(path =>
        {
            string rootPath = string.Empty;
            var directoryInfo = new DirectoryInfo(path);
            if (directoryInfo.Exists)
            {
                rootPath = Request.IncludeSourceDiretory ? directoryInfo.Parent?.FullName ?? string.Empty : directoryInfo.FullName;
            }
            else
            {
                var fileInfo = new FileInfo(path);
                rootPath = Request.IncludeSourceDiretory ? fileInfo.Directory.Parent.FullName ?? string.Empty : fileInfo.Directory.FullName;
            }
            return GetPathList(path, rootPath);
        })];
    }

    List<FilePathInfo> GetPathList(string path, string rootPath)
    {
        var directoryInfo = new DirectoryInfo(path);
        if (directoryInfo.Exists)
        {
            return [.. directoryInfo.GetDirectories().Select(y => y.FullName).Concat(directoryInfo.GetFiles().Select(y => y.FullName)).SelectMany(x => GetPathList(x, rootPath))];
        }
        else
        {
            var fileInfo = new FileInfo(path);
            if (!fileInfo.Exists) throw new FileNotFoundException("file not found", path);
            var pathInfo = new FilePathInfo(path, FormatPath(path).TrimStart(FormatPath(rootPath)).TrimStart("/").TrimEnd("/"), fileInfo.Name, fileInfo.Length);
            return [pathInfo];
        }
    }

    string FormatPath(string path)
    {
        return path.Replace("\\", "/").Replace("//", "/").TrimEnd("/");
    }
}

internal abstract class CompressHandler<TOutputStream, TEntry>(CompressRequest request) : CompressHandler(request) where TOutputStream : Stream where TEntry : class
{
    public abstract TOutputStream CreateStream(Stream targetStream);

    public abstract Task WriteNextEntryAsync(TOutputStream outputStream, FilePathInfo pathInfo);

    public override async Task HandleAsync()
    {
        if (Request.SourcePaths.IsNullOrEmpty()) throw new InvalidOperationException("source path required");
        if (!SupportPassword && Request.Password.NotNullOrWhiteSpace()) throw new InvalidDataException("password not supported");
        Request.TargetPath.RemoveFileIfExist();

        var pathList = GetPathList();
        using var targetStream = new FileInfo(Request.TargetPath).OpenOrCreate();
        using var outputStream = CreateStream(targetStream);
        Request.Total = pathList.Sum(x => x.Size);

        foreach (var path in pathList)
        {
            if (Request.CancellationToken?.IsCancellationRequested ?? false) break;
            await WriteNextEntryAsync(outputStream, path);
        }

        if (Request.CancellationToken?.IsCancellationRequested ?? false) throw new OperationCanceledException(Request.CancellationToken.Value);
        await outputStream.FlushAsync(Request.CancellationToken ?? System.Threading.CancellationToken.None);
    }
}

internal class FilePathInfo(string path, string name, string selfName, long size)
{
    public string Path { get; } = path;
    public string Name { get; } = name;
    public string SelfName { get; } = selfName;
    public long Size { get; } = size;
}





