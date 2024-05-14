namespace SharpDevLib.Standard;

/// <summary>
/// 压缩/解压进度参数
/// </summary>
public class CompressionProgressArgs
{
    /// <summary>
    /// 实例化参数
    /// </summary>
    /// <param name="currentName">当前处理的文件名称</param>
    /// <param name="totalCount">总数</param>
    /// <param name="handledCount">已处理的数量</param>
    public CompressionProgressArgs(string currentName, int totalCount, int handledCount)
    {
        CurrentName = currentName;
        TotalCount = totalCount;
        HandledCount = handledCount;
    }


    /// <summary>
    /// 当前处理的文件名称
    /// </summary>
    public string CurrentName { get; }

    /// <summary>
    /// 总数
    /// </summary>
    public int TotalCount { get; }

    /// <summary>
    /// 已处理的数量
    /// </summary>
    public int HandledCount { get; }

    /// <summary>
    /// 进度(%)
    /// </summary>
    public double Progress => TotalCount <= 0 ? 0 : Math.Round(HandledCount * 1.0 / TotalCount * 100, 2);

    /// <summary>
    /// 进度字符串(加了%)
    /// </summary>
    public string ProgressText => $"{Progress}%";
}
