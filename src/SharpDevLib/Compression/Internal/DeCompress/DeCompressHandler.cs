using SharpCompress.Common;
using SharpCompress.Readers;

namespace SharpDevLib.Compression.Internal.DeCompress;

internal abstract class DeCompressHandler(DeCompressRequest request)
{
    public DeCompressRequest Request { get; } = request;

    public virtual async Task HandleAsync()
    {
        Request.TargetPath.CreateDirectoryIfNotExist();
        await Task.Yield();

        var fileInfo = new FileInfo(Request.SourceFile);
        using var sourceStream = fileInfo.OpenOrCreate();
        SetTotalSize(sourceStream);
        var options = new ReaderOptions
        {
            Password = Request.Password,
            LeaveStreamOpen = true,
        };
        using var reader = ReaderFactory.Open(sourceStream, options);
        reader.EntryExtractionProgress += (_, e) =>
        {
            Request.CurrentName = reader.Entry.Key;
            Request.Transfered += e.ReaderProgress?.BytesTransferred ?? 0;
        };
        while (reader.MoveToNextEntry())
        {
            if (Request.CancellationToken?.IsCancellationRequested ?? false) break;
            if (reader.Entry.IsDirectory) continue;

            reader.WriteEntryToDirectory(Request.TargetPath, new ExtractionOptions
            {
                Overwrite = true,
                ExtractFullPath = true
            });
        }

        if (Request.CancellationToken?.IsCancellationRequested ?? false) throw new OperationCanceledException(Request.CancellationToken.Value);
    }

    private void SetTotalSize(Stream sourceStream)
    {
        using var reader = ReaderFactory.Open(sourceStream, new ReaderOptions { Password = Request.Password, LeaveStreamOpen = true });
        while (reader.MoveToNextEntry())
        {
            Request.Total += reader.Entry?.Size ?? 0;
        }
        reader.Dispose();
        sourceStream.Seek(0, SeekOrigin.Begin);
    }
}





