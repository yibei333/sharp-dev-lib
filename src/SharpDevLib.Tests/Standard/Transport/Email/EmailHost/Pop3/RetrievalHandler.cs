using SharpDevLib.Tests.Standard.Email.EmailHost.Pop3.Lib;
using System;
using System.Linq;

namespace SharpDevLib.Tests.Standard.Email.EmailHost.Pop3;

public class RetrievalHandler(IServiceProvider serviceProvider) : BaseVoidHandler<POP3MessageRetrievalRequest>(serviceProvider)
{
    public override void Handle(POP3MessageRetrievalRequest request)
    {
        var user = UserService.Get(x => x.Id == Guid.Parse(request.AuthMailboxID)).FirstOrDefault() ?? throw new NullReferenceException();
        var emailDetail = EmailDetailSerivce.Get(x => x.UserId == user.Id && x.EmailId == Guid.Parse(request.MessageUniqueID)).FirstOrDefault() ?? throw new NullReferenceException();
        var email = EmailSerivce.Get(x => x.Id == emailDetail.EmailId).FirstOrDefault() ?? throw new NullReferenceException();
        request.UseTextFile(email.FilePath, false);
    }
}
