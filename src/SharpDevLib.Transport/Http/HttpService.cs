using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SharpDevLib.Transport.Internal.References;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Web;

namespace SharpDevLib.Transport;

internal class HttpService : IHttpService
{
    const string _edgeUA = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/124.0.0.0 Safari/537.36 Edg/124.0.0.0";
    readonly ILogger<HttpService>? _logger;

    public HttpService(IServiceProvider? provider)
    {
        _logger = provider?.GetService<ILogger<HttpService>>();
        var options = provider?.GetService<IOptionsMonitor<HttpGlobalSettingsOptions>>()?.CurrentValue;
        if (options is not null)
        {
            HttpGlobalOptions.BaseUrl = options.BaseUrl;
            HttpGlobalOptions.RetryCount = options.RetryCount;
            if (options.TimeOut.HasValue) HttpGlobalOptions.TimeOut = TimeSpan.FromSeconds(options.TimeOut.Value);
        }
    }

    async Task<HttpResponse<T>> GetAsync<T>(HttpKeyValueRequest request, bool isGenericMethod, CancellationToken? cancellationToken = null)
    {
        using var client = await CreateClientAsync(request);
        var url = BuildGetUrl(request);
        var responseMonitor = await Retry(client, () => new HttpRequestMessage(HttpMethod.Get, url), request, cancellationToken);
        return await BuildResponse<T>(responseMonitor, nameof(GetAsync), isGenericMethod);
    }

    public async Task<HttpResponse<T>> GetAsync<T>(HttpKeyValueRequest request, CancellationToken? cancellationToken = null) => await GetAsync<T>(request, true, cancellationToken);

    public async Task<HttpResponse> GetAsync(HttpKeyValueRequest request, CancellationToken? cancellationToken = null) => await GetAsync<string>(request, false, cancellationToken);

    public async Task<Stream> GetStreamAsync(HttpKeyValueRequest request)
    {
        try
        {
            using var client = await CreateClientAsync(request);
            var url = BuildGetUrl(request);
            var stream = await client.GetStreamAsync(url);
            Log(null, request, null, nameof(GetStreamAsync));
            return stream;
        }
        catch (Exception ex)
        {
            Log(ex, request, null, nameof(GetAsync));
            throw ex;
        }
    }

    async Task<HttpResponse<T>> PostAsync<T>(HttpJsonRequest request, bool isGenericMethod, CancellationToken? cancellationToken = null)
    {
        using var client = await CreateClientAsync(request);
        var url = BuildUrl(request);
        var responseMonitor = await Retry(client, () =>
        {
            var content = new StringContent(request.Parameters, Encoding.UTF8, "application/json");
            var message = new HttpRequestMessage(HttpMethod.Post, url) { Content = content };
            return message;
        }, request, cancellationToken);

        var response = await BuildResponse<T>(responseMonitor, nameof(PostAsync), isGenericMethod);
        return response;
    }

    public async Task<HttpResponse<T>> PostAsync<T>(HttpJsonRequest request, CancellationToken? cancellationToken = null) => await PostAsync<T>(request, true, cancellationToken);

    public async Task<HttpResponse> PostAsync(HttpJsonRequest request, CancellationToken? cancellationToken = null) => await PostAsync<string>(request, false, cancellationToken);

    async Task<HttpResponse<T>> PostAsync<T>(HttpMultiPartFormDataRequest request, bool isGenericMethod, CancellationToken? cancellationToken = null)
    {
        using var client = await CreateClientAsync(request);
        var url = BuildUrl(request);
        var responseMonitor = await Retry(client, () =>
        {
            var content = new MultipartFormDataContent();
            if (request.Parameters.NotNullOrEmpty())
            {
                foreach (var item in request.Parameters)
                {
                    content.Add(new StringContent(item.Value), item.Key);
                }
            }
            if (request.Files.NotNullOrEmpty())
            {
                foreach (var file in request.Files)
                {
                    var stream = file.Stream;
                    if (stream is null)
                    {
                        if (file.Bytes.IsNullOrEmpty()) throw new Exception($"file data required");
                        else stream = new MemoryStream(file.Bytes);
                    }
                    content.Add(new StreamContent(stream), file.ParameterName, file.FileName);
                }
            }

            var message = new HttpRequestMessage(HttpMethod.Post, url) { Content = content };
            return message;
        }, request, cancellationToken);

        var response = await BuildResponse<T>(responseMonitor, nameof(PostAsync), isGenericMethod);
        return response;
    }

