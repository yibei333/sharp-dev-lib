using SharpDevLib.Tests.Transport.Email.EmailHost.Models;
using System;

namespace SharpDevLib.Tests.Transport.Email.EmailHost.Service;

public class EmailDetailSerivce : BaseService<EmailDetail>
{
    public Guid Save(EmailDetail detail)
    {
        Data.Add(detail);
        return detail.Id;
    }
}
