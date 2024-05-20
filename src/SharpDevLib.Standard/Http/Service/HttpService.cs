using System.Net;
using System.Text.Json;

namespace SharpDevLib.Standard;

internal class HttpService : IHttpService
{
    public async Task<HttpResponse<T>> GetAsync<T>(HttpKeyValueRequest request, CancellationToken? cancellationToken = null)
    {
        using var client = CreateClient(request);
        var url = BuildGetUrl(request);
        var responseMonitor = await Retry(client, () => new HttpRequestMessage(HttpMethod.Get, url), request, cancellationToken);
        var response = await BuildResponse<T>(url, responseMonitor);
        return response;
    }

    public async Task<HttpResponse> GetAsync(HttpKeyValueRequest request, CancellationToken? cancellationToken = null)
    {
        using var client = CreateClient(request);
        var url = BuildGetUrl(request);
        var responseMonitor = await Retry(client, () => new HttpRequestMessage(HttpMethod.Get, url), request, cancellationToken);
        var response = await BuildResponse(url, responseMonitor);
        return response;
    }

    public Task<Stream> GetStreamAsync(HttpKeyValueRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<HttpResponse<T>> PostAsync<T>(HttpJsonRequest request, CancellationToken? cancellationToken = null)
    {
        throw new NotImplementedException();
    }

    public Task<HttpResponse> PostAsync(HttpJsonRequest request, CancellationToken? cancellationToken = null)
    {
        throw new NotImplementedException();
    }

    public Task<HttpResponse<T>> PostAsync<T>(HttpMultiPartFormDataRequest request, CancellationToken? cancellationToken = null)
    {
        throw new NotImplementedException();
    }

    public Task<HttpResponse> PostAsync(HttpMultiPartFormDataRequest request, CancellationToken? cancellationToken = null)
    {
        throw new NotImplementedException();
    }

    public Task<HttpResponse<T>> PostAsync<T>(HttpUrlEncodedFormRequest request, CancellationToken? cancellationToken = null)
    {
        throw new NotImplementedException();
    }

    public Task<HttpResponse> PostAsync(HttpUrlEncodedFormRequest request, CancellationToken? cancellationToken = null)
    {
        throw new NotImplementedException();
    }

    public Task<HttpResponse<T>> PutAsync<T>(HttpJsonRequest request, CancellationToken? cancellationToken = null)
    {
        throw new NotImplementedException();
    }

    public Task<HttpResponse> PutAsync(HttpJsonRequest request, CancellationToken? cancellationToken = null)
    {
        throw new NotImplementedException();
    }

    public Task<HttpResponse<T>> DeleteAsync<T>(HttpKeyValueRequest request, CancellationToken? cancellationToken = null)
    {
        throw new NotImplementedException();
    }

    public Task<HttpResponse> DeleteAsync(HttpKeyValueRequest request, CancellationToken? cancellationToken = null)
    {
        throw new NotImplementedException();
    }

    HttpClient CreateClient(HttpRequest request)
    {
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

        if (request.Headers.NotNullOrEmpty())
        {
            foreach (var header in request.Headers)
            {
                if (header.Value.NotNullOrEmpty()) client.DefaultRequestHeaders.Add(header.Key, header.Value);
            }
        }
        client.Timeout = request.TimeOut ?? HttpGlobalSettings.TimeOut ?? TimeSpan.FromDays(1);
        return client;
    }

    string BuildUrl(HttpRequest request)
    {
        if (request.Url.IsNullOrWhiteSpace()) return HttpGlobalSettings.BaseUrl ?? request.Url;
        else
        {
            if (!Uri.IsWellFormedUriString(request.Url, UriKind.Absolute) && HttpGlobalSettings.BaseUrl.NotNullOrWhiteSpace()) return HttpGlobalSettings.BaseUrl.CombinePath(request.Url);
        }
        return request.Url;
    }

    string BuildGetUrl(HttpKeyValueRequest request)
    {
        var url = BuildUrl(request);
        if (request.Parameters.IsNullOrEmpty()) return url;
        var prefix = url.Contains("?") ? "&" : "?";
        return $"{url}{prefix}{string.Join("&", request.Parameters.Select(x => $"{x.Key}={x.Value.ToUtf8Bytes().UrlEncode()}"))}";
    }

    async Task<ResponseMonitor> Retry(HttpClient client, Func<HttpRequestMessage> requestMessageBuilder, HttpRequest request, CancellationToken? cancellationToken = null)
    {
        var retryCount = request.RetryCount ?? HttpGlobalSettings.RetryCount ?? 0;
        var retryIndex = -1;
        var totalStartTime = DateTime.Now;
        HttpResponseMessage? response = null;
        string? exceptionMessage = null;
        TimeSpan last;
        TimeSpan total = TimeSpan.Zero;

        while (retryIndex < retryCount)
        {
            if (cancellationToken?.IsCancellationRequested ?? false) break;
            var requestMessage = requestMessageBuilder();

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
                return new ResponseMonitor(null, retryIndex, last, endTime - totalStartTime, response);
            }
            catch (Exception ex)
            {
                last = DateTime.Now - startTime;
                total += last;
                exceptionMessage = ex.Message;
            }
        };
        return new ResponseMonitor(exceptionMessage, retryIndex, last, total, response);
    }

