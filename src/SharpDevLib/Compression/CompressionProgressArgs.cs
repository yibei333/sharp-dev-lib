namespace SharpDevLib;

/// <summary>
/// 压缩/解压进度参数，用于报告压缩或解压操作的进度信息
/// </summary>
public class CompressionProgressArgs
{
    internal CompressionProgressArgs()
    {

    }

    /// <summary>
    /// 获取当前正在处理的文件名称
    /// </summary>
    public string? CurrentName { get; internal set; }

    /// <summary>
    /// 获取需要处理的总字节数
    /// </summary>
    public double Total { get; internal set; }

    /// <summary>
    /// 获取已处理的字节数
    /// </summary>
    public double Trasnsfed { get; internal set; }

    /// <summary>
    /// 获取进度百分比，范围从0到100
    /// </summary>
    public double Progress => Total <= 0 ? 0 : (Trasnsfed >= Total ? 1 : Math.Round(Trasnsfed * 1.0 / Total * 100, 2));

    /// <summary>
    /// 获取进度文本，包含百分号
    /// </summary>
    public string ProgressText => $"{Progress}%";
}
