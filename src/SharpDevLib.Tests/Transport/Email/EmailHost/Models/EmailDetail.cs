using System;

namespace SharpDevLib.Tests.Transport.Email.EmailHost.Models;

public class EmailDetail(Guid emailId, Guid userId, int type)
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid EmailId { get; set; } = emailId;
    public Guid UserId { get; set; } = userId;
    /// <summary>
    /// 1-from,2-to
    /// </summary>
    public int Type { get; set; } = type;
    public bool IsDeleted { get; set; }
}
