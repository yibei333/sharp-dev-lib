using SharpDevLib.Compression.Internal;

namespace SharpDevLib;

/// <summary>
/// 压缩请求，用于配置文件或目录的压缩操作
/// </summary>
/// <remarks>
/// 使用源路径和目标路径示例化压缩请求
/// </remarks>
/// <param name="sourcePaths">要压缩的文件或目录路径集合</param>
/// <param name="targetPath">保存压缩文件的目标路径</param>
public class CompressRequest(List<string> sourcePaths, string targetPath) : CompressionRequest(targetPath)
{

    /// <summary>
    /// 获取要压缩的文件或目录路径集合
    /// </summary>
    public List<string> SourcePaths { get; } = sourcePaths;

    /// <summary>
    /// 获取或设置是否在压缩时包含目录的名称结构，默认为false
    /// </summary>
    public bool IncludeSourceDiretory { get; set; }

    /// <summary>
    /// 获取或设置压缩级别，默认为Normal
    /// </summary>
    public CompressionLevel Level { get; set; } = CompressionLevel.Normal;

    /// <summary>
    /// 获取根据目标路径自动推断的压缩文件格式
    /// </summary>
    public CompressionFormat Format => TargetPath.GetComopressFormat();
}
