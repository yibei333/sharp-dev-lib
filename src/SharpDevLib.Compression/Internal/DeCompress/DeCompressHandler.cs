using SharpCompress.Common;
using SharpCompress.Readers;
using SharpDevLib.Compression.Internal.References;

namespace SharpDevLib.Compression.Internal.DeCompress;

internal abstract class DeCompressHandler
{
    protected DeCompressHandler(DeCompressOption option, CancellationToken? cancellationToken)
    {
        Option = option;
        CancellationToken = cancellationToken;
    }
    public DeCompressOption Option { get; }
    public CancellationToken? CancellationToken { get; }

    public virtual async Task HandleAsync()
    {
        Option.TargetPath.EnsureDirectoryExist();
        await Task.Yield();

        var fileInfo = new FileInfo(Option.SourceFile);
        using var sourceStream = fileInfo.OpenOrCreate();
        SetTotalSize(sourceStream);
        using var reader = ReaderFactory.Open(sourceStream, new ReaderOptions { Password = Option.Password, LeaveStreamOpen = true });
        reader.EntryExtractionProgress += (_, e) =>
        {
            Option.CurrentName = reader.Entry.Key;
            Option.Transfered += e.ReaderProgress?.BytesTransferred ?? 0;
        };
        while (reader.MoveToNextEntry())
        {
            if (CancellationToken?.IsCancellationRequested ?? false) break;
            if (reader.Entry.IsDirectory) continue;

            reader.WriteEntryToDirectory(Option.TargetPath, new ExtractionOptions
            {
                Overwrite = true,
                ExtractFullPath = true
            });
        }

        if (CancellationToken?.IsCancellationRequested ?? false) throw new OperationCanceledException(CancellationToken.Value);
    }

    private void SetTotalSize(Stream sourceStream)
    {
        using var reader = ReaderFactory.Open(sourceStream, new ReaderOptions { Password = Option.Password, LeaveStreamOpen = true });
        while (reader.MoveToNextEntry())
        {
            Option.Total += reader.Entry?.Size ?? 0;
        }
        reader.Dispose();
        sourceStream.Seek(0, SeekOrigin.Begin);
    }
}





