using System.ComponentModel;
using System.Net;
using System.Net.Http.Headers;

//copy from https://github.com/aspnet/AspNetWebStack
namespace SharpDevLib.Transport.Internal.References;

internal class ProgressMessageHandler : DelegatingHandler
{
    public ProgressMessageHandler()
    {
    }

    public ProgressMessageHandler(HttpMessageHandler innerHandler) : base(innerHandler)
    {
    }

    public event EventHandler<HttpProgressEventArgs>? HttpSendProgress;
    public event EventHandler<HttpProgressEventArgs>? HttpReceiveProgress;
    public event EventHandler<EventArgs>? HttpStartSend;

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        if (HttpStartSend is not null) HttpStartSend(this, EventArgs.Empty);
        AddRequestProgress(request);
        var response = await base.SendAsync(request, cancellationToken);

        if (HttpReceiveProgress != null && response != null && response.Content != null)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await AddResponseProgressAsync(request, response);
        }
        return response!;
    }

    protected internal virtual void OnHttpRequestProgress(HttpRequestMessage request, HttpProgressEventArgs e)
    {
        if (HttpSendProgress is not null && e.TotalBytes > 0) HttpSendProgress(request, e);
    }

    protected internal virtual void OnHttpResponseProgress(HttpRequestMessage request, HttpProgressEventArgs e)
    {
        if (HttpReceiveProgress is not null && e.TotalBytes > 0) HttpReceiveProgress(request, e);
    }

    private void AddRequestProgress(HttpRequestMessage request)
    {
        if (HttpSendProgress is null || request is null || request.Content is null) return;

        HttpContent progressContent = new ProgressContent(request.Content, this, request);
        request.Content = progressContent;
    }

    private async Task<HttpResponseMessage> AddResponseProgressAsync(HttpRequestMessage request, HttpResponseMessage response)
    {
        var stream = await response.Content.ReadAsStreamAsync();
        var progressStream = new ProgressStream(stream, this, request, response);
        var progressContent = new StreamContent(progressStream);
        response.Content.Headers.CopyTo(progressContent.Headers);
        response.Content = progressContent;
        return response;
    }
}

internal class ProgressContent : HttpContent
{
    private readonly HttpContent _innerContent;
    private readonly ProgressMessageHandler _handler;
    private readonly HttpRequestMessage _request;

    public ProgressContent(HttpContent innerContent, ProgressMessageHandler handler, HttpRequestMessage request)
    {
        _innerContent = innerContent;
        _handler = handler;
        _request = request;
        innerContent.Headers.CopyTo(Headers);
    }

    protected override Task SerializeToStreamAsync(Stream stream, TransportContext context)
    {
        var progressStream = new ProgressStream(stream, _handler, _request, null);
        return _innerContent.CopyToAsync(progressStream);
    }

    protected override bool TryComputeLength(out long length)
    {
        var contentLength = _innerContent.Headers.ContentLength;
        if (contentLength.HasValue)
        {
            length = contentLength.Value;
            return true;
        }

        length = -1;
        return false;
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        if (disposing) _innerContent.Dispose();
    }
}

internal class ProgressStream : DelegatingStream
{
    private readonly ProgressMessageHandler _handler;
    private readonly HttpRequestMessage _request;
    private long _bytesReceived;
    private readonly long? _totalBytesToReceive;
    private long _bytesSent;
    private readonly long? _totalBytesToSend;

    public ProgressStream(Stream innerStream, ProgressMessageHandler handler, HttpRequestMessage request, HttpResponseMessage? response) : base(innerStream)
    {
        if (request.Content is not null) _totalBytesToSend = request.Content.Headers.ContentLength;
        if (response is not null && response.Content is not null) _totalBytesToReceive = response.Content.Headers.ContentLength;
        _handler = handler;
        _request = request;
    }

    public override int Read(byte[] buffer, int offset, int count)
    {
        int bytesRead = InnerStream.Read(buffer, offset, count);
        ReportBytesReceived(bytesRead, null);
        return bytesRead;
    }

    public override int ReadByte()
    {
        int byteRead = InnerStream.ReadByte();
        ReportBytesReceived(byteRead == -1 ? 0 : 1, null);
        return byteRead;
    }

