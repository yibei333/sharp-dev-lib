using SharpDevLib.Tests.Standard.Email.EmailHost.Pop3.Lib;
using System;
using System.Linq;

namespace SharpDevLib.Tests.Standard.Email.EmailHost.Pop3;

public class AuthHandler(IServiceProvider serviceProvider) : BaseVoidHandler<POP3AuthenticationRequest>(serviceProvider)
{
    public override void Handle(POP3AuthenticationRequest request)
    {
        var user = UserService.Get(x => x.Name == request.SuppliedUsername && x.Password == request.SuppliedPassword).FirstOrDefault();
        if (user is not null)
        {
            request.AuthMailboxID = user.Id.ToString();
        }
    }
}
