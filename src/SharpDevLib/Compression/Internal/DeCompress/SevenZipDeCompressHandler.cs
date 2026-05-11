using SharpCompress.Archives.SevenZip;
using SharpCompress.Readers;

namespace SharpDevLib.Compression.Internal.DeCompress;

internal class SevenZipDeCompressHandler(DeCompressRequest request) : DeCompressHandler(request)
{
    public override async Task HandleAsync()
    {
        Request.TargetPath.CreateDirectoryIfNotExist();
        using var archive = SevenZipArchive.OpenArchive(Request.SourceFile, new ReaderOptions { Password = Request.Password });
        var progress = Request.OnProgress is null ? null : new CompressionProgressArgs { Total = archive.TotalUncompressedSize };

        foreach (var entry in archive.Entries.Where(entry => !entry.IsDirectory))
        {
            if (Request.CancellationToken?.IsCancellationRequested ?? false) throw new OperationCanceledException(Request.CancellationToken.Value);
            if (entry.Key.IsNullOrWhiteSpace()) continue;
            using var entryStream = entry.OpenEntryStream();
            string targetFile = Path.Combine(Request.TargetPath, entry.Key);
            targetFile.GetFileDirectory().CreateDirectoryIfNotExist();
            targetFile.RemoveFileIfExist();
            using var fileStream = File.Create(targetFile);
            await entryStream.CopyToAsync(fileStream, Request.CancellationToken ?? CancellationToken.None);

            if (progress is not null)
            {
                progress.CurrentName = entry.Key;
                progress.Transferred += entry.Size;
                Request.OnProgress!.Invoke(progress);
            }
        }
    }
}
