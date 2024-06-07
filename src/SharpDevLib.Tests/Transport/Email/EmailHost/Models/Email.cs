using System;

namespace SharpDevLib.Tests.Transport.Email.EmailHost.Models;

public class Email(string subject, string messageId, string filePath)
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string FilePath { get; set; } = filePath;
    public string Subject { get; set; } = subject;
    public string MessageId { get; set; } = messageId;
    public string? ReferenceMessageIds { get; set; }
}
