using SharpDevLib.Transport.Internal;
using System.Net;
using System.Text;

namespace SharpDevLib;

/// <summary>
/// HTTP扩展，提供HTTP请求、响应处理的扩展方法
/// </summary>
public static class HttpHelper
{
    /// <summary>
    /// 将键值对集合转换为查询字符串格式
    /// </summary>
    /// <param name="parameters">键值对集合</param>
    /// <returns>查询字符串，格式为key1=value1&amp;key2=value2。如果集合为空或null则返回空字符串</returns>
    public static string ToQueryString(this IEnumerable<KeyValuePair<string, string?>>? parameters) => parameters.IsNullOrEmpty() ? string.Empty : string.Join("&", parameters.Select(x => $"{x.Key}={x.Value}"));

    /// <summary>
    /// 异步发送GET请求
    /// </summary>
    /// <param name="request">HTTP请求对象</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>HTTP响应对象</returns>
    /// <exception cref="InvalidOperationException">当URL为空时引发异常</exception>
    public static async Task<HttpResponse> GetAsync(this HttpRequest request, CancellationToken? cancellationToken = null) => await request.SendAsync(HttpMethod.Get, cancellationToken);

    /// <summary>
    /// 异步发送GET请求并获取响应流
    /// </summary>
    /// <param name="request">HTTP请求对象</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>响应流</returns>
    /// <exception cref="InvalidOperationException">当URL为空时引发异常</exception>
    /// <exception cref="Exception">当HTTP响应状态码不成功时抛出</exception>
    public static async Task<Stream> GetStreamAsync(this HttpRequest request, CancellationToken? cancellationToken = null)
    {
        var response = await request.GetAsync(cancellationToken);
        response.EnsureSuccessStatusCode();
        return await response.HttpResponseMessage.Content.ReadAsStreamAsync();
    }

    /// <summary>
    /// 异步发送POST请求
    /// </summary>
    /// <param name="request">HTTP请求对象</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>HTTP响应对象</returns>
    /// <exception cref="InvalidOperationException">当URL为空时引发异常</exception>
    public static async Task<HttpResponse> PostAsync(this HttpRequest request, CancellationToken? cancellationToken = null) => await request.SendAsync(HttpMethod.Post, cancellationToken);

    /// <summary>
    /// 异步发送PUT请求
    /// </summary>
    /// <param name="request">HTTP请求对象</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>HTTP响应对象</returns>
    /// <exception cref="InvalidOperationException">当URL为空时引发异常</exception>
    public static async Task<HttpResponse> PutAsync(this HttpRequest request, CancellationToken? cancellationToken = null) => await request.SendAsync(HttpMethod.Put, cancellationToken);

    /// <summary>
    /// 异步发送DELETE请求
    /// </summary>
    /// <param name="request">HTTP请求对象</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>HTTP响应对象</returns>
    /// <exception cref="InvalidOperationException">当URL为空时引发异常</exception>
    public static async Task<HttpResponse> DeleteAsync(this HttpRequest request, CancellationToken? cancellationToken = null) => await request.SendAsync(HttpMethod.Delete, cancellationToken);

    #region Private
    static async Task<HttpResponse> SendAsync(this HttpRequest request, HttpMethod method, CancellationToken? cancellationToken = null)
    {
        if (request.Url.IsNullOrWhiteSpace()) throw new InvalidOperationException("URL不能为空");
        if (method == HttpMethod.Get || method == HttpMethod.Delete)
        {
            if (request.Parameters.NotNullOrEmpty())
            {
                var url = request.Url.TrimEnd("?");
                var prefix = url.Contains("?") ? "&" : "?";
                request.RequestUrl = $"{url}{prefix}{request.Parameters.ToQueryString()}";
            }
        }
        using var client = CreateClient(request);
        HttpRequestMessage CreateRequestMessage()
        {
            //query string
            if (method == HttpMethod.Get || method == HttpMethod.Delete) return new HttpRequestMessage(method, request.RequestUrl);

            //json
            if (request.Json.NotNullOrEmpty()) return new HttpRequestMessage(method, request.RequestUrl) { Content = new StringContent(request.Json, Encoding.UTF8, "application/json") };

            //multi part form data
            if (request.Files.NotNullOrEmpty())
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
                        if (file.Bytes.IsNullOrEmpty()) throw new Exception($"文件数据不能为空");
                        else stream = new MemoryStream(file.Bytes);
                    }
                    multipartFormDataContent.Add(new StreamContent(stream), file.ParameterName, file.FileName);
                }
                return new HttpRequestMessage(method, request.RequestUrl) { Content = multipartFormDataContent };
            }

            //form url encoded
            if (request.Parameters.NotNullOrEmpty()) return new HttpRequestMessage(method, request.RequestUrl) { Content = new FormUrlEncodedContent(request.Parameters ?? []) };

            //default
            return new HttpRequestMessage(method, request.Url);
        }
        var responseMonitor = await RetryAsync(client, request, CreateRequestMessage, cancellationToken);
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

        client.Timeout = request.Config?.Timeout ?? HttpConfig.Default?.Timeout ?? TimeSpan.FromDays(1);
        return client;
    }

    static async Task<ResponseMonitor> RetryAsync(HttpClient client, HttpRequest request, Func<HttpRequestMessage> createRequestMessage, CancellationToken? cancellationToken = null)
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
                request.HttpRequestMessage = createRequestMessage();
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
            var message = (ResponseMessage?.IsSuccessStatusCode ?? false) ? null : Exception?.Message ?? ResponseMessage?.ReasonPhrase;
            return new HttpResponse(Request, ResponseMessage, message, RetryCount, LastTimeConsuming, TotalTimeConsuming);
        }
    }
    #endregion
}