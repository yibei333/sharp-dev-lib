using SharpDevLib.Compression.Internal;

namespace SharpDevLib;

/// <summary>
/// 解压选项
/// </summary>
/// <remarks>
/// 实例化解压选项
/// </remarks>
/// <param name="sourceFile">压缩文件路径</param>
/// <param name="targetPath">要解压到的目标路径</param>
public class DeCompressRequest(string sourceFile, string targetPath) : CompressionRequest(targetPath)
{
    /// <summary>
    /// 压缩文件路径
    /// </summary>
    public string SourceFile { get; set; } = sourceFile;

    /// <summary>
    /// 解压文件格式
    /// </summary>
    public CompressionFormat Format => SourceFile.GetDecompressFormat();
}