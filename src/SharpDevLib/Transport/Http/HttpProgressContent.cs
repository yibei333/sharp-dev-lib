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
        _progress = new HttpProgress { RequestUrl = request.Message?.RequestUri?.ToString() ?? string.Empty, Total = requestMessage.Content?.Headers?.ContentLength ?? 0 };
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
        var lastProgress = _progress.Progress;
        var progressStream = new HttpProgressStream(stream, (p) =>
        {
            _progress.Transfered = p;
            if (_progress.Progress >= 100 || (_progress.Progress - lastProgress) > 5)
            {
                _onProgress?.Invoke(_progress);
                lastProgress = _progress.Progress;
            }
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