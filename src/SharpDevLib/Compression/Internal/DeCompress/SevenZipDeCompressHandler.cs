using SharpCompress.Archives;
using SharpCompress.Archives.SevenZip;
using SharpCompress.Common;
using SharpCompress.Readers;

namespace SharpDevLib.Compression.Internal.DeCompress;

internal class SevenZipDeCompressHandler(DeCompressRequest request) : DeCompressHandler(request)
{
    public override async Task HandleAsync()
    {
        await Task.Yield();
        Request.TargetPath.CreateDirectoryIfNotExist();
        using var archive = SevenZipArchive.Open(Request.SourceFile, new ReaderOptions { Password = Request.Password });
        var progress = Request.OnProgress is null ? null : new CompressionProgressArgs { Total = archive.TotalUncompressSize };

        foreach (var entry in archive.Entries.Where(entry => !entry.IsDirectory))
        {
            if (Request.CancellationToken?.IsCancellationRequested ?? false) break;
            entry.WriteToDirectory(Request.TargetPath, new ExtractionOptions
            {
                ExtractFullPath = true,
                Overwrite = true
            });

            if (progress is not null)
            {
                progress.CurrentName = entry.Key;
                progress.Trasnsfed += entry.Size;
                Request.OnProgress!.Invoke(progress);
            }
        }
        if (Request.CancellationToken?.IsCancellationRequested ?? false) throw new OperationCanceledException(Request.CancellationToken.Value);
    }
}
