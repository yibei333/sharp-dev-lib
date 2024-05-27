using SharpDevLib.Tests.Standard.Email.EmailHost.Smtp;
using SmtpServer;
using System;
using System.Threading;

namespace SharpDevLib.Tests.Standard.Email.EmailHost;

public class SmtpHost
{
    readonly IServiceProvider _serviceProvider;
    readonly SmtpServer.SmtpServer _server;

    public SmtpHost(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;

        var options = new SmtpServerOptionsBuilder()
                        .ServerName("localhost")
                        .Endpoint(builder =>
                        {
                            builder.Port(25).AllowUnsecureAuthentication(true);
                        })
                        .Build();

        var internalServiceProvider = new SmtpServer.ComponentModel.ServiceProvider();
        internalServiceProvider.Add(new SampleMessageStore(_serviceProvider));
        internalServiceProvider.Add(new SampleMailboxFilter(_serviceProvider));
        internalServiceProvider.Add(new SampleUserAuthenticator(_serviceProvider));

        _server = new SmtpServer.SmtpServer(options, internalServiceProvider);
    }

    public async void StartAsync()
    {
        await _server.StartAsync(CancellationToken.None);
    }

    public void Stop() => _server.Shutdown();
}



