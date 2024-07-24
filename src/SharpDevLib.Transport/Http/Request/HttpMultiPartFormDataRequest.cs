using System.Text;

namespace SharpDevLib.Transport;

/// <summary>
/// multipart/form-data表单请求
/// </summary>
public class HttpMultiPartFormDataRequest : HttpRequest<Dictionary<string, string>>
{
    /// <summary>
    /// 实例化表单请求
    /// </summary>
    /// <param name="url">请求地址</param>
    public HttpMultiPartFormDataRequest(string url) : base(url)
    {
    }

    /// <summary>
    /// 实例化表单请求
    /// </summary>
    /// <param name="url">请求地址</param>
    /// <param name="parameters">请求表单参数</param>
    public HttpMultiPartFormDataRequest(string url, Dictionary<string, string> parameters) : base(url, parameters)
    {
    }

    /// <summary>
    /// 实例化表单请求
    /// </summary>
    /// <param name="url">请求地址</param>
    /// <param name="files">文件集合</param>
    public HttpMultiPartFormDataRequest(string url, HttpFormFile[] files) : base(url)
    {
        Files = files;
    }

    /// <summary>
    /// 实例化表单请求
    /// </summary>
    /// <param name="url">请求地址</param>
    /// <param name="parameters">请求表单参数</param>
    /// <param name="files">文件集合</param>
    public HttpMultiPartFormDataRequest(string url, Dictionary<string, string> parameters, HttpFormFile[] files) : base(url, parameters)
    {
        Files = files;
    }

    /// <summary>
    /// 文件集合
    /// </summary>
    public HttpFormFile[]? Files { get; }

    /// <summary>
    /// 将请求转换为字符串,用于记录日志
    /// </summary>
    /// <returns>字符串</returns>
    public override string ToString()
    {
        var builder = new StringBuilder();
        builder.Append(base.ToString());
        if (Files is null)
        {
            builder.AppendLine($"Files:null");
        }
        else
        {
            foreach (var file in Files)
            {
                builder.AppendLine($"ParameterName={file.ParameterName},FileName={file.FileName},Size={file.Size},BytesIsNull={file.Bytes is null},StreamIsNull={file.Stream is null}");
            }
        }
        return builder.ToString();
    }
}