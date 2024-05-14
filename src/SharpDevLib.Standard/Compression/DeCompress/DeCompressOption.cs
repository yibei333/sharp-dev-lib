namespace SharpDevLib.Standard;

/// <summary>
/// 解压选项
/// </summary>
public class DeCompressOption
{
    /// <summary>
    /// 实例化解压选项
    /// </summary>
    /// <param name="sourceFile">压缩文件路径</param>
    /// <param name="targetPath">要解压到的目标路径</param>
    public DeCompressOption(string sourceFile, string targetPath)
    {
        SourceFile = sourceFile;
        TargetPath = targetPath;
    }

    /// <summary>
    /// 压缩文件路径
    /// </summary>
    public string SourceFile { get; set; }

    /// <summary>
    /// 要解压到的目标路径
    /// </summary>
    public string TargetPath { get; set; }

    /// <summary>
    /// 密码
    /// </summary>
    public string? Password { get; set; }

    /// <summary>
    /// 压缩级别
    /// </summary>
    public CompressionLevel Level { get; set; } = CompressionLevel.Normal;

    /// <summary>
    /// CancellationToken
    /// </summary>
    public CancellationToken CancellationToken { get; set; } = Statics.CancellationToken;

    /// <summary>
    /// 进度变化回调
    /// </summary>
    public Action<CompressionProgressArgs>? OnProgress { get; set; }

    /// <summary>
    /// 压缩文件格式
    /// </summary>
    public CompressionFormat Format => TargetPath.GetFormatByName();
}