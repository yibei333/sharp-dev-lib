using Microsoft.Extensions.DependencyInjection;
using SharpDevLib.Tests.Standard.Email.EmailHost.Service;
using System;

namespace SharpDevLib.Tests.Standard.Email.EmailHost.Pop3;

public abstract class BaseHandler(IServiceProvider serviceProvider)
{
    protected IServiceProvider ServiceProvider { get; } = serviceProvider;
    protected EmailUserService UserService { get; } = serviceProvider.GetRequiredService<EmailUserService>();
    protected EmailDetailSerivce EmailDetailSerivce { get; } = serviceProvider.GetRequiredService<EmailDetailSerivce>();
    protected EmailSerivce EmailSerivce { get; } = serviceProvider.GetRequiredService<EmailSerivce>();
}

public abstract class BaseVoidHandler<TRequest>(IServiceProvider serviceProvider) : BaseHandler(serviceProvider)
{
    public abstract void Handle(TRequest request);
}


public abstract class BaseHandler<TRequest, TResponse>(IServiceProvider serviceProvider) : BaseHandler(serviceProvider)
{
    public abstract TResponse Handle(TRequest request);
}

public abstract class BaseVoidHandler<TRequest1, TRequest2>(IServiceProvider serviceProvider) : BaseHandler(serviceProvider)
{
    public abstract void Handle(TRequest1 request1, TRequest2 request2);
}