using System.Net.Mail;
using System.Text;

namespace SharpDevLib;

/// <summary>
/// 邮箱内容
/// </summary>
public class EmailContent
{
    EmailContent() { }

    /// <summary>
    /// 实例化邮件内容
    /// </summary>
    /// <param name="receivers">收件人邮箱地址列表</param>
    /// <param name="subject">邮件主题</param>
    /// <param name="body">邮件正文</param>
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
    /// 抄送人邮箱地址列表
    /// </summary>
    public IEnumerable<string>? CC { get; set; }

    /// <summary>
    /// 密送人邮箱地址列表
    /// </summary>
    public IEnumerable<string>? BCC { get; set; }

    /// <summary>
    /// 回复人邮箱地址列表
    /// </summary>
    public IEnumerable<string>? Repliers { get; set; }

    /// <summary>
    /// 邮件主题
    /// </summary>
    public string? Subject { get; set; }

    /// <summary>
    /// 邮件正文内容
    /// </summary>
    public string? Body { get; set; }

    /// <summary>
    /// 邮件附件列表
    /// </summary>
    public IEnumerable<EmailAttachment>? Attachments { get; set; }

    /// <summary>
    /// 邮件优先级
    /// </summary>
    public MailPriority? Priority { get; set; }

    /// <summary>
    /// 邮件正文编码
    /// </summary>
    public Encoding? BodyEncoding { get; set; }

    /// <summary>
    /// 邮件头部编码
    /// </summary>
    public Encoding? HeaderEncoding { get; set; }

    /// <summary>
    /// 邮件正文是否为HTML格式
    /// </summary>
    public bool IsHtml { get; set; }
}