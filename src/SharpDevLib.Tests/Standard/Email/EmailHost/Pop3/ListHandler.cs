using System;
using System.Collections.Generic;
using System.Linq;

namespace SharpDevLib.Tests.Standard.Email.EmailHost.Pop3;

public class ListHandler(IServiceProvider serviceProvider) : BaseHandler<string, IEnumerable<string>>(serviceProvider)
{
    public override IEnumerable<string> Handle(string request)
    {
        var id = Guid.TryParse(request, out var userId) ? userId : Guid.Empty;
        var user = UserService.Get(x => x.Id == id).FirstOrDefault() ?? throw new NullReferenceException();
        var details = EmailDetailSerivce.Get(x => x.UserId == user.Id && x.Type == 2 && !x.IsDeleted);
        return details.Select(x => x.EmailId.ToString()).ToList();
    }
}
