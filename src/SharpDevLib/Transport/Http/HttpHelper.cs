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
    /// 设置HTTP客户端配置
    /// </summary>
    /// <param name="clientId">HTTP客户端Id</param>
    /// <param name="config">配置</param>
    public static void SetConfig(string clientId, HttpConfig config) => HttpClientFactory.SetConfig(clientId, config);

    /// <summary>
    /// 设置默认HTTP客户端配置
    /// </summary>
    /// <param name="config">配置</param>
    public static void SetDefaultConfig(HttpConfig config) => HttpClientFactory.SetDefaultConfig(config);

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
        return await response.ReadAsStreamAsync();
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
        var url = request.Url;
        if (method == HttpMethod.Get || method == HttpMethod.Delete)
        {
            if (request.Parameters.NotNullOrEmpty())
            {
                url = request.Url.TrimEnd("?");
                var prefix = url.Contains("?") ? "&" : "?";
                url = $"{url}{prefix}{request.Parameters.ToQueryString()}";
            }
        }
        var client = HttpClientFactory.GetClient(request.ClientId);
        request.Cookies?.ForEach(client.ClientHandler.CookieContainer.Add);

        HttpRequestMessage CreateRequestMessage()
        {
            //query string
            if (method == HttpMethod.Get || method == HttpMethod.Delete) return new HttpRequestMessage(method, url);

            //json
            if (request.Json.NotNullOrEmpty()) return new HttpRequestMessage(method, url) { Content = new StringContent(request.Json, Encoding.UTF8, "application/json") };

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
                return new HttpRequestMessage(method, url) { Content = multipartFormDataContent };
            }

            //form url encoded
            if (request.Parameters.NotNullOrEmpty()) return new HttpRequestMessage(method, url) { Content = new FormUrlEncodedContent(request.Parameters ?? []) };

            //default
            return new HttpRequestMessage(method, request.Url);
        }

        var responseMonitor = await RetryAsync(client.Client, request, CreateRequestMessage, cancellationToken);
        return responseMonitor.BuildResponse();
    }

    static async Task<ResponseMonitor> RetryAsync(HttpClient client, HttpRequest request, Func<HttpRequestMessage> createRequestMessage, CancellationToken? cancellationToken = null)
    {
        var retryCount = HttpClientFactory.GetClient(request.ClientId).Config.RetryCount;
        var retryIndex = -1;
        var totalStartTime = DateTime.Now;
        HttpResponseMessage? response = null;
        Exception? exception = null;
        TimeSpan last = TimeSpan.Zero;
        TimeSpan total = TimeSpan.Zero;

        do
        {
            if (cancellationToken?.IsCancellationRequested ?? false) break;

            var startTime = DateTime.Now;
            retryIndex++;
            try
            {
                var message = createRequestMessage();
                request.Message = message;
                var clientInfo = HttpClientFactory.GetClient(request.ClientId);
                var ua = clientInfo.Config.UserAgent;
                if (ua.NotNullOrWhiteSpace() && !(request.Headers?.ContainsKey("User-Agent") ?? false))
                {
                    request.Headers ??= [];
                    request.Headers.Add("User-Agent", [ua]);
                }

                if (request.Headers.NotNullOrEmpty())
                {
                    foreach (var header in request.Headers)
                    {
                        if (header.Value.NotNullOrEmpty()) message.Headers.Add(header.Key, header.Value);
                    }
                }
                response = await client.SendAsync(HttpProgressContent.Convert(request, message), clientInfo.Config.HttpCompletionOption, cancellationToken ?? CancellationToken.None);
                var endTime = DateTime.Now;
                last = endTime - startTime;
                total += last;

                if (response is null || !response.IsSuccessStatusCode)
                {
                    continue;
                }
                return new ResponseMonitor(request, null, retryIndex, last, endTime - totalStartTime, response);
            }
            catch (Exception ex)
            {
                last = DateTime.Now - startTime;
                total += last;
                exception = ex;
            }
        } while (retryIndex < retryCount);
        return new ResponseMonitor(request, exception, retryIndex, last, total, response);
    }

    class ResponseMonitor(HttpRequest request, Exception? exception, int retryCount, TimeSpan lastTimeConsuming, TimeSpan totalTimeConsuming, HttpResponseMessage? responseMessage)
    {
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