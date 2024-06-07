using System;

namespace SharpDevLib.Tests.Transport.Email.EmailHost.Service;

public class EmailSerivce : BaseService<Models.Email>
{

    public Guid Save(Models.Email email)
    {
        Data.Add(email);
        return email.Id;
    }
}
