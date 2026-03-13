namespace SharpDevLib;

/// <summary>
/// 邮件附件
/// </summary>
public class EmailAttachment
{
    EmailAttachment() { }

    /// <summary>
    /// 从文件路径示例化邮件附件
    /// </summary>
    /// <param name="path">文件路径，必须存在</param>
    /// <exception cref="FileNotFoundException">当文件不存在时引发异常</exception>
    public EmailAttachment(string path)
    {
        path.ThrowIfFileNotExist();
        Path = path;
        Name = new FileInfo(path).Name;
        Bytes = File.ReadAllBytes(path);
    }

    /// <summary>
    /// 从字节数组示例化邮件附件
    /// </summary>
    /// <param name="name">文件名</param>
    /// <param name="bytes">文件字节数组</param>
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