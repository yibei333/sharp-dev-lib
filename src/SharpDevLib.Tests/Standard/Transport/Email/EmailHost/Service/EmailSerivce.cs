using System;

namespace SharpDevLib.Tests.Standard.Email.EmailHost.Service;

public class EmailSerivce : BaseService<Models.Email>
{

    public Guid Save(Models.Email email)
    {
        Data.Add(email);
        return email.Id;
    }
}
