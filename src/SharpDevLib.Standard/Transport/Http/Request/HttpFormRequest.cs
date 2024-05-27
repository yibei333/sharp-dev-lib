using System.Text;

namespace SharpDevLib.Standard;

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
    public HttpMultiPartFormDataRequest(string url, FormFile[] files) : base(url)
    {
        Files = files;
    }

    /// <summary>
    /// 实例化表单请求
    /// </summary>
    /// <param name="url">请求地址</param>
    /// <param name="parameters">请求表单参数</param>
    /// <param name="files">文件集合</param>
    public HttpMultiPartFormDataRequest(string url, Dictionary<string, string> parameters, FormFile[] files) : base(url, parameters)
    {
        Files = files;
    }

    /// <summary>
    /// 文件集合
    /// </summary>
    public FormFile[]? Files { get; }

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
            builder.AppendLine($"Files:{Files.Select(x => new { x.ParameterName, x.FileName, x.Size, BytesIsNull = x.Bytes is null, StreamIsNull = x.Stream is null }).Serialize()}");
        }
        return builder.ToString();
    }
}

/// <summary>
/// 表单文件
/// </summary>
public class FormFile
{
    /// <summary>
    /// 实例化表单文件
    /// </summary>
    /// <param name="parameterName">参数名称</param>
    /// <param name="fileName">文件名</param>
    /// <param name="bytes">文件字节数组</param>
    public FormFile(string parameterName, string fileName, byte[] bytes)
    {
        ParameterName = parameterName;
        FileName = fileName;
        Bytes = bytes;
        Size = bytes.Length;
    }

    /// <summary>
    /// 实例化表单文件
    /// </summary>
    /// <param name="parameterName">参数名称</param>
    /// <param name="fileName">文件名</param>
    /// <param name="stream">文件流</param>
    public FormFile(string parameterName, string fileName, Stream stream)
    {
        ParameterName = parameterName;
        FileName = fileName;
        Size = stream.Length;
        if (stream.CanSeek) stream.Seek(0, SeekOrigin.Begin);
        Stream = stream;
    }

    /// <summary>
    /// 参数名称
    /// </summary>
    public string ParameterName { get; }

    /// <summary>
    /// 文件名
    /// </summary>
    public string FileName { get; }

    /// <summary>
    /// 文件字节数组
    /// </summary>
    public byte[]? Bytes { get; }

    /// <summary>
    /// 文件流
    /// </summary>
    public Stream? Stream { get; }

    /// <summary>
    /// 大小
    /// </summary>
    public long Size { get; }
}