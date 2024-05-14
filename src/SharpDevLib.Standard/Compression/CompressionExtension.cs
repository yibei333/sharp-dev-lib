namespace SharpDevLib.Standard;

/// <summary>
/// 压缩/解压扩展
/// </summary>
public static class CompressionExtension
{
    /// <summary>
    /// 压缩文件
    /// </summary>
    /// <param name="option">选项</param>
    /// <returns>Task</returns>
    public static async Task CompressAsync(this CompressOption option) => await option.InternalCompressAsync();

    /// <summary>
    /// 解压文件
    /// </summary>
    /// <param name="option">选项</param>
    /// <returns>Task</returns>
    public static async Task DeCompressAsync(this DeCompressOption option) => await option.InternalDeCompressAsync();
}
