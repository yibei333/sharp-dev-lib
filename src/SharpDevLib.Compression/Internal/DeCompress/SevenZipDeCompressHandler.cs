using SharpCompress.Archives;
using SharpCompress.Archives.SevenZip;
using SharpCompress.Common;
using SharpCompress.Readers;
using SharpDevLib.Compression.Internal.References;

namespace SharpDevLib.Compression.Internal.DeCompress;

internal class SevenZipDeCompressHandler : DeCompressHandler
{
    public SevenZipDeCompressHandler(DeCompressOption option, CancellationToken? cancellationToken) : base(option, cancellationToken)
    {
    }

    public override async Task HandleAsync()
    {
        await Task.Yield();
        Option.TargetPath.EnsureDirectoryExist();
        using var archive = SevenZipArchive.Open(Option.SourceFile, new ReaderOptions { Password = Option.Password });
        var progress = Option.OnProgress is null ? null : new CompressionProgressArgs { Total = archive.TotalUncompressSize };

        foreach (var entry in archive.Entries.Where(entry => !entry.IsDirectory))
        {
            if (CancellationToken?.IsCancellationRequested ?? false) break;
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
        if (CancellationToken?.IsCancellationRequested ?? false) throw new OperationCanceledException(CancellationToken.Value);
    }
}
