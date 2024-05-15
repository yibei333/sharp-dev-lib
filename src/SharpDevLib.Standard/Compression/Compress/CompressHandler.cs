namespace SharpDevLib.Standard.Compression.Compress;

internal abstract class CompressHandler
{
    protected CompressHandler(CompressOption option)
    {
        Option = option;
    }

    public CompressOption Option { get; }

    public virtual bool SupportPassword { get; } = false;

    public abstract Task HandleAsync();
}

internal abstract class CompressHandler<TOutputStream, TEntry> : CompressHandler where TOutputStream : Stream where TEntry : class
{
    protected CompressHandler(CompressOption option) : base(option)
    { }

    public abstract TEntry CreateEntry(string key, string path);

    public abstract TOutputStream CreateStream(Stream sourceStream);

    public abstract void PutNextEntry(TOutputStream stream, TEntry entry);

    public override async Task HandleAsync()
    {
        if (Option.SourcePaths.IsNullOrEmpty()) throw new InvalidOperationException("source path required");
        if (!SupportPassword && Option.Password.NotNullOrWhiteSpace()) throw new InvalidDataException("password not supported");
        Option.TargetPath.RemoveFileIfExist();

        var entries = GetEntries();
        using var stream = new FileInfo(Option.TargetPath).OpenOrCreate();
        using var outputStream = CreateStream(stream);

        var progress = Option.OnProgress is null ? null : new CompressionProgressArgs { Total = entries.Sum(x => x.FileInfo.Length) };
        foreach (var entry in entries)
        {
            if (Option.CancellationToken.IsCancellationRequested) break;
            PutNextEntry(outputStream, entry.Entry);
            using var entryStream = entry.FileInfo.OpenRead();
            await entryStream.CopyToAsync(outputStream, Statics.BufferSize, Option.CancellationToken);
            if (Option.OnProgress is not null)
            {
                progress!.CurrentName = entry.FileInfo.FullName;
                progress.Handled += entry.FileInfo.Length;
                Option.OnProgress?.Invoke(progress);
            }
        }

        if (Option.CancellationToken.IsCancellationRequested) throw new OperationCanceledException(Option.CancellationToken);
        await outputStream.FlushAsync(Option.CancellationToken);
    }

    List<EntryInfo<TEntry>> GetEntries()
    {
        var result = new List<EntryInfo<TEntry>>();
        Option.SourcePaths.ForEach(x =>
        {
            var directoryInfo = new DirectoryInfo(x);
            if (directoryInfo.Exists) result.AddRange(GetEntries(directoryInfo));
            else
            {
                var fileInfo = new FileInfo(x);
                if (fileInfo.Exists) result.Add(GetEntry(fileInfo));
                else throw new FileNotFoundException("file not found", x);
            }
        });
        return result;
    }

    List<EntryInfo<TEntry>> GetEntries(DirectoryInfo directoryInfo, string relative = "")
    {
        if (relative.IsNullOrWhiteSpace() && Option.IncludeSourceDiretory) relative = directoryInfo.Name;

        var result = new List<EntryInfo<TEntry>>();
        foreach (var childDir in directoryInfo.GetDirectories()) result.AddRange(GetEntries(childDir, relative.CombinePath(childDir.Name)));
        foreach (var childFile in directoryInfo.GetFiles()) result.Add(GetEntry(childFile, relative));
        return result;
    }

    EntryInfo<TEntry> GetEntry(FileInfo file, string relative = "")
    {
        return new EntryInfo<TEntry>(file, CreateEntry(relative.CombinePath(file.Name), file.FullName));
    }
}

internal class EntryInfo<TEntry> where TEntry : class
{
    public EntryInfo(FileInfo fileInfo, TEntry entry)
    {
        FileInfo = fileInfo;
        Entry = entry;
    }

    public FileInfo FileInfo { get; set; }
    public TEntry Entry { get; set; }
}





