namespace SharpDevLib.Standard;

/// <summary>
/// 压缩/解压进度参数
/// </summary>
public class CompressionProgressArgs<T> where T : struct
{
    internal CompressionProgressArgs()
    {

    }

    /// <summary>
    /// 当前处理的文件名称
    /// </summary>
    public string? CurrentName { get; internal set; }

    /// <summary>
    /// 总数(int为文件个数,double为字节个数)
    /// </summary>
    public T Total { get; internal set; }

    /// <summary>
    /// 已处理的数量(int为文件个数,double为字节个数)
    /// </summary>
    public T Handled { get; internal set; }

    /// <summary>
    /// 进度(%)
    /// </summary>
    public double Progress
    {
        get
        {
            var total = (double)Convert.ChangeType(Total, typeof(double));
            if (total <= 0) return 0;
            var handled = (double)Convert.ChangeType(Handled, typeof(double));
            return Math.Round(handled * 1.0 / total * 100, 2);
        }
    }

    /// <summary>
    /// 进度字符串(加了%)
    /// </summary>
    public string ProgressText => $"{Progress}%";
}
