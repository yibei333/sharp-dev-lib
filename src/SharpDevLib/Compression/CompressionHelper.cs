using SharpDevLib.Compression.Internal;

namespace SharpDevLib;

/// <summary>
/// 压缩/解压扩展
/// </summary>
public static class CompressionHelper
{
    /// <summary>
    /// 压缩文件
    /// </summary>
    /// <param name="request">选项</param>
    /// <returns>Task</returns>
    public static async Task CompressAsync(this CompressRequest request) => await request.InternalCompressAsync();

    /// <summary>
    /// 解压文件
    /// </summary>
    /// <param name="request">选项</param>
    /// <returns>Task</returns>
    public static async Task DeCompressAsync(this DeCompressRequest request) => await request.InternalDeCompressAsync();
}
