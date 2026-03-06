using Microsoft.Extensions.DependencyInjection;
using SharpDevLib.Transport;
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
    /// 设置配置
    /// </summary>
    /// <param name="config">配置</param>
    public static void SetConfig(EmailConfig config)
    {
        if (config is null) throw new EmailVerifyException("call EmailHelper.SetConfig() to set config first");
        if (config.Host.IsNullOrWhiteSpace()) throw new EmailVerifyException("host requried");
        if (config.Port <= 0) throw new EmailVerifyException("port requried");
        if (config.Sender.IsNullOrWhiteSpace()) throw new EmailVerifyException("sender address requried");
        if (config.SenderPassword.IsNullOrWhiteSpace()) throw new EmailVerifyException("sender password requried");
        _config = config;
    }

    /// <summary>
    /// 发送邮件
    /// </summary>
    /// <param name="content">内容</param>
    /// <param name="cancellationToken">cancellationToken</param>
    public static async Task SendAsync(this EmailContent content, CancellationToken? cancellationToken = null)
    {
        await Task.Yield();
        var config = _config ?? throw new EmailVerifyException("call EmailHelper.SetConfig() to set config first");
        if (content.Receivers.IsNullOrEmpty()) throw new EmailVerifyException("receivers requried");
        if (content.Subject.IsNullOrWhiteSpace()) throw new EmailVerifyException("email subject requried");
        if (content.Body.IsNullOrWhiteSpace()) throw new EmailVerifyException("email body requried");

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
                message.Attachments.Add(new Attachment(new MemoryStream(attachment.Bytes ?? Array.Empty<byte>()), attachment.Name));
            }
        }
        return message;
    }
    #endregion
}
