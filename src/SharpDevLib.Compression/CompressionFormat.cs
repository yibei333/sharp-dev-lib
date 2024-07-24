namespace SharpDevLib.Compression;

/// <summary>
/// 压缩文件格式
/// </summary>
public enum CompressionFormat
{
    /// <summary>
    /// 未知
    /// </summary>
    UnKnown,
    /// <summary>
    /// .zip
    /// </summary>
    Zip,
    /// <summary>
    /// .rar
    /// </summary>
    Rar,
    /// <summary>
    /// .7z
    /// </summary>
    SevenZip,
    /// <summary>
    /// .tar
    /// </summary>
    Tar,
    /// <summary>
    /// .tgz或.tar.gz
    /// </summary>
    Gz,
    /// <summary>
    /// .tar.xz
    /// </summary>
    Xz,
    /// <summary>
    /// .bz2
    /// </summary>
    Bz2,
}