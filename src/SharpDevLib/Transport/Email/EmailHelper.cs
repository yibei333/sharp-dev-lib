using System.Net;
using System.Net.Mail;

namespace SharpDevLib;

/// <summary>
/// 邮件扩展
/// </summary>
public static class EmailHelper
{
    static EmailConfig? _config;

    /// <summary>
    /// 设置邮件发送配置
    /// </summary>
    /// <param name="config">邮件配置对象</param>
    /// <exception cref="EmailVerifyException">当配置无效时引发异常</exception>
    public static void SetConfig(EmailConfig config)
    {
        if (config is null) throw new EmailVerifyException("请先调用EmailHelper.SetConfig()设置配置");
        if (config.Host.IsNullOrWhiteSpace()) throw new EmailVerifyException("SMTP服务器地址不能为空");
        if (config.Port <= 0) throw new EmailVerifyException("端口号必须大于0");
        if (config.Sender.IsNullOrWhiteSpace()) throw new EmailVerifyException("发件人地址不能为空");
        if (config.SenderPassword.IsNullOrWhiteSpace()) throw new EmailVerifyException("发件人密码不能为空");
        _config = config;
    }

    /// <summary>
    /// 异步发送邮件
    /// </summary>
    /// <param name="content">邮件内容对象</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <exception cref="EmailVerifyException">当邮件内容无效时引发异常</exception>
    public static async Task SendAsync(this EmailContent content, CancellationToken? cancellationToken = null)
    {
        await Task.Yield();
        var config = _config ?? throw new EmailVerifyException("请先调用EmailHelper.SetConfig()设置配置");
        if (content.Receivers.IsNullOrEmpty()) throw new EmailVerifyException("收件人地址不能为空");
        if (content.Subject.IsNullOrWhiteSpace()) throw new EmailVerifyException("邮件主题不能为空");
        if (content.Body.IsNullOrWhiteSpace()) throw new EmailVerifyException("邮件正文不能为空");

        var message = BuildMailMessage(config, content);
        using var client = CreateClient(config);
        await Task.Run(() =>
        {
            client.Send(message);
        }, cancellationToken ?? CancellationToken.None);
    }

    #region Private
    static SmtpClient CreateClient(EmailConfig config)
    {
        var client = new SmtpClient(config.Host, config.Port)
        {
            EnableSsl = config.UseSSL,
            Credentials = new NetworkCredential(config.Sender, config.SenderPassword)
        };
        return client;
    }

    static MailMessage BuildMailMessage(EmailConfig config, EmailContent content)
    {
        var message = new MailMessage(config.Sender!, string.Join(",", content.Receivers ?? []))
        {
            Subject = content.Subject,
            Body = content.Body,
            Priority = content.Priority ?? MailPriority.Normal,
            From = new MailAddress(config.Sender!, config.SenderDisplayName),
            IsBodyHtml = content.IsHtml,
        };

        if (content.CC.NotNullOrEmpty()) message.CC.Add(string.Join(",", content.CC));
        if (content.BCC.NotNullOrEmpty()) message.CC.Add(string.Join(",", content.BCC));
        if (content.Repliers.NotNullOrEmpty()) message.ReplyToList.Add(string.Join(",", content.Repliers));
        if (content.BodyEncoding is not null) message.BodyEncoding = content.BodyEncoding;
        if (content.HeaderEncoding is not null) message.HeadersEncoding = content.HeaderEncoding;
        if (content.Attachments.NotNullOrEmpty())
        {
            foreach (EmailAttachment attachment in content.Attachments)
            {
                message.Attachments.Add(new Attachment(new MemoryStream(attachment.Bytes ?? []), attachment.Name));
            }
        }
        return message;
    }
    #endregion
}
