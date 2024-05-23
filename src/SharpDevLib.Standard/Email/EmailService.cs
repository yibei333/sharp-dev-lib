using Microsoft.Extensions.Options;
using SharpDevLib.Standard;
using System.Net;
using System.Net.Mail;

namespace SharpDevLib.Extensions.Email;

/// <summary>
/// email service
/// </summary>
public class EmailService : IEmailService
{
    private readonly EmailOptions _options;

    /// <summary>
    /// instantient email service use di
    /// </summary>
    /// <param name="options">email options</param>
    public EmailService(IOptions<EmailOptions> options)
    {
        _options = options.Value;
    }

    /// <summary>
    /// instantient email service use di
    /// </summary>
    /// <param name="options">email options</param>
    public EmailService(EmailOptions options)
    {
        _options = options;
    }

    /// <summary>
    /// send email
    /// </summary>
    /// <param name="content">email content</param>
    public void Send(EmailContent content)
    {
        VerifyOptions(content);
        var message = BuildMailMessage(content);
        using var client = CreateClient();
        client.Send(message);
    }

    /// <summary>
    /// send email async
    /// </summary>
    /// <param name="content">email content</param>
    /// <param name="cancellationToken">cancellationToken</param>
    public async Task SendAsync(EmailContent content, CancellationToken? cancellationToken)
    {
        VerifyOptions(content);
        var message = BuildMailMessage(content);
        using var client = CreateClient();
        await Task.Run(async () =>
        {
            await client.SendMailAsync(message);
        }, cancellationToken ?? CancellationToken.None);
        //await client.SendMailAsync(message, cancellationToken ?? CancellationToken.None);
    }

    #region Private
    private void VerifyOptions(EmailContent content)
    {
        if (_options.Host.IsNullOrWhiteSpace()) throw new EmailVerifyException("host requried");
        if (_options.Port <= 0) throw new EmailVerifyException("port requried");
        if (_options.Sender.IsNullOrWhiteSpace()) throw new EmailVerifyException("sender address requried");
        if (_options.SenderPassword.IsNullOrWhiteSpace()) throw new EmailVerifyException("sender password requried");

        if (content.Receivers.IsNullOrEmpty()) throw new EmailVerifyException("receivers requried");
        if (content.Subject.IsNullOrWhiteSpace()) throw new EmailVerifyException("email subject requried");
        if (content.Body.IsNullOrWhiteSpace()) throw new EmailVerifyException("email body requried");
    }

    private SmtpClient CreateClient()
    {
        var client = new SmtpClient(_options.Host, _options.Port)
        {
            EnableSsl = _options.UseSSL,
            Credentials = new NetworkCredential(_options.Sender, _options.SenderPassword)
        };
        return client;
    }

    private MailMessage BuildMailMessage(EmailContent content)
    {
        var message = new MailMessage(_options.Sender!, string.Join(",", content.Receivers ?? new List<string>()))
        {
            Subject = content.Subject,
            Body = content.Body,
            Priority = content.Priority ?? MailPriority.Normal,
            From = new MailAddress(_options.Sender!, _options.SenderDisplayName),
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