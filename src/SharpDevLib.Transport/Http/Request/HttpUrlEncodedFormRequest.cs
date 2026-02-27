namespace SharpDevLib.Transport;

/// <summary>
/// x-www-form-urlencoded表单请求
/// </summary>
/// <remarks>
/// 实例化表单请求
/// </remarks>
/// <param name="url">请求地址</param>
/// <param name="parameters">请求表单参数</param>
public class HttpUrlEncodedFormRequest(string url, Dictionary<string, string> parameters) : HttpRequest<Dictionary<string, string>>(url, parameters)
{
}