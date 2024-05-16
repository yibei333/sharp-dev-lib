﻿namespace SharpDevLib.Standard.Compression.Compress;

internal abstract class CompressHandler
{
    protected CompressHandler(CompressOption option)
    {
        Option = option;
    }

    public CompressOption Option { get; }

    public virtual bool SupportPassword { get; } = false;

    public abstract Task HandleAsync();

    protected async Task CopyStream(FilePathInfo pathInfo, Stream outputStream)
    {
        using var sourceStream = new FileInfo(pathInfo.Path).OpenOrCreate();
        await sourceStream.CopyToAsync(outputStream, Option.CancellationToken, (_, _, transfered) =>
        {
            Option.CurrentName = pathInfo.Path;
            Option.Transfered += transfered;
        });
    }

    protected List<FilePathInfo> GetPathList()
    {
        return Option.SourcePaths.SelectMany(path =>
        {
            string rootPath = string.Empty;
            var directoryInfo = new DirectoryInfo(path);
            if (directoryInfo.Exists)
            {
                rootPath = Option.IncludeSourceDiretory ? directoryInfo.Parent?.FullName ?? string.Empty : directoryInfo.FullName;
            }
            else
            {
                var fileInfo = new FileInfo(path);
                rootPath = fileInfo.Directory.FullName;
            }
            return GetPathList(path, rootPath);
        }).ToList();
    }

    List<FilePathInfo> GetPathList(string path, string rootPath)
    {
        var directoryInfo = new DirectoryInfo(path);
        if (directoryInfo.Exists)
        {
            return directoryInfo.GetDirectories().Select(y => y.FullName).Concat(directoryInfo.GetFiles().Select(y => y.FullName)).SelectMany(x => GetPathList(x, rootPath)).ToList();
        }
        else
        {
            var fileInfo = new FileInfo(path);
            if (!fileInfo.Exists) throw new FileNotFoundException("file not found", path);
            var pathInfo = new FilePathInfo(path, path.TrimStart(rootPath).TrimStart("\\"), fileInfo.Name, fileInfo.Length);
            return new List<FilePathInfo> { pathInfo };
        }
    }
}

internal abstract class CompressHandler<TOutputStream, TEntry> : CompressHandler where TOutputStream : Stream where TEntry : class
{
    protected CompressHandler(CompressOption option) : base(option)
    { }

    public abstract TOutputStream CreateStream(Stream targetStream);

    public abstract Task WriteNextEntryAsync(TOutputStream outputStream, FilePathInfo pathInfo);

    public override async Task HandleAsync()
    {
        if (Option.SourcePaths.IsNullOrEmpty()) throw new InvalidOperationException("source path required");
        if (!SupportPassword && Option.Password.NotNullOrWhiteSpace()) throw new InvalidDataException("password not supported");
        Option.TargetPath.RemoveFileIfExist();

        var pathList = GetPathList();
        using var targetStream = new FileInfo(Option.TargetPath).OpenOrCreate();
        using var outputStream = CreateStream(targetStream);
        Option.Total = pathList.Sum(x => x.Size);

        foreach (var path in pathList)
        {
            if (Option.CancellationToken.IsCancellationRequested) break;
            await WriteNextEntryAsync(outputStream, path);
        }

        if (Option.CancellationToken.IsCancellationRequested) throw new OperationCanceledException(Option.CancellationToken);
        await outputStream.FlushAsync(Option.CancellationToken);
    }
}

internal class FilePathInfo
{
    public FilePathInfo(string path, string name, string selfName, long size)
    {
        Path = path;
        Name = name;
        Size = size;
        SelfName = selfName;
    }

    public string Path { get; }
    public string Name { get; }
    public string SelfName { get; }
    public long Size { get; }
}





