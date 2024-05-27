using System;
using System.Collections.Generic;
using System.Linq;

namespace SharpDevLib.Tests.Standard.Email.EmailHost.Pop3;

public class DeleteHandler(IServiceProvider serviceProvider) : BaseVoidHandler<string, IList<string>>(serviceProvider)
{
    public override void Handle(string request1, IList<string> request2)
    {
        var user = UserService.Get(x => x.Id == Guid.Parse(request1)).FirstOrDefault() ?? throw new NullReferenceException();
        var emailDetail = EmailDetailSerivce.Get(x => x.UserId == user.Id && request2.Select(x => x.ToLower()).Contains(x.EmailId.ToString().ToLower())).ToList();
        emailDetail.ForEach(x =>
        {
            x.IsDeleted = true;
        });
    }
}
