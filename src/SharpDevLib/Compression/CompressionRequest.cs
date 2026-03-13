namespace SharpDevLib;

/// <summary>
/// 压缩/解压请求基类，定义压缩和解压操作的通用配置
/// </summary>
/// <remarks>
/// 使用指定目标路径示例化压缩/解压请求
/// </remarks>
/// <param name="targetPath">保存压缩文件或解压文件的目标路径</param>
public abstract class CompressionRequest(string targetPath)
{
    long _transfered;
    internal long Total { get; set; }
    internal CompressionProgressArgs? progress;
    internal string? CurrentName { get; set; }
    internal long Transfered
    {
        get => _transfered;
        set
        {
            var handledValue = Math.Min(value, Total);
            if (handledValue == _transfered) return;
            _transfered = handledValue;
            if (Total > 0 && OnProgress is not null)
            {
                progress ??= new CompressionProgressArgs { Total = Total };
                progress.CurrentName = CurrentName;
                var lastProcess = progress.Progress;
                progress.Trasnsfed = _transfered;
                if ((progress.Progress - lastProcess) > 5) OnProgress.Invoke(progress);
            }
        }
    }

    /// <summary>
    /// 获取保存压缩文件或解压文件的目标路径
    /// </summary>
    public string TargetPath { get; } = targetPath;

    /// <summary>
    /// 获取或设置压缩文件的密码（如果需要加密）
    /// </summary>
    public string? Password { get; set; }

    /// <summary>
    /// 获取或设置取消令牌，用于取消长时间运行的压缩或解压操作
    /// </summary>
    public CancellationToken? CancellationToken { get; set; }

    /// <summary>
    /// 获取或设置进度变化回调函数，用于接收压缩/解压进度更新
    /// </summary>
    public Action<CompressionProgressArgs>? OnProgress { get; set; }
}
