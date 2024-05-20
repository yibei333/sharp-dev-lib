using System.Net;
using System.Text;

namespace SharpDevLib.Standard;

/// <summary>
/// http请求
/// </summary>
public abstract class HttpRequest
{
    /// <summary>
    /// 实例化http请求
    /// </summary>
    /// <param name="url">请求地址</param>
    public HttpRequest(string url)
    {
        Url = url.UrlDecode().ToUtf8String();
    }

    /// <summary>
    /// 超时时间
    /// </summary>
    public TimeSpan? TimeOut { get; set; }

    /// <summary>
    /// 重试次数
    /// </summary>
    public int? RetryCount { get; set; }

    /// <summary>
    /// 请求地址
    /// </summary>
    public string Url { get; }

    /// <summary>
    /// 请求头
    /// </summary>
    public Dictionary<string, IEnumerable<string>>? Headers { get; set; }

    /// <summary>
    /// 请求Cookie
    /// </summary>
    public List<Cookie>? Cookies { get; set; }

    /// <summary>
    /// 接收数据回调
    /// </summary>
    public Action<HttpProgress>? OnReceiveProgress { get; set; }

    /// <summary>
    /// 传入数据回调
    /// </summary>
    public Action<HttpProgress>? OnSendProgress { get; set; }

    /// <summary>
    /// 将请求转换为字符串,用于记录日志
    /// </summary>
    /// <returns>字符串</returns>
    public override string ToString()
    {
        var builder = new StringBuilder();
        builder.AppendLine($"********request********");
        builder.AppendLine($"Url:{Url}");
        builder.AppendLine($"TimeOut:{TimeOut}");
        builder.AppendLine($"RetryCount:{RetryCount}");
        builder.AppendLine($"Headers:{Headers?.Serialize()}");
        builder.AppendLine($"Cookies:{Cookies?.Select(x => new { x.Name, x.Value }).Serialize()}");
        builder.AppendLine($"OnReceiveProgress is null:{OnReceiveProgress is null}");
        builder.AppendLine($"OnSendProgress is null:{OnSendProgress is null}");
        return builder.ToString();
    }
}

/// <summary>
/// http请求
/// </summary>
/// <typeparam name="TParameters">请求参数类型</typeparam>
public abstract class HttpRequest<TParameters> : HttpRequest
{
    /// <summary>
    /// 实例化http请求
    /// </summary>
    /// <param name="url">请求地址</param>
    public HttpRequest(string url) : base(url)
    {
    }

    /// <summary>
    /// 实例化http请求
    /// </summary>
    /// <param name="url">请求地址</param>
    /// <param name="parameters">请求参数</param>
    public HttpRequest(string url, TParameters parameters) : this(url)
    {
        Parameters = parameters;
    }

    /// <summary>
    /// 请求参数
    /// </summary>
    public TParameters? Parameters { get; }

    /// <summary>
    /// 将请求转换为字符串,用于记录日志
    /// </summary>
    /// <returns>字符串</returns>
    public override string ToString()
    {
        var builder = new StringBuilder();
        builder.Append(base.ToString());
        if (Parameters is null)
        {
            builder.AppendLine($"Parameters:null");
        }
        else
        {
            var type = typeof(TParameters);
            if (type.IsClass) builder.AppendLine($"Parameters:{(Parameters.TrySerialize(out var res) ? res : Parameters.ToString())}");
            else builder.AppendLine($"Parameters:{Parameters}");
        }
        return builder.ToString();
    }
}
