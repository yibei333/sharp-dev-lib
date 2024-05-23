namespace SharpDevLib.Extensions.Email;

/// <summary>
/// email basic options
/// </summary>
public class EmailOptions
{
    /// <summary>
    /// server address
    /// </summary>
    public string? Host { get; set; }
    /// <summary>
    /// server port
    /// </summary>
    public int Port { get; set; }
    /// <summary>
    /// indicate is use ssl
    /// </summary>
    public bool UseSSL { get; set; }
    /// <summary>
    /// sender address
    /// </summary>
    public string? Sender { get; set; }
    /// <summary>
    /// sender password
    /// </summary>
    public string? SenderPassword { get; set; }
    /// <summary>
    /// send display name
    /// </summary>
    public string? SenderDisplayName { get; set; }
}