using SharpCompress.Archives;
using SharpCompress.Readers;

namespace SharpDevLib.Compression.Internal.DeCompress;

internal abstract class DeCompressHandler(DeCompressRequest request)
{
    public DeCompressRequest Request { get; } = request;

    public virtual async Task HandleAsync()
    {
        Request.TargetPath.CreateDirectoryIfNotExist();
        var fileInfo = new FileInfo(Request.SourceFile);
        using var sourceStream = fileInfo.OpenOrCreate();
        SetTotalSize(sourceStream);
        var options = new ReaderOptions
        {
            Password = Request.Password,
            LeaveStreamOpen = true,
        };
        using var reader = ReaderFactory.Open(sourceStream, options);
        while (reader.MoveToNextEntry())
        {
            if (Request.CancellationToken?.IsCancellationRequested ?? false) throw new OperationCanceledException(Request.CancellationToken.Value);
            if (reader.Entry.IsDirectory) continue;

            using var entryStream = reader.OpenEntryStream();
            string targetFile = Path.Combine(Request.TargetPath, reader.Entry.Key);
            targetFile.GetFileDirectory().CreateDirectoryIfNotExist();
            targetFile.RemoveFileIfExist();
            using var fileStream = File.Create(targetFile);
            await entryStream.CopyToAsync(fileStream, Request.CancellationToken ?? CancellationToken.None, transfered =>
            {
                Request.CurrentName = reader.Entry.Key;
                Request.Transfered = transfered;
            });
        }
        if (Request.OnProgress is not null && Request.Total > 0 && Request.Transfered != Request.Total)
        {
            Request.Transfered = Request.Total;
        }
    }

    void SetTotalSize(Stream sourceStream)
    {
        try
        {
            sourceStream.Seek(0, SeekOrigin.Begin);
            using var archive = ArchiveFactory.Open(sourceStream);
            Request.Total = archive.TotalUncompressSize;
        }
        catch
        {
            sourceStream.Seek(0, SeekOrigin.Begin);
            using var reader = ReaderFactory.Open(sourceStream, new ReaderOptions { Password = Request.Password, LeaveStreamOpen = true });
            while (reader.MoveToNextEntry())
            {
                Request.Total += reader.Entry?.Size ?? 0;
            }
            reader.Dispose();
        }
        sourceStream.Seek(0, SeekOrigin.Begin);
    }
}





