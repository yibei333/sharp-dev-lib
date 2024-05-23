using SmtpServer;
using SmtpServer.Authentication;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SharpDevLib.Tests.Standard.Email.EmailHost.Smtp;

public class SampleUserAuthenticator(IServiceProvider serviceProvider) : SmtpBase(serviceProvider), IUserAuthenticator
{
    public async Task<bool> AuthenticateAsync(ISessionContext context, string name, string password, CancellationToken token)
    {
        await Task.Yield();
        return UserService.Get(x => x.Name == name && x.Password == password).FirstOrDefault() is not null;
    }
}