    public async Task<HttpResponse<T>> PostAsync<T>(HttpMultiPartFormDataRequest request, CancellationToken? cancellationToken = null) => await PostAsync<T>(request, true, cancellationToken);

    public async Task<HttpResponse> PostAsync(HttpMultiPartFormDataRequest request, CancellationToken? cancellationToken = null) => await PostAsync<string>(request, false, cancellationToken);

    async Task<HttpResponse<T>> PostAsync<T>(HttpUrlEncodedFormRequest request, bool isGenericMethod, CancellationToken? cancellationToken = null)
    {
        using var client = await CreateClientAsync(request);
        var url = BuildUrl(request);
        var responseMonitor = await Retry(client, () =>
        {
            var content = new FormUrlEncodedContent(request.Parameters ?? new());
            var message = new HttpRequestMessage(HttpMethod.Post, url) { Content = content };
            return message;
        }, request, cancellationToken);

        var response = await BuildResponse<T>(responseMonitor, nameof(PostAsync), isGenericMethod);
        return response;
    }

    public async Task<HttpResponse<T>> PostAsync<T>(HttpUrlEncodedFormRequest request, CancellationToken? cancellationToken = null) => await PostAsync<T>(request, true, cancellationToken);

    public async Task<HttpResponse> PostAsync(HttpUrlEncodedFormRequest request, CancellationToken? cancellationToken = null) => await PostAsync<string>(request, false, cancellationToken);

    async Task<HttpResponse<T>> PutAsync<T>(HttpJsonRequest request, bool isGenericMethod, CancellationToken? cancellationToken = null)
    {
        using var client = await CreateClientAsync(request);
        var url = BuildUrl(request);
        var responseMonitor = await Retry(client, () =>
        {
            var content = new StringContent(request.Parameters);
            var message = new HttpRequestMessage(HttpMethod.Put, url) { Content = content };
            return message;
        }, request, cancellationToken);

        var response = await BuildResponse<T>(responseMonitor, nameof(PutAsync), isGenericMethod);
        return response;
    }

    public async Task<HttpResponse<T>> PutAsync<T>(HttpJsonRequest request, CancellationToken? cancellationToken = null) => await PutAsync<T>(request, true, cancellationToken);

    public async Task<HttpResponse> PutAsync(HttpJsonRequest request, CancellationToken? cancellationToken = null) => await PutAsync<string>(request, false, cancellationToken);

    async Task<HttpResponse<T>> DeleteAsync<T>(HttpKeyValueRequest request, bool isGenericMethod, CancellationToken? cancellationToken = null)
    {
        using var client = await CreateClientAsync(request);
        var url = BuildGetUrl(request);
        var responseMonitor = await Retry(client, () => new HttpRequestMessage(HttpMethod.Delete, url), request, cancellationToken);
        return await BuildResponse<T>(responseMonitor, nameof(DeleteAsync), isGenericMethod);
    }

    public async Task<HttpResponse<T>> DeleteAsync<T>(HttpKeyValueRequest request, CancellationToken? cancellationToken = null) => await DeleteAsync<T>(request, true, cancellationToken);

    public async Task<HttpResponse> DeleteAsync(HttpKeyValueRequest request, CancellationToken? cancellationToken = null) => await DeleteAsync<string>(request, false, cancellationToken);

