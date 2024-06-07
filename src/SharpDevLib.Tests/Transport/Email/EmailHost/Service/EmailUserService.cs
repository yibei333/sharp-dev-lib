using SharpDevLib.Tests.Transport.Email.EmailHost.Models;

namespace SharpDevLib.Tests.Transport.Email.EmailHost.Service;

public class EmailUserService : BaseService<EmailUser>
{
    public EmailUserService()
    {
        Data.Add(new("foo", "foo_password"));
        Data.Add(new("bar", "bar_password"));
        Data.Add(new("baz", "baz_password"));
        Data.Add(new("qux", "qux_password"));
    }
}
