namespace SharpDevLib.Extensions.Email;

/// <summary>
/// email service abstraction
/// </summary>
public interface IEmailService
{
    /// <summary>
    /// send email
    /// </summary>
    /// <param name="content">email content</param>
    void Send(EmailContent content);
    /// <summary>
    /// send email
    /// </summary>
    /// <param name="content">email content</param>
    /// <param name="cancellationToken">async cancellationToken</param>
    Task SendAsync(EmailContent content, CancellationToken? cancellationToken);
}