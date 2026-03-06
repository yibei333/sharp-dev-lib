namespace SharpDevLib;

/// <summary>
/// 压缩/解压选项
/// </summary>
/// <remarks>
/// 实例化压缩/解压选项
/// </remarks>
/// <param name="targetPath">目标路径</param>
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
    /// 保存目标路径
    /// </summary>
    public string TargetPath { get; } = targetPath;

    /// <summary>
    /// 密码
    /// </summary>
    public string? Password { get; set; }

    /// <summary>
    /// 取消令牌
    /// </summary>
    public CancellationToken? CancellationToken { get; set; }

    /// <summary>
    /// 进度变化回调
    /// </summary>
    public Action<CompressionProgressArgs>? OnProgress { get; set; }
}