    #region Private
    async Task<HttpClient> CreateClientAsync(HttpRequest request)
    {
        await Task.Yield();

        var handler = new HttpClientHandler { CookieContainer = new CookieContainer() };
        if (request.Cookies.NotNullOrEmpty())
        {
            handler.CookieContainer = new CookieContainer();
            foreach (var cookie in request.Cookies) handler.CookieContainer.Add(cookie);
        }

        var progressHanlder = new ProgressMessageHandler(handler);
        var client = new HttpClient(progressHanlder);

        if (request.OnSendProgress is not null)
        {
            var progress = new HttpProgress();
            progressHanlder.HttpStartSend += (_, _) => progress.Reset();
            progressHanlder.HttpSendProgress += (_, e) =>
            {
                progress.Total = e.TotalBytes ?? 0;
                progress.Transfered = e.BytesTransferred;
                request.OnSendProgress(progress);
            };
        }

        if (request.OnReceiveProgress is not null)
        {
            var progress = new HttpProgress();
            progressHanlder.HttpStartSend += (_, _) => progress.Reset();
            progressHanlder.HttpReceiveProgress += (_, e) =>
            {
                progress.Total = e.TotalBytes ?? 0;
                progress.Transfered = e.BytesTransferred;
                request.OnReceiveProgress(progress);
            };
        }

        if (request.UseEdgeUserAgent && !(request.Headers?.ContainsKey("User-Agent") ?? false))
        {
            request.Headers ??= new Dictionary<string, IEnumerable<string>>();
            request.Headers.Add("User-Agent", new string[] { _edgeUA });
        }

        if (request.Headers.NotNullOrEmpty())
        {
            foreach (var header in request.Headers)
            {
                if (header.Value.NotNullOrEmpty()) client.DefaultRequestHeaders.Add(header.Key, header.Value);
            }
        }

        client.Timeout = request.TimeOut ?? HttpGlobalOptions.TimeOut ?? TimeSpan.FromDays(1);
        return client;
    }

    string BuildUrl(HttpRequest request)
    {
        if (request.Url.IsNullOrWhiteSpace()) return HttpGlobalOptions.BaseUrl ?? request.Url;
        else
        {
            if (!Uri.IsWellFormedUriString(request.Url, UriKind.Absolute) && HttpGlobalOptions.BaseUrl.NotNullOrWhiteSpace()) return HttpGlobalOptions.BaseUrl.CombinePath(request.Url);
        }
        return request.Url;
    }

    string BuildGetUrl(HttpKeyValueRequest request)
    {
        var url = BuildUrl(request);
        if (request.Parameters.IsNullOrEmpty()) return url;
        var prefix = url.Contains("?") ? "&" : "?";
        return $"{url}{prefix}{string.Join("&", request.Parameters.Select(x => $"{x.Key}={HttpUtility.UrlEncode(Encoding.UTF8.GetBytes(x.Value))}"))}";
    }

