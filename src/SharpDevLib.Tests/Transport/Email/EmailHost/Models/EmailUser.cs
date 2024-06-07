using System;

namespace SharpDevLib.Tests.Transport.Email.EmailHost.Models;

public class EmailUser(string name, string password)
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Name { get; set; } = name;

    public string Password { get; set; } = password;
}
