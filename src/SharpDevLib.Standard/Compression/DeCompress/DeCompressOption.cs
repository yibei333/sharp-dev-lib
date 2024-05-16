namespace SharpDevLib.Standard;

/// <summary>
/// 解压选项
/// </summary>
public class DeCompressOption : CompressionOption
{
    /// <summary>
    /// 实例化解压选项
    /// </summary>
    /// <param name="sourceFile">压缩文件路径</param>
    /// <param name="targetPath">要解压到的目标路径</param>
    public DeCompressOption(string sourceFile, string targetPath) : base(targetPath)
    {
        SourceFile = sourceFile;
    }

    /// <summary>
    /// 压缩文件路径
    /// </summary>
    public string SourceFile { get; set; }

    /// <summary>
    /// 解压文件格式
    /// </summary>
    public CompressionFormat Format => SourceFile.GetFormatByName();
}