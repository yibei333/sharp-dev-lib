using System.Text;

namespace SharpDevLib;

/// <summary>
/// HTTP传输进度
/// </summary>
public class HttpProgress
{
    long _transfered;
    DateTime? _lastTransferTime;

    /// <summary>
    /// HttpRequestMessage
    /// </summary>
    public HttpRequestMessage? RequestMessage { get; internal set; }

    /// <summary>
    /// ResponseMessage
    /// </summary>
    public HttpResponseMessage? ResponseMessage { get; internal set; }

    /// <summary>
    /// 总字节数
    /// </summary>
    /// <remarks>某些情况下可能为0，表示未知总大小</remarks>
    public long Total { get; internal set; }

    /// <summary>
    /// 请求地址
    /// </summary>
    public string RequestUrl { get; internal set; } = "";

    /// <summary>
    /// 已传输字节数
    /// </summary>
    public long Transfered
    {
        get => _transfered;
        internal set
        {
            _lastTransferTime ??= DateTime.Now;
            var singleTransfered = value - _transfered;
            _transfered = value;
            if (_transfered <= 0) return;

            var now = DateTime.Now;
            var time = now - (_lastTransferTime ?? now);
            _lastTransferTime = now;
            if (time.TotalMilliseconds <= 0) return;
            var count = (long)Math.Round(singleTransfered * 1000 / time.TotalMilliseconds, 2);
            Speed = $"{count.ToFileSizeString()}/s";
        }
    }

    /// <summary>
    /// 传输进度百分比
    /// </summary>
    /// <value>范围0-100</value>
    public double Progress => Total <= 0 ? 0 : Transfered >= Total ? 100 : Math.Round(Transfered * 100.0 / Total, 2);

    /// <summary>
    /// 进度字符串（带百分号）
    /// </summary>
    public string ProgressString => $"{Progress}%";

    /// <summary>
    /// 当前传输速度
    /// </summary>
    /// <value>格式为"XX/s"，XX为带单位的大小字符串</value>
    public string Speed { get; private set; } = string.Empty;

    /// <summary>
    /// 将进度信息转换为字符串
    /// </summary>
    /// <returns>包含总字节数、已传输字节数、进度、速度等信息的字符串</returns>
    public override string ToString()
    {
        var builder = new StringBuilder();
        builder.AppendLine($"********progress********");
        builder.AppendLine($"RequestUrl:{RequestUrl}");
        builder.AppendLine($"Total:{Total}");
        builder.AppendLine($"Transfered:{Transfered}");
        builder.AppendLine($"Progress:{Progress}");
        builder.AppendLine($"ProgressString:{ProgressString}");
        builder.AppendLine($"Speed:{Speed}");
        return builder.ToString();
    }
}
