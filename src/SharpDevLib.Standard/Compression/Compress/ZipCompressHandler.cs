using ICSharpCode.SharpZipLib.Zip;

namespace SharpDevLib.Standard.Compression.Compress;

internal class ZipCompressHandler : CompressHandler
{
    public ZipCompressHandler(CompressOption option) : base(option)
    {
    }

    public override async Task HandleAsync()
    {
        if (Option.SourcePaths.IsNullOrEmpty()) throw new InvalidOperationException("source path required");
        var entries = GetEntries();
        using var stream = new FileInfo(Option.TargetPath).OpenOrCreate();
        using var outputStream = new ZipOutputStream(stream)
        {
            Password = Option.Password,
            UseZip64 = UseZip64.Dynamic,
        };
        outputStream.SetLevel(Option.Level.ConvertToSharpZipLibLevel());

        var count = 0;
        var progress = Option.OnProgress is null ? null : new CompressionProgressArgs<int> { Total = entries.Count };
        foreach (var entry in entries)
        {
            if (Option.CancellationToken.IsCancellationRequested) break;
            outputStream.PutNextEntry(entry.Entry);
            using var entryStream = entry.FileInfo.OpenRead();
            await entryStream.CopyToAsync(outputStream, Statics.BufferSize, Option.CancellationToken);
            count++;
            if (Option.OnProgress is not null)
            {
                progress!.CurrentName = entry.FileInfo.FullName;
                progress.Handled = count;
                Option.OnProgress?.Invoke(progress);
            }
        }

        if (Option.CancellationToken.IsCancellationRequested) throw new OperationCanceledException(Option.CancellationToken);
        await outputStream.FlushAsync(Option.CancellationToken);
    }

    List<ZipEntryInfo> GetEntries()
    {
        var result = new List<ZipEntryInfo>();
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

    List<ZipEntryInfo> GetEntries(DirectoryInfo directoryInfo, string relative = "")
    {
        if (relative.IsNullOrWhiteSpace() && Option.IncludeSourceDiretory) relative = directoryInfo.Name;

        var result = new List<ZipEntryInfo>();
        foreach (var childDir in directoryInfo.GetDirectories()) result.AddRange(GetEntries(childDir, relative.CombinePath(childDir.Name)));
        foreach (var childFile in directoryInfo.GetFiles()) result.Add(GetEntry(childFile, relative));
        return result;
    }

    ZipEntryInfo GetEntry(FileInfo file, string relative = "")
    {
        var entry = new ZipEntry(relative.CombinePath(file.Name));
        return new ZipEntryInfo(file, entry);
    }

    class ZipEntryInfo
    {
        public ZipEntryInfo(FileInfo fileInfo, ZipEntry entry)
        {
            FileInfo = fileInfo;
            Entry = entry;
        }

        public FileInfo FileInfo { get; set; }
        public ZipEntry Entry { get; set; }
    }
}
