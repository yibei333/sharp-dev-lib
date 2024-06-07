namespace SharpDevLib.Compression;

/// <summary>
/// 压缩/解压扩展
/// </summary>
public static class CompressionExtension
{
    /// <summary>
    /// 压缩文件
    /// </summary>
    /// <param name="option">选项</param>
    /// <param name="cancellationToken">cancellationToken</param>
    /// <returns>Task</returns>
    public static async Task CompressAsync(this CompressOption option, CancellationToken? cancellationToken = null) => await option.InternalCompressAsync(cancellationToken);

    /// <summary>
    /// 解压文件
    /// </summary>
    /// <param name="option">选项</param>
    /// <param name="cancellationToken">cancellationToken</param>
    /// <returns>Task</returns>
    public static async Task DeCompressAsync(this DeCompressOption option, CancellationToken? cancellationToken = null) => await option.InternalDeCompressAsync(cancellationToken);

    /// <summary>
    /// 压缩文件
    /// </summary>
    /// <param name="option">选项</param>
    /// <returns>Task</returns>
    public static void Compress(this CompressOption option) => option.InternalCompressAsync().GetAwaiter().GetResult();

    /// <summary>
    /// 解压文件
    /// </summary>
    /// <param name="option">选项</param>
    /// <returns>Task</returns>
    public static void DeCompress(this DeCompressOption option) => option.InternalDeCompressAsync().GetAwaiter().GetResult();
}
