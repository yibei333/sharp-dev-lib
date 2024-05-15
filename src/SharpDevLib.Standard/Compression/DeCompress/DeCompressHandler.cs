using SharpCompress.Common;
using SharpCompress.Readers;

namespace SharpDevLib.Standard.Compression.DeCompress;

internal abstract class DeCompressHandler
{
    protected DeCompressHandler(DeCompressOption option)
    {
        Option = option;
    }
    public DeCompressOption Option { get; }

    protected virtual double GetUnCompressedSize(FileInfo fileInfo) => 0;

    public virtual async Task HandleAsync()
    {
        Option.TargetPath.EnsureDirectoryExist();
        await Task.Yield();

        var fileInfo = new FileInfo(Option.SourceFile);
        var totalSize = GetUnCompressedSize(fileInfo);
        using var sourceStream = fileInfo.OpenOrCreate();
        using var reader = ReaderFactory.Open(sourceStream, new ReaderOptions { Password = Option.Password });
        var progress = Option.OnProgress is null ? null : new CompressionProgressArgs<double> { Total = totalSize };

        while (reader.MoveToNextEntry())
        {
            if (Option.CancellationToken.IsCancellationRequested) break;
            if (reader.Entry.IsDirectory) continue;

            reader.WriteEntryToDirectory(Option.TargetPath, new ExtractionOptions
            {
                Overwrite = true,
                ExtractFullPath = true
            });

            if (progress is not null)
            {
                progress.CurrentName = reader.Entry.Key;
                progress.Handled += reader.Entry.Size;
                Option.OnProgress!.Invoke(progress);
            }
        }

        if (Option.CancellationToken.IsCancellationRequested) throw new OperationCanceledException(Option.CancellationToken);
    }
}





