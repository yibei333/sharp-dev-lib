namespace SharpDevLib.Standard;

/// <summary>
/// 压缩选项
/// </summary>
public class CompressOption
{
    /// <summary>
    /// 实例化压缩选项
    /// </summary>
    /// <param name="sourcePaths">路径集合,可以是目录也可以是文件路径</param>
    /// <param name="targetPath">保存目标路径</param>
    public CompressOption(List<string> sourcePaths, string targetPath)
    {
        SourcePaths = sourcePaths;
        TargetPath = targetPath;
    }

    /// <summary>
    /// 路径集合,可以是目录也可以是文件路径
    /// </summary>
    public List<string> SourcePaths { get; }

    /// <summary>
    /// 如果SourcePath中的是目录,是否要包含目录的名称结构,默认为false
    /// </summary>
    public bool IncludeSourceDiretory { get; set; }

    /// <summary>
    /// 保存目标路径
    /// </summary>
    public string TargetPath { get; }

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