    private static async Task<HttpResponse> BuildResponse(string url, ResponseMonitor responseMonitor)
    {
        var response = await BuildResponse<string>(url, responseMonitor);
        response.Data = null;
        return response;
    }

    private static async Task<HttpResponse<T>> BuildResponse<T>(string url, ResponseMonitor responseMonitor)
    {
        if (responseMonitor.ResponseMessage is null) return new HttpResponse<T>(url, false, HttpStatusCode.ServiceUnavailable, responseMonitor.ExceptionMessage ?? "no response", default, responseMonitor.RetryCount, responseMonitor.LastTimeConsuming, responseMonitor.TotalTimeConsuming);

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
            var host = new Uri(url).Host;
            foreach (var item in cookieValues)
            {
                cookies ??= new();
                var cookie = item.ParseCookie(host);
                if (cookie is not null) cookies.Add(cookie);
            }
        }

        var content = responseMonitor.ResponseMessage.Content;
        var responseText = content is null ? "empty response" : await content.ReadAsStringAsync();
        T? data = default;

        if (responseMonitor.ResponseMessage.IsSuccessStatusCode)
        {
            var type = typeof(T);
            if (type.IsClass)
            {
                if (type == typeof(string)) data = (T)Convert.ChangeType(responseText, type);
                else data = JsonSerializer.Deserialize<T>(responseText);
            }
            else
            {
                data = (T)Convert.ChangeType(responseText, type);
            }
        }

        return new HttpResponse<T>(url, responseMonitor.ResponseMessage.IsSuccessStatusCode, responseMonitor.ResponseMessage.StatusCode, responseMonitor.ExceptionMessage ?? responseText, data, headers, cookies, responseMonitor.RetryCount, responseMonitor.LastTimeConsuming, responseMonitor.TotalTimeConsuming);
    }
}

class ResponseMonitor
{
    public ResponseMonitor(string? exceptionMessage, int retryCount, TimeSpan lastTimeConsuming, TimeSpan totalTimeConsuming, HttpResponseMessage? responseMessage)
    {
        ExceptionMessage = exceptionMessage;
        RetryCount = retryCount;
        LastTimeConsuming = lastTimeConsuming;
        TotalTimeConsuming = totalTimeConsuming;
        ResponseMessage = responseMessage;
    }

    public string? ExceptionMessage { get; }

    public int RetryCount { get; }

    public TimeSpan LastTimeConsuming { get; }

    public TimeSpan TotalTimeConsuming { get; }

    public HttpResponseMessage? ResponseMessage { get; }
}

#region old
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Logging;
//using Microsoft.Extensions.Options;
//using Newtonsoft.Json;
//using SharpDevLib.Extensions.Model;
//using System.Diagnostics;
//using System.Net;
//using System.Net.Http.Handlers;
//using System.Net.Http.Headers;

//namespace SharpDevLib.Standard;

//internal class HttpService : IHttpService
//{
//    private readonly HttpGlobalSettings? _globalOptions;
//    private readonly ILogger<HttpService>? _logger;

//    public HttpService(IServiceProvider serviceProvider)
//    {
//        _globalOptions = serviceProvider.GetService<IOptions<HttpGlobalSettings>>()?.Value;
//        _logger = serviceProvider.GetService<ILogger<HttpService>>();
//    }

//    public async Task<HttpResult<T>> GetAsync<T>(HttpKeyValueRequest option, CancellationToken? cancellationToken = null)
//    {
//        using var client = CreateClient(option);
//        var url = BuildUrl(option);
//        url = BuildGetUrl(url, option.Parameters);
//        if (typeof(T) == typeof(Stream)) throw new Exception($"Call GetStream Method Instead");
//        _logger?.LogInformation("start http get request:{url}", url);
//        return await Retry<T>(option, () => client.GetAsync(url, cancellationToken ?? new CancellationToken()));
//    }

//    public async Task<Stream> GetStreamAsync(HttpKeyValueRequest option)
//    {
//        using var client = CreateClient(option);
//        var url = BuildUrl(option);
//        url = BuildGetUrl(url, option.Parameters);
//        _logger?.LogInformation("start http get request:{url}", url);
//        return await client.GetStreamAsync(url);
//    }

