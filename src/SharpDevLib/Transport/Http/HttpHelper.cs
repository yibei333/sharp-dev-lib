using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharpDevLib.Transport.Internal;
using System;
using System.Net;
using System.Text;

namespace SharpDevLib;

/// <summary>
/// http扩展
/// </summary>
public static class HttpHelper
{
    public static string ToQueryString(this IEnumerable<KeyValuePair<string, string?>>? parameters) => string.Join("&", parameters?.Select(x => $"{x.Key}={x.Value}"));

    public static async Task<HttpResponse> GetAsync(this HttpRequest request, CancellationToken? cancellationToken = null) => await request.SendAsync(HttpMethod.Get, cancellationToken);

    public static async Task<Stream> GetStreamAsync(this HttpRequest request, CancellationToken? cancellationToken = null)
    {
        var response = await request.GetAsync(cancellationToken);
        response.EnsureSuccessStatusCode();
        return await response.HttpResponseMessage.Content.ReadAsStreamAsync();
    }

    public static async Task<HttpResponse> PostAsync(this HttpRequest request, CancellationToken? cancellationToken = null) => await request.SendAsync(HttpMethod.Post, cancellationToken);
    
    public static async Task<HttpResponse> PutAsync(this HttpRequest request, CancellationToken? cancellationToken = null) => await request.SendAsync(HttpMethod.Put, cancellationToken);
    
    public static async Task<HttpResponse> DeleteAsync(this HttpRequest request, CancellationToken? cancellationToken = null) => await request.SendAsync(HttpMethod.Delete, cancellationToken);

    #region Private
    static async Task<HttpResponse> SendAsync(this HttpRequest request, HttpMethod method, CancellationToken? cancellationToken = null)
    {
        if (request.Url.IsNullOrWhiteSpace()) throw new InvalidOperationException("url requried");
        using var client = CreateClient(request);
        if (method == HttpMethod.Get || method == HttpMethod.Delete)
        {
            var url = request.Url.TrimEnd("?");
            var prefix = url.Contains("?") ? "&" : "?";
            request.RequestUrl = $"{url}{prefix}{request.Parameters.ToQueryString()}".Utf8Decode().UrlEncode();
            request.HttpRequestMessage = new HttpRequestMessage(method, request.RequestUrl);
        }
        else
        {
            HttpContent? content = null;
            if (request.Json.NotNullOrEmpty())
            {
                content = new StringContent(request.Json, Encoding.UTF8, "application/json");
            }
            else if (request.Files.NotNullOrEmpty())
            {
                var multipartFormDataContent = new MultipartFormDataContent();
                if (request.Parameters.NotNullOrEmpty())
                {
                    foreach (var item in request.Parameters)
                    {
                        multipartFormDataContent.Add(new StringContent(item.Value), item.Key);
                    }
                }
                foreach (var file in request.Files)
                {
                    var stream = file.Stream;
                    if (stream is null)
                    {
                        if (file.Bytes.IsNullOrEmpty()) throw new Exception($"file data required");
                        else stream = new MemoryStream(file.Bytes);
                    }
                    multipartFormDataContent.Add(new StreamContent(stream), file.ParameterName, file.FileName);
                }
                content = multipartFormDataContent;
            }
            else if (request.Parameters.NotNullOrEmpty())
            {
                content = new FormUrlEncodedContent(request.Parameters ?? []);
            }
            request.HttpRequestMessage = new HttpRequestMessage(method, request.Url);
            if (content is not null) request.HttpRequestMessage.Content = content;
        }
        var responseMonitor = await RetryAsync(client, request, cancellationToken);
        return responseMonitor.BuildResponse();
    }

