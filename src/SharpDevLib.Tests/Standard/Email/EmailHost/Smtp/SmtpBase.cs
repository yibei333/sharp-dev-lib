using Microsoft.Extensions.DependencyInjection;
using SharpDevLib.Tests.Standard.Email.EmailHost.Service;
using System;

namespace SharpDevLib.Tests.Standard.Email.EmailHost.Smtp;

public abstract class SmtpBase(IServiceProvider serviceProvider)
{
    protected IServiceProvider ServiceProvider { get; } = serviceProvider;
    protected EmailUserService UserService { get; } = serviceProvider.GetRequiredService<EmailUserService>();
    protected EmailDetailSerivce EmailDetailSerivce { get; } = serviceProvider.GetRequiredService<EmailDetailSerivce>();
    protected EmailSerivce EmailSerivce { get; } = serviceProvider.GetRequiredService<EmailSerivce>();
}
