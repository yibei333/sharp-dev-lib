using System.Net;

namespace SharpDevLib;

internal class HttpProgressContent : HttpContent
{
    public static HttpRequestMessage Convert(HttpRequest request, HttpRequestMessage requestMessage)
    {
        requestMessage.Content = new HttpProgressContent(request, requestMessage);
        return requestMessage;
    }

    readonly HttpProgress _progress;
    readonly HttpContent? _innerContent;
    readonly Action<HttpProgress>? _onProgress;

    HttpProgressContent(HttpRequest request, HttpRequestMessage requestMessage)
    {
        _innerContent = requestMessage.Content;
        _progress = new HttpProgress { Total = requestMessage.Content?.Headers?.ContentLength ?? 0 };
        CopyHeaders();
        _onProgress = request.Config?.OnSendProgress ?? HttpConfig.Default.OnSendProgress;
    }

    void CopyHeaders()
    {
        if (_innerContent?.Headers?.IsNullOrEmpty() ?? true) return;
        foreach (var header in _innerContent.Headers)
        {
            Headers.TryAddWithoutValidation(header.Key, header.Value);
        }
    }

    protected override Task SerializeToStreamAsync(Stream stream, TransportContext context)
    {
        if (_innerContent is null) return Task.CompletedTask;
        var progressStream = new ProgressStream(stream, (p) =>
        {
            var lastProgress = _progress.Progress;
            _progress.Transfered = p;
            if (_progress.Progress == 1 || (_progress.Progress - lastProgress) > 5) _onProgress?.Invoke(_progress);
        });
        return _innerContent.CopyToAsync(progressStream);
    }

    protected override bool TryComputeLength(out long length)
    {
        var contentLength = _innerContent?.Headers?.ContentLength;
        if (contentLength.HasValue)
        {
            length = contentLength.Value;
            return true;
        }

        length = 0;
        return false;
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        if (disposing) _innerContent?.Dispose();
    }
}

internal class ProgressStream(Stream innerStream, Action<long> onProgress) : Stream
{
    readonly Stream _innerStream = innerStream;
    readonly Action<long> _onProgress = onProgress;
    long _bytesSent = 0;

    public override bool CanRead => _innerStream.CanRead;

    public override bool CanSeek => _innerStream.CanSeek;

    public override bool CanWrite => _innerStream.CanWrite;

    public override long Length => _innerStream.Length;

    public override long Position { get => _innerStream.Position; set => _innerStream.Position = value; }

    public override int Read(byte[] buffer, int offset, int count)
    {
        int bytesRead = _innerStream.Read(buffer, offset, count);
        ReportBytesTransfered(bytesRead);
        return bytesRead;
    }

    public override int ReadByte()
    {
        int byteRead = _innerStream.ReadByte();
        ReportBytesTransfered(byteRead);
        return byteRead;
    }

    public override async Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
    {
        int readCount = await _innerStream.ReadAsync(buffer, offset, count, cancellationToken);
        ReportBytesTransfered(readCount);
        return readCount;
    }

    public override void Write(byte[] buffer, int offset, int count)
    {
        _innerStream.Write(buffer, offset, count);
        ReportBytesTransfered(count);
    }

    public override void WriteByte(byte value)
    {
        _innerStream.WriteByte(value);
        ReportBytesTransfered(1);
    }

    public override async Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
    {
        await _innerStream.WriteAsync(buffer, offset, count, cancellationToken);
        ReportBytesTransfered(count);
    }

    internal void ReportBytesTransfered(int bytesSent)
    {
        if (bytesSent <= 0) return;

        _bytesSent += bytesSent;
        _onProgress(_bytesSent);
    }

    public override void Flush()
    {
        _innerStream.Flush();
    }

    public override long Seek(long offset, SeekOrigin origin)
    {
        return _innerStream.Seek(offset, origin);
    }

    public override void SetLength(long value)
    {
        _innerStream.SetLength(value);
    }
}
