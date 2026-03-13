using SharpDevLib.Compression.Internal;

namespace SharpDevLib;

/// <summary>
/// 解压请求，用于配置压缩文件的解压操作
/// </summary>
/// <remarks>
/// 使用源压缩文件和目标路径示例化解压请求
/// </remarks>
/// <param name="sourceFile">要解压的压缩文件路径</param>
/// <param name="targetPath">解压文件保存的目标路径</param>
public class DeCompressRequest(string sourceFile, string targetPath) : CompressionRequest(targetPath)
{
    /// <summary>
    /// 获取或设置要解压的压缩文件路径
    /// </summary>
    public string SourceFile { get; set; } = sourceFile;

    /// <summary>
    /// 获取根据源文件自动推断的解压文件格式
    /// </summary>
    public CompressionFormat Format => SourceFile.GetDecompressFormat();
}