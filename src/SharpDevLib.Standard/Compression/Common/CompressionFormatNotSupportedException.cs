namespace SharpDevLib.Standard;

/// <summary>
/// 压缩/解压不支持的格式异常
/// </summary>
public class CompressionFormatNotSupportedException : NotSupportedException
{
    /// <summary>
    /// 实例化异常
    /// </summary>
    /// <param name="extension">文件扩展名</param>
    public CompressionFormatNotSupportedException(string extension) : base($"extension '{extension}' not supported,supported format is '{string.Join(",", InternalCompressionExtension.SupportedExtensions)}'")
    {
        Extension = extension;
    }

    /// <summary>
    /// 支持的格式
    /// </summary>
    public List<string> SupportedFormats => InternalCompressionExtension.SupportedExtensions;

    /// <summary>
    /// 文件扩展名
    /// </summary>
    public string Extension { get; }
}