    async Task<ResponseMonitor> Retry(HttpClient client, Func<HttpRequestMessage> requestMessageBuilder, HttpRequest request, CancellationToken? cancellationToken = null)
    {
        var retryCount = request.RetryCount ?? HttpGlobalOptions.RetryCount ?? 0;
        var retryIndex = -1;
        var totalStartTime = DateTime.Now;
        HttpResponseMessage? response = null;
        Exception? exception = null;
        TimeSpan last;
        TimeSpan total = TimeSpan.Zero;
        string url = string.Empty;

        while (retryIndex < retryCount)
        {
            if (cancellationToken?.IsCancellationRequested ?? false) break;
            var requestMessage = requestMessageBuilder();
            url = requestMessage.RequestUri.AbsoluteUri;

            var startTime = DateTime.Now;
            retryIndex++;
            try
            {
                response = await client.SendAsync(requestMessage, HttpCompletionOption.ResponseContentRead, cancellationToken ?? CancellationToken.None);
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
        };
        return new ResponseMonitor(url, request, exception, retryIndex, last, total, response);
    }

    async Task<HttpResponse<T>> BuildResponse<T>(ResponseMonitor responseMonitor, string methodName, bool isGenericMethod = true)
    {
        var genericType = isGenericMethod ? typeof(T) : null;
        if (responseMonitor.ResponseMessage is null)
        {
            var errorResponse = new HttpResponse<T>(responseMonitor.Url, false, HttpStatusCode.ServiceUnavailable, responseMonitor.Exception?.Message ?? "no response", default, responseMonitor.RetryCount, responseMonitor.LastTimeConsuming, responseMonitor.TotalTimeConsuming);
            Log(responseMonitor.Exception, responseMonitor.Request, errorResponse, methodName, genericType);
            return errorResponse;
        }

        //set headers
        Dictionary<string, IEnumerable<string>>? headers = null;
        foreach (var header in responseMonitor.ResponseMessage.Headers)
        {
            headers ??= new();
            headers.Add(header.Key, header.Value);
        }

        // set cookies
        List<Cookie>? cookies = null;
        if (headers.NotNullOrEmpty() && headers.TryGetValue("Set-Cookie", out var cookieValues))
        {
            var host = new Uri(responseMonitor.Url).Host;
            foreach (var item in cookieValues)
            {
                cookies ??= new();
                var cookie = item.ParseCookie(host);
                if (cookie is not null) cookies.Add(cookie);
            }
        }

        var content = responseMonitor.ResponseMessage.Content;
        T? data = default;
        if (content is not null)
        {
            var responseText = await content.ReadAsStringAsync();
            if (responseText.NotNullOrWhiteSpace())
            {
                if (isGenericMethod && responseMonitor.ResponseMessage.IsSuccessStatusCode)
                {
                    var type = typeof(T);
                    if (type.IsClass)
                    {
                        if (type == typeof(string)) data = (T)Convert.ChangeType(responseText, type);
                        else
                        {
                            var obj = responseText.DeSerialize(type);
                            if (obj is not null) data = (T)obj;
                        }
                    }
                    else
                    {
                        data = (T)Convert.ChangeType(responseText, type);
                    }
                }
            }
        }

        var response = new HttpResponse<T>(responseMonitor.Url, responseMonitor.ResponseMessage.IsSuccessStatusCode, responseMonitor.ResponseMessage.StatusCode, responseMonitor.Exception?.Message ?? "empty response", data, headers, cookies, responseMonitor.RetryCount, responseMonitor.LastTimeConsuming, responseMonitor.TotalTimeConsuming);
        Log(responseMonitor.Exception, responseMonitor.Request, response, methodName, genericType);
        return response;
    }

    void Log(Exception? ex, HttpRequest request, HttpResponse? response, string methodName, Type? genericType = null)
    {
        var builder = new StringBuilder();
        builder.AppendLine("HttpService." + methodName + (genericType is not null ? $"<{genericType.GetTypeDefinitionName()}>" : ""));
        if (ex is not null) builder.AppendLine(ex.Message);
        if (request is not null) builder.AppendLine(request.ToString());
        if (response is not null) builder.AppendLine(response.ToString());
        var message = builder.ToString();

        if (_logger is not null)
        {
            if (ex is not null) _logger.LogError(ex, message);
            else _logger.LogTrace(ex, message);
        }
        else Debug.WriteLine(message);
    }

    class ResponseMonitor
    {
        public ResponseMonitor(string url, HttpRequest request, Exception? exception, int retryCount, TimeSpan lastTimeConsuming, TimeSpan totalTimeConsuming, HttpResponseMessage? responseMessage)
        {
            Url = url;
            Request = request;
            Exception = exception;
            RetryCount = retryCount;
            LastTimeConsuming = lastTimeConsuming;
            TotalTimeConsuming = totalTimeConsuming;
            ResponseMessage = responseMessage;
        }

        public string Url { get; }

        public Exception? Exception { get; }

        public int RetryCount { get; }

        public TimeSpan LastTimeConsuming { get; }

        public TimeSpan TotalTimeConsuming { get; }

        public HttpResponseMessage? ResponseMessage { get; }

        public HttpRequest Request { get; }
    }
    #endregion
}