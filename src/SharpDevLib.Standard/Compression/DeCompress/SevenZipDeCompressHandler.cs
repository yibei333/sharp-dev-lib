using SharpCompress.Archives;
using SharpCompress.Archives.SevenZip;
using SharpCompress.Common;
using SharpCompress.Readers;

namespace SharpDevLib.Standard.Compression.DeCompress;

internal class SevenZipDeCompressHandler : DeCompressHandler
{
    public SevenZipDeCompressHandler(DeCompressOption option) : base(option)
    {
    }

    public override async Task HandleAsync()
    {
        await Task.Yield();
        using var archive = SevenZipArchive.Open(Option.SourceFile, new ReaderOptions { Password = Option.Password });
        var progress = Option.OnProgress is null ? null : new CompressionProgressArgs<double> { Total = archive.TotalUncompressSize };

        foreach (var entry in archive.Entries.Where(entry => !entry.IsDirectory))
        {
            if (Option.CancellationToken.IsCancellationRequested) break;
            entry.WriteToDirectory(Option.TargetPath, new ExtractionOptions
            {
                ExtractFullPath = true,
                Overwrite = true
            });

            if (progress is not null)
            {
                progress.CurrentName = entry.Key;
                progress.Handled += entry.Size;
                Option.OnProgress!.Invoke(progress);
            }
        }
        if (Option.CancellationToken.IsCancellationRequested) throw new OperationCanceledException(Option.CancellationToken);
    }
}
