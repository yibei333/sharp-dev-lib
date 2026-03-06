using SharpDevLib.Compression.Internal;

namespace SharpDevLib;

/// <summary>
/// 压缩和解压扩展，提供文件和目录的压缩与解压功能
/// </summary>
public static class CompressionHelper
{
    /// <summary>
    /// 异步压缩文件或目录
    /// </summary>
    /// <param name="request">压缩请求配置</param>
    /// <returns>表示异步压缩任务的Task</returns>
    public static async Task CompressAsync(this CompressRequest request) => await request.InternalCompressAsync();

    /// <summary>
    /// 异步解压文件
    /// </summary>
    /// <param name="request">解压请求配置</param>
    /// <returns>表示异步解压任务的Task</returns>
    public static async Task DeCompressAsync(this DeCompressRequest request) => await request.InternalDeCompressAsync();
}
