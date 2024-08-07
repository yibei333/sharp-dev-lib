﻿using Microsoft.Extensions.DependencyInjection;

namespace SharpDevLib.Transport;

/// <summary>
/// 邮件扩展
/// </summary>
public static class Email
{
    /// <summary>
    /// 添加邮件服务
    /// </summary>
    /// <param name="services">service collection</param>
    /// <returns>service collection</returns>
    public static IServiceCollection AddEmailService(this IServiceCollection services)
    {
        services.AddScoped<IEmailService, EmailService>();
        return services;
    }

    /// <summary>
    /// 发送邮件
    /// </summary>
    /// <param name="options">配置</param>
    /// <param name="content">内容</param>
    public static void Send(this EmailOptions options, EmailContent content)
    {
        new EmailService(options).Send(content);
    }

    /// <summary>
    /// 发送邮件
    /// </summary>
    /// <param name="options">配置</param>
    /// <param name="content">内容</param>
    /// <param name="cancellationToken">cancellationToken</param>
    public static async Task SendAsync(this EmailOptions options, EmailContent content, CancellationToken? cancellationToken = null)
    {
        await new EmailService(options).SendAsync(content, cancellationToken);
    }
}
