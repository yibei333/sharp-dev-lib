using SharpDevLib.Compression.Internal;

namespace SharpDevLib;

/// <summary>
/// 解压不支持的格式异常
/// </summary>
/// <remarks>
/// 实例化异常
/// </remarks>
/// <param name="extension">文件扩展名</param>
public class DeCompressFormatNotSupportedException(string extension) : NotSupportedException($"extension '{extension}' not supported,supported format is '{string.Join(",", InternalCompressionExtension.SupportedDeCompressExtensions)}'")
{
    /// <summary>
    /// 支持的格式
    /// </summary>
    public List<string> SupportedFormats => InternalCompressionExtension.SupportedDeCompressExtensions;

    /// <summary>
    /// 文件扩展名
    /// </summary>
    public string Extension { get; } = extension;
}