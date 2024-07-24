namespace SharpDevLib.Transport;

/// <summary>
/// x-www-form-urlencoded表单请求
/// </summary>
public class HttpUrlEncodedFormRequest : HttpRequest<Dictionary<string, string>>
{
    /// <summary>
    /// 实例化表单请求
    /// </summary>
    /// <param name="url">请求地址</param>
    /// <param name="parameters">请求表单参数</param>
    public HttpUrlEncodedFormRequest(string url, Dictionary<string, string> parameters) : base(url, parameters)
    {
    }
}