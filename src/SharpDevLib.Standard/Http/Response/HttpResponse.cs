using System.Net;
using System.Text;

namespace SharpDevLib.Standard;

/// <summary>
/// http响应
/// </summary>
public class HttpResponse
{
    /// <summary>
    /// 实例化http响应
    /// </summary>
    /// <param name="url">请求地址</param>
    /// <param name="isSuccess">请求是否成功</param>
    /// <param name="code">http状态码</param>
    /// <param name="message">消息</param>
    /// <param name="headers">响应头</param>
    /// <param name="cookies">cookie集合</param>
    /// <param name="retryCount">重试次数</param>
    /// <param name="timeConsuming">耗时</param>
    internal HttpResponse(string url, bool isSuccess, HttpStatusCode code, string message, Dictionary<string, string> headers, List<Cookie> cookies, int retryCount, TimeSpan timeConsuming)
    {
        Url = url;
        IsSuccess = isSuccess;
        Code = code;
        Message = message;
        Headers = headers;
        RetryCount = retryCount;
        TimeConsuming = timeConsuming;
        Cookies = cookies;
    }

    /// <summary>
    /// 请求地址
    /// </summary>
    public string Url { get; set; }

    /// <summary>
    /// 请求是否成功
    /// </summary>
    public bool IsSuccess { get; }

    /// <summary>
    /// http状态码
    /// </summary>
    public HttpStatusCode Code { get; }

    /// <summary>
    /// 消息
    /// </summary>
    public string Message { get; }

    /// <summary>
    /// 响应头
    /// </summary>
    public Dictionary<string, string> Headers { get; }

    /// <summary>
    /// 重试次数
    /// </summary>
    public int RetryCount { get; }

    /// <summary>
    /// 耗时
    /// </summary>
    public TimeSpan TimeConsuming { get; }

    /// <summary>
    /// cookie集合
    /// </summary>
    public List<Cookie> Cookies { get; }

    /// <summary>
    /// 创建失败响应
    /// </summary>
    /// <param name="url">请求地址</param>
    /// <param name="code">http状态码</param>
    /// <param name="message">消息</param>
    /// <returns>http响应</returns>
    public static HttpResponse Failed(string url, HttpStatusCode code, string message)
    {
        return new HttpResponse(url, false, code, message, new(), new(), 0, TimeSpan.Zero);
    }

    /// <summary>
    /// 创建失败响应
    /// </summary>
    /// <typeparam name="T">数据类型</typeparam>
    /// <param name="url">请求地址</param>
    /// <param name="code">http状态码</param>
    /// <param name="message">消息</param>
    /// <returns>http响应</returns>
    public static HttpResponse Failed<T>(string url, HttpStatusCode code, string message)
    {
        return new HttpResponse<T>(url, false, code, message, default, new(), new(), 0, TimeSpan.Zero);
    }

    /// <summary>
    /// 创建成功响应
    /// </summary>
    /// <param name="url">请求地址</param>
    /// <param name="message">消息</param>
    /// <returns>http响应</returns>
    public static HttpResponse Succeed(string url, string? message = null)
    {
        return new HttpResponse(url, true, HttpStatusCode.OK, message ?? string.Empty, new(), new(), 0, TimeSpan.Zero);
    }

    /// <summary>
    /// 创建成功响应
    /// </summary>
    /// <typeparam name="T">数据类型</typeparam>
    /// <param name="url">请求地址</param>
    /// <param name="data">数据</param>
    /// <param name="message">消息</param>
    /// <returns>http响应</returns>
    public static HttpResponse Succeed<T>(string url, T? data, string? message = null)
    {
        return new HttpResponse<T>(url, true, HttpStatusCode.OK, message ?? string.Empty, data, new(), new(), 0, TimeSpan.Zero);
    }

    /// <summary>
    /// 将响应转换为字符串,用于记录日志
    /// </summary>
    /// <returns>字符串</returns>
    public override string ToString()
    {
        var builder = new StringBuilder();
        builder.AppendLine($"********response********");
        builder.AppendLine($"Url:{Url}");
        builder.AppendLine($"IsSuccess:{IsSuccess}");
        builder.AppendLine($"Code:{Code}");
        builder.AppendLine($"Message:{Message}");
        builder.AppendLine($"RetryCount:{RetryCount}");
        builder.AppendLine($"TimeConsuming:{TimeConsuming.TotalMilliseconds}ms");
        builder.AppendLine($"Headers:{Headers?.Serialize()}");
        builder.AppendLine($"Cookies:{Cookies?.Select(x => new { x.Name, x.Value }).Serialize()}");
        return builder.ToString();
    }
}

/// <summary>
/// http响应
/// </summary>
public class HttpResponse<T> : HttpResponse
{

    /// <summary>
    /// 实例化http响应
    /// </summary>
    /// <param name="url">请求地址</param>
    /// <param name="isSuccess">请求是否成功</param>
    /// <param name="code">http状态码</param>
    /// <param name="message">消息</param>
    /// <param name="data">数据</param>
    /// <param name="headers">响应头</param>
    /// <param name="cookies">cookie集合</param>
    /// <param name="retryCount">重试次数</param>
    /// <param name="timeConsuming">耗时</param>
    internal HttpResponse(string url, bool isSuccess, HttpStatusCode code, string message, T? data, Dictionary<string, string> headers, List<Cookie> cookies, int retryCount, TimeSpan timeConsuming) : base(url, isSuccess, code, message, headers, cookies, retryCount, timeConsuming)
    {
        Data = data;
    }

    /// <summary>
    /// 数据
    /// </summary>
    public T? Data { get; }

    /// <summary>
    /// 将请求转换为字符串,用于记录日志
    /// </summary>
    /// <returns>字符串</returns>
    public override string ToString()
    {
        var builder = new StringBuilder();
        builder.Append(base.ToString());
        if (Data is null)
        {
            builder.AppendLine($"Data:null");
        }
        else
        {
            var type = typeof(T);
            if (type.IsClass) builder.AppendLine($"Data:{(Data.TrySerialize(out var res) ? res : Data.ToString())}");
            else builder.AppendLine($"Data:{Data}");
        }
        return builder.ToString();
    }
}