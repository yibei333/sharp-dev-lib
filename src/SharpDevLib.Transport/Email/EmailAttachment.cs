using SharpDevLib.Transport.Internal.References;

namespace SharpDevLib.Transport;

/// <summary>
/// 邮件附件
/// </summary>
public class EmailAttachment
{
    private EmailAttachment() { }

    /// <summary>
    /// 实例化邮件附件
    /// </summary>
    /// <param name="path">文件路径</param>
    public EmailAttachment(string path)
    {
        path.EnsureFileExist();
        Path = path;
        Name = new FileInfo(path).Name;
        Bytes = File.ReadAllBytes(path);
    }

    /// <summary>
    /// 实例化邮件附件
    /// </summary>
    /// <param name="name">文件名</param>
    /// <param name="bytes">字节数组</param>
    public EmailAttachment(string name, byte[] bytes)
    {
        Name = name;
        Bytes = bytes;
    }

    /// <summary>
    /// 文件名
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// 字节数组
    /// </summary>
    public byte[]? Bytes { get; set; }

    /// <summary>
    /// 文件路径
    /// </summary>
    public string? Path { get; set; }
}