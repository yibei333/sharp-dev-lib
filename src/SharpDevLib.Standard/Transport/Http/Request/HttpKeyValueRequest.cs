namespace SharpDevLib.Standard;

/// <summary>
/// 键值对请求
/// </summary>
public class HttpKeyValueRequest : HttpRequest<Dictionary<string, string>>
{
    /// <summary>
    /// 实例化键值对请求
    /// </summary>
    /// <param name="url">请求地址</param>
    public HttpKeyValueRequest(string url) : base(url)
    {
    }

    /// <summary>
    /// 实例化键值对请求
    /// </summary>
    /// <param name="url">请求地址</param>
    /// <param name="keyValues">键值对集合</param>
    public HttpKeyValueRequest(string url, Dictionary<string, string> keyValues) : base(url, keyValues)
    {
    }
}
