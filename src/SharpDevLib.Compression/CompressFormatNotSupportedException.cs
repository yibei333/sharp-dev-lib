using SharpDevLib.Compression.Internal.References;

namespace SharpDevLib.Compression;

/// <summary>
/// 压缩不支持的格式异常
/// </summary>
public class CompressFormatNotSupportedException : NotSupportedException
{
    /// <summary>
    /// 实例化异常
    /// </summary>
    /// <param name="extension">文件扩展名</param>
    public CompressFormatNotSupportedException(string extension) : base($"extension '{extension}' not supported,supported format is '{string.Join(",", InternalCompressionExtension.SupportedCompressExtensions)}'")
    {
        Extension = extension;
    }

    /// <summary>
    /// 支持的格式
    /// </summary>
    public List<string> SupportedFormats => InternalCompressionExtension.SupportedCompressExtensions;

    /// <summary>
    /// 文件扩展名
    /// </summary>
    public string Extension { get; }
}