    static HttpClient CreateClient(HttpRequest request)
    {
        var handler = new HttpClientHandler { CookieContainer = new CookieContainer() };
        if (request.Cookies.NotNullOrEmpty())
        {
            handler.CookieContainer = new CookieContainer();
            foreach (var cookie in request.Cookies) handler.CookieContainer.Add(cookie);
        }

        var progressHanlder = new ProgressMessageHandler(handler);
        var client = new HttpClient(progressHanlder);

        var onSendProgress = request.Config?.OnSendProgress ?? HttpConfig.Default?.OnSendProgress;
        if (onSendProgress is not null)
        {
            var progress = new HttpProgress();
            progressHanlder.HttpStartSend += (_, _) => progress.Reset();
            progressHanlder.HttpSendProgress += (_, e) =>
            {
                progress.Total = e.TotalBytes ?? 0;
                progress.Transfered = e.BytesTransferred;
                onSendProgress(progress);
            };
        }

        var onReceiveProgress = request.Config?.OnReceiveProgress ?? HttpConfig.Default?.OnReceiveProgress;
        if (onReceiveProgress is not null)
        {
            var progress = new HttpProgress();
            progressHanlder.HttpStartSend += (_, _) => progress.Reset();
            progressHanlder.HttpReceiveProgress += (_, e) =>
            {
                progress.Total = e.TotalBytes ?? 0;
                progress.Transfered = e.BytesTransferred;
                onReceiveProgress(progress);
            };
        }

        var ua = request.Config?.UserAgent ?? HttpConfig.Default?.UserAgent;
        if (ua.NotNullOrWhiteSpace() && !(request.Headers?.ContainsKey("User-Agent") ?? false))
        {
            request.Headers ??= [];
            request.Headers.Add("User-Agent", [ua]);
        }

        if (request.Headers.NotNullOrEmpty())
        {
            foreach (var header in request.Headers)
            {
                if (header.Value.NotNullOrEmpty()) client.DefaultRequestHeaders.Add(header.Key, header.Value);
            }
        }

        client.Timeout = request.Config?.TimeOut ?? HttpConfig.Default?.TimeOut ?? TimeSpan.FromDays(1);
        return client;
    }

    static async Task<ResponseMonitor> RetryAsync(HttpClient client, HttpRequest request, CancellationToken? cancellationToken = null)
    {
        var retryCount = request.Config?.RetryCount ?? HttpConfig.Default?.RetryCount ?? 0;
        var retryIndex = -1;
        var totalStartTime = DateTime.Now;
        HttpResponseMessage? response = null;
        Exception? exception = null;
        TimeSpan last;
        TimeSpan total = TimeSpan.Zero;
        string url = string.Empty;

        do
        {
            if (cancellationToken?.IsCancellationRequested ?? false) break;

            var startTime = DateTime.Now;
            retryIndex++;
            try
            {
                response = await client.SendAsync(request.HttpRequestMessage, HttpCompletionOption.ResponseContentRead, cancellationToken ?? CancellationToken.None);
                var endTime = DateTime.Now;
                last = endTime - startTime;
                total += last;

                if (response is null || !response.IsSuccessStatusCode)
                {
                    continue;
                }
                return new ResponseMonitor(url, request, null, retryIndex, last, endTime - totalStartTime, response);
            }
            catch (Exception ex)
            {
                last = DateTime.Now - startTime;
                total += last;
                exception = ex;
            }
        } while (retryIndex < retryCount);
        return new ResponseMonitor(url, request, exception, retryIndex, last, total, response);
    }

    class ResponseMonitor(string url, HttpRequest request, Exception? exception, int retryCount, TimeSpan lastTimeConsuming, TimeSpan totalTimeConsuming, HttpResponseMessage? responseMessage)
    {
        public string Url { get; } = url;

        public Exception? Exception { get; } = exception;

        public int RetryCount { get; } = retryCount;

        public TimeSpan LastTimeConsuming { get; } = lastTimeConsuming;

        public TimeSpan TotalTimeConsuming { get; } = totalTimeConsuming;

        public HttpResponseMessage? ResponseMessage { get; } = responseMessage;

        public HttpRequest Request { get; } = request;

        public HttpResponse BuildResponse()
        {
            return new HttpResponse(Request, ResponseMessage, Exception?.Message, RetryCount, LastTimeConsuming, TotalTimeConsuming);
        }
    }
    #endregion
}