//    public async Task<HttpResult<T>> PostAsync<T>(HttpJsonRequest option, CancellationToken? cancellationToken = null)
//    {
//        using var client = CreateClient(option);
//        var content = new StringContent(option.Parameters ?? "{}");
//        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
//        var url = BuildUrl(option);
//        _logger?.LogInformation("start http post json request:{url}", url);
//        return await Retry<T>(option, () => client.PostAsync(url, content, cancellationToken ?? new CancellationToken()));
//    }

//    public async Task<HttpResult<T>> PostFormAsync<T>(HttpFromRequest option, CancellationToken? cancellationToken = null)
//    {
//        using var client = CreateClient(option);
//        HttpContent content;
//        if (option.IsUrlEncoded)
//        {
//            content = new FormUrlEncodedContent(option.Parameters ?? new Dictionary<string, string>());
//        }
//        else
//        {
//            var formDataContent = new MultipartFormDataContent();
//            if (option.Parameters.NotNull())
//            {
//                foreach (var parameter in option.Parameters!)
//                {
//                    formDataContent.Add(new StringContent(parameter.Value), parameter.Key);
//                }
//            }
//            if (option.Files.NotEmpty())
//            {
//                foreach (var file in option.Files!)
//                {
//                    if (file.FileStream is not null) formDataContent.Add(new StreamContent(file.FileStream), file.ParameterName, file.FileName);
//                    else if (file.FileData is not null) formDataContent.Add(new ByteArrayContent(file.FileData), file.ParameterName, file.FileName);
//                }
//            }
//            content = formDataContent;
//        }
//        var url = BuildUrl(option);
//        _logger?.LogInformation("start http post form request:{url}", url);
//        return await Retry<T>(option, () => client.PostAsync(url, content, cancellationToken ?? new CancellationToken()));
//    }

//    public async Task<HttpResult<T>> PutAsync<T>(HttpJsonRequest option, CancellationToken? cancellationToken = null)
//    {
//        using var client = CreateClient(option);
//        var content = new StringContent(option.Parameters ?? "{}");
//        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
//        var url = BuildUrl(option);
//        _logger?.LogInformation("start http put request:{url}", url);
//        return await Retry<T>(option, () => client.PutAsync(url, content, cancellationToken ?? new CancellationToken()));
//    }

//    public async Task<HttpResult<T>> DeleteAsync<T>(HttpKeyValueRequest option, CancellationToken? cancellationToken = null)
//    {
//        using var client = CreateClient(option);
//        var url = BuildUrl(option);
//        url = BuildGetUrl(url, option.Parameters);
//        _logger?.LogInformation("start http delete request:{url}", url);
//        return await Retry<T>(option, () => client.DeleteAsync(url, cancellationToken ?? new CancellationToken()));
//    }

//    private HttpClient CreateClient(HttpRequest option)
//    {
//        var handler = new HttpClientHandler { CookieContainer = new CookieContainer() };
//        if (option.Cookies!.NotEmpty())
//        {
//            handler.CookieContainer = new CookieContainer();
//            foreach (var cookie in option.Cookies!)
//            {
//                handler.CookieContainer.Add(cookie);
//            }
//        }
//        var progressHanlder = new ProgressMessageHandler(handler);
//        if (option.OnSendProgress.NotNull())
//        {
//            var speed = new TransferFileStatisticsModel(DateTime.Now);
//            int lastProgress = 0;
//            progressHanlder.HttpSendProgress += (_, e) =>
//            {
//                var progress = new HttpProgress(e.TotalBytes ?? 0, e.BytesTransferred, speed);
//                if (progress.Progress > lastProgress)
//                {
//                    lastProgress = progress.Progress;
//                    option.OnSendProgress!.Invoke(progress);
//                }
//            };
//        }
//        if (option.OnReceiveProgress.NotNull())
//        {
//            var speed = new TransferFileStatisticsModel(DateTime.Now);
//            int lastProgress = 0;
//            progressHanlder.HttpReceiveProgress += (_, e) =>
//            {
//                var progress = new HttpProgress(e.TotalBytes ?? 0, e.BytesTransferred, speed);
//                if (progress.Progress > lastProgress)
//                {
//                    lastProgress = progress.Progress;
//                    option.OnReceiveProgress!.Invoke(progress);
//                }
//            };
//        }

//        var client = new HttpClient(progressHanlder);
//        if (option.Headers!.NotEmpty())
//        {
//            foreach (var header in option.Headers!)
//            {
//                if (!string.IsNullOrWhiteSpace(header.Value)) client.DefaultRequestHeaders.Add(header.Key, header.Value);
//            }
//        }
//        client.Timeout = option.TimeOut ?? _globalOptions?.TimeOut ?? TimeSpan.FromDays(1);
//        return client;
//    }


#endregion