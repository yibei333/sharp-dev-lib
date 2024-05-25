using SharpDevLib.Standard;
using System.Net.Mail;
using System.Text;

namespace SharpDevLib.Extensions.Email;

/// <summary>
/// 邮箱内容
/// </summary>
public class EmailContent
{
    private EmailContent() { }

    /// <summary>
    /// 实例化邮箱内容
    /// </summary>
    /// <param name="receivers">收件人</param>
    /// <param name="subject">标题</param>
    /// <param name="body">主体</param>
    public EmailContent(IEnumerable<string>? receivers, string? subject, string? body)
    {
        Receivers = receivers;
        Subject = subject;
        Body = body;
    }

    /// <summary>
    /// 收件人
    /// </summary>
    public IEnumerable<string>? Receivers { get; set; }

    /// <summary>
    /// 抄送人
    /// </summary>
    public IEnumerable<string>? CC { get; set; }

    /// <summary>
    /// 密送人
    /// </summary>
    public IEnumerable<string>? BCC { get; set; }

    /// <summary>
    /// 回复人
    /// </summary>
    public IEnumerable<string>? Repliers { get; set; }

    /// <summary>
    /// 标题
    /// </summary>
    public string? Subject { get; set; }

    /// <summary>
    /// 主体
    /// </summary>
    public string? Body { get; set; }

    /// <summary>
    /// 附件
    /// </summary>
    public IEnumerable<EmailAttachment>? Attachments { get; set; }

    /// <summary>
    /// 优先级
    /// </summary>
    public MailPriority? Priority { get; set; }

    /// <summary>
    /// 主体编码
    /// </summary>
    public Encoding? BodyEncoding { get; set; }

    /// <summary>
    /// 头部编码
    /// </summary>
    public Encoding? HeaderEncoding { get; set; }

    /// <summary>
    /// 是否是Html内容
    /// </summary>
    public bool IsHtml { get; set; }
}

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