    public override async Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
    {
        int readCount = await InnerStream.ReadAsync(buffer, offset, count, cancellationToken);
        ReportBytesReceived(readCount, null);
        return readCount;
    }

    public override void Write(byte[] buffer, int offset, int count)
    {
        InnerStream.Write(buffer, offset, count);
        ReportBytesSent(count, null);
    }

    public override void WriteByte(byte value)
    {
        InnerStream.WriteByte(value);
        ReportBytesSent(1, null);
    }

    public override async Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
    {
        await InnerStream.WriteAsync(buffer, offset, count, cancellationToken);
        ReportBytesSent(count, null);
    }

    internal void ReportBytesSent(int bytesSent, object? userState)
    {
        if (bytesSent <= 0) return;

        _bytesSent += bytesSent;
        int percentage = 0;
        if (_totalBytesToSend.HasValue && _totalBytesToSend != 0) percentage = (int)(100L * _bytesSent / _totalBytesToSend);
        _handler.OnHttpRequestProgress(_request, new HttpProgressEventArgs(percentage, userState!, _bytesSent, _totalBytesToSend));
    }

    private void ReportBytesReceived(int bytesReceived, object? userState)
    {
        if (bytesReceived <= 0) return;

        _bytesReceived += bytesReceived;
        int percentage = 0;
        if (_totalBytesToReceive.HasValue && _totalBytesToReceive != 0) percentage = (int)(100L * _bytesReceived / _totalBytesToReceive);
        _handler.OnHttpResponseProgress(_request, new HttpProgressEventArgs(percentage, userState!, _bytesReceived, _totalBytesToReceive));
    }
}

internal abstract class DelegatingStream : Stream
{
    private readonly Stream _innerStream;

    protected DelegatingStream(Stream innerStream)
    {
        _innerStream = innerStream ?? throw new NullReferenceException("innerStream");
    }

    protected Stream InnerStream => _innerStream;

    public override bool CanRead => _innerStream.CanRead;

    public override bool CanSeek => _innerStream.CanSeek;

    public override bool CanWrite => _innerStream.CanWrite;

    public override long Length => _innerStream.Length;

    public override long Position
    {
        get => _innerStream.Position;
        set => _innerStream.Position = value;
    }

    public override int ReadTimeout
    {
        get => _innerStream.ReadTimeout;
        set => _innerStream.ReadTimeout = value;
    }

    public override bool CanTimeout => _innerStream.CanTimeout;

    public override int WriteTimeout
    {
        get => _innerStream.WriteTimeout;
        set => _innerStream.WriteTimeout = value;
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing) _innerStream.Dispose();
        base.Dispose(disposing);
    }

    public override long Seek(long offset, SeekOrigin origin) => _innerStream.Seek(offset, origin);

    public override int Read(byte[] buffer, int offset, int count) => _innerStream.Read(buffer, offset, count);

    public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken) => _innerStream.ReadAsync(buffer, offset, count, cancellationToken);

    public override int ReadByte() => _innerStream.ReadByte();

    public override void Flush() => _innerStream.Flush();

    public override Task FlushAsync(CancellationToken cancellationToken) => _innerStream.FlushAsync(cancellationToken);

    public override void SetLength(long value) => _innerStream.SetLength(value);

    public override void Write(byte[] buffer, int offset, int count) => _innerStream.Write(buffer, offset, count);

    public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken) => _innerStream.WriteAsync(buffer, offset, count, cancellationToken);

    public override void WriteByte(byte value) => _innerStream.WriteByte(value);
}

internal class HttpProgressEventArgs : ProgressChangedEventArgs
{
    public HttpProgressEventArgs(int progressPercentage, object userToken, long bytesTransferred, long? totalBytes) : base(progressPercentage, userToken)
    {
        BytesTransferred = bytesTransferred;
        TotalBytes = totalBytes;
    }

    public long BytesTransferred { get; private set; }

    public long? TotalBytes { get; private set; }
}

internal static class HttpHeaderExtensions
{
    public static void CopyTo(this HttpContentHeaders fromHeaders, HttpContentHeaders toHeaders)
    {
        foreach (var header in fromHeaders)
        {
            toHeaders.TryAddWithoutValidation(header.Key, header.Value);
        }
    }
}