namespace SharpDevLib.Compression;

/// <summary>
/// 压缩/解压选项
/// </summary>
public abstract class CompressionOption
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
            _transfered = value;
            if (Total > 0 && OnProgress is not null)
            {
                progress ??= new CompressionProgressArgs { Total = Total };
                progress.CurrentName = CurrentName;
                progress.Handled = _transfered;
                OnProgress.Invoke(progress);
            }
        }
    }

    /// <summary>
    /// 实例化压缩/解压选项
    /// </summary>
    /// <param name="targetPath">目标路径</param>
    public CompressionOption(string targetPath)
    {
        TargetPath = targetPath;
    }

    /// <summary>
    /// 保存目标路径
    /// </summary>
    public string TargetPath { get; }

    /// <summary>
    /// 密码
    /// </summary>
    public string? Password { get; set; }

    /// <summary>
    /// 进度变化回调
    /// </summary>
    public Action<CompressionProgressArgs>? OnProgress { get; set; }
}
