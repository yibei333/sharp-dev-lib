namespace SharpDevLib.Transport;

/// <summary>
/// json请求
/// </summary>
public class HttpJsonRequest : HttpRequest<string>
{
    /// <summary>
    /// 实例化json请求
    /// </summary>
    /// <param name="url">请求地址</param>
    public HttpJsonRequest(string url) : base(url)
    {
    }

    /// <summary>
    /// 实例化json请求
    /// </summary>
    /// <param name="url">请求地址</param>
    /// <param name="json">json</param>
    public HttpJsonRequest(string url, string json) : base(url, json)
    {
    }
}