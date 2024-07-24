namespace SharpDevLib.Compression;

/// <summary>
/// 压缩/解压进度参数
/// </summary>
public class CompressionProgressArgs
{
    internal CompressionProgressArgs()
    {

    }

    /// <summary>
    /// 当前处理的文件名称
    /// </summary>
    public string? CurrentName { get; internal set; }

    /// <summary>
    /// 总字节数
    /// </summary>
    public double Total { get; internal set; }

    /// <summary>
    /// 已处理的字节数
    /// </summary>
    public double Handled { get; internal set; }

    /// <summary>
    /// 进度(%)
    /// </summary>
    public double Progress => Total <= 0 ? 0 : Math.Round(Handled * 1.0 / Total * 100, 2);

    /// <summary>
    /// 进度字符串(加了%)
    /// </summary>
    public string ProgressText => $"{Progress}%";
}
