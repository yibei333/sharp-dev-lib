namespace SharpDevLib.Extensions.Email;

/// <summary>
/// 邮件服务
/// </summary>
public interface IEmailService
{
    /// <summary>
    /// 发送邮件
    /// </summary>
    /// <param name="content">内容</param>
    void Send(EmailContent content);

    /// <summary>
    /// 发送邮件
    /// </summary>
    /// <param name="content">内容</param>
    /// <param name="cancellationToken">cancellationToken</param>
    /// <returns>Task</returns>
    Task SendAsync(EmailContent content, CancellationToken? cancellationToken);
}