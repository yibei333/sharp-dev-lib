namespace SharpDevLib;

/// <summary>
/// 压缩文件格式枚举
/// </summary>
public enum CompressionFormat
{
    /// <summary>
    /// 未知格式
    /// </summary>
    UnKnown,
    /// <summary>
    /// ZIP格式，扩展名为.zip
    /// </summary>
    Zip,
    /// <summary>
    /// RAR格式，扩展名为.rar
    /// </summary>
    Rar,
    /// <summary>
    /// 7-Zip格式，扩展名为.7z
    /// </summary>
    SevenZip,
    /// <summary>
    /// TAR格式，扩展名为.tar
    /// </summary>
    Tar,
    /// <summary>
    /// GZIP格式，扩展名为.tgz或.tar.gz
    /// </summary>
    Gz,
    /// <summary>
    /// XZ格式，扩展名为.tar.xz
    /// </summary>
    Xz,
    /// <summary>
    /// BZIP2格式，扩展名为.bz2
    /// </summary>
    Bz2,
}