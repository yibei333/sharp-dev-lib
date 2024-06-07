using SharpDevLib.Tests.Transport.Email.EmailHost.Pop3;
using SharpDevLib.Tests.Transport.Email.EmailHost.Pop3.Lib;
using System;
using System.Net;
using System.Threading.Tasks;

namespace SharpDevLib.Tests.Transport.Email.EmailHost;

public class Pop3Host
{
    readonly POP3Listener _listener;
    readonly IServiceProvider _serviceProvider;

    public Pop3Host(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        _listener = new POP3Listener();
        _listener.Events.OnAuthenticate = new AuthHandler(_serviceProvider).Handle;
        _listener.Events.OnMessageList = new ListHandler(_serviceProvider).Handle;
        _listener.Events.OnMessageRetrieval = new RetrievalHandler(_serviceProvider).Handle;
        _listener.Events.OnMessageDelete = new DeleteHandler(_serviceProvider).Handle;
    }

    public async void StartAsync()
    {
        await Task.Yield();
        _listener.ListenOn(IPAddress.Loopback, 110, false);
    }

    public void Stop()
    {
        _listener.Stop();
    }
}
