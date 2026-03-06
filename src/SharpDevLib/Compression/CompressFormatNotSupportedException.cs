using SharpDevLib.Compression.Internal;

namespace SharpDevLib;

/// <summary>
/// 压缩格式不支持异常，当尝试使用不支持的格式进行压缩时抛出
/// </summary>
/// <remarks>
/// 使用指定的文件扩展名实例化异常
/// </remarks>
/// <param name="extension">不支持的文件扩展名</param>
public class CompressFormatNotSupportedException(string extension) : NotSupportedException($"extension '{extension}' not supported,supported format is '{string.Join(",", InternalCompressionExtension.SupportedCompressExtensions)}'")
{
    /// <summary>
    /// 获取所有支持的压缩格式列表
    /// </summary>
    public List<string> SupportedFormats => InternalCompressionExtension.SupportedCompressExtensions;

    /// <summary>
    /// 获取不支持的文件扩展名
    /// </summary>
    public string Extension { get; } = extension;
}