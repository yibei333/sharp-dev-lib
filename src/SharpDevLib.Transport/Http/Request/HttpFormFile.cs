namespace SharpDevLib.Transport;

/// <summary>
/// 表单文件
/// </summary>
public class HttpFormFile
{
    /// <summary>
    /// 实例化表单文件
    /// </summary>
    /// <param name="parameterName">参数名称</param>
    /// <param name="fileName">文件名</param>
    /// <param name="bytes">文件字节数组</param>
    public HttpFormFile(string parameterName, string fileName, byte[] bytes)
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
    public HttpFormFile(string parameterName, string fileName, Stream stream)
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