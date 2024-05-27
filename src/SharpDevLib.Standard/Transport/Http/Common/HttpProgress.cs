using System.Text;

namespace SharpDevLib.Standard;

/// <summary>
/// http进度
/// </summary>
public class HttpProgress
{
    long _transfered;
    DateTime? _lastTransferTime;

    /// <summary>
    /// 总字节数
    /// </summary>
    public long Total { get; internal set; }

    /// <summary>
    /// 已传输字节数
    /// </summary>
    public long Transfered
    {
        get => _transfered;
        internal set
        {
            var singleTransfered = value - _transfered;
            _transfered = value;

            var now = DateTime.Now;
            var time = now - (_lastTransferTime ?? now);
            _lastTransferTime = now;
            if (time.TotalMilliseconds <= 0) return;
            var count = (long)Math.Round(singleTransfered * 1000 / time.TotalMilliseconds, 2);
            Speed = $"{count.ToFileSizeString()}/s";
        }
    }

    /// <summary>
    /// 进度(0-100)
    /// </summary>
    public double Progress => Total <= 0 ? 0 : Math.Round(Transfered * 100.0 / Total, 2);

    /// <summary>
    /// 进度字符串(带%)
    /// </summary>
    public string ProgressString => $"{Progress}%";

    /// <summary>
    /// 传输速度
    /// </summary>
    public string Speed { get; private set; } = string.Empty;

    internal void Reset()
    {
        _lastTransferTime = DateTime.Now;
        _transfered = 0;
        Total = 0;
    }

    /// <summary>
    /// 将请求转换为字符串,用于记录日志
    /// </summary>
    /// <returns>字符串</returns>
    public override string ToString()
    {
        var builder = new StringBuilder();
        builder.AppendLine($"********progress********");
        builder.AppendLine($"Total:{Total}");
        builder.AppendLine($"Transfered:{Transfered}");
        builder.AppendLine($"Progress:{Progress}");
        builder.AppendLine($"ProgressString:{ProgressString}");
        builder.AppendLine($"Speed:{Speed}");
        return builder.ToString();
    }
}
