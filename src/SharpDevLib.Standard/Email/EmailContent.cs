using SharpDevLib.Standard;
using System.Net.Mail;
using System.Text;

namespace SharpDevLib.Extensions.Email;

/// <summary>
/// email content
/// </summary>
public class EmailContent
{
    /// <summary>
    /// prevent use instantient default
    /// </summary>
    private EmailContent() { }

    /// <summary>
    /// instantient email content
    /// </summary>
    /// <param name="receivers">receivers</param>
    /// <param name="subject">subject</param>
    /// <param name="body">body</param>
    public EmailContent(IEnumerable<string>? receivers, string? subject, string? body)
    {
        Receivers = receivers;
        Subject = subject;
        Body = body;
    }


    /// <summary>
    /// receivers address
    /// </summary>
    public IEnumerable<string>? Receivers { get; set; }
    /// <summary>
    /// the carbon copies (CC) recipients address
    /// </summary>
    public IEnumerable<string>? CC { get; set; }
    /// <summary>
    /// the blind carbon copy (BCC) recipients address
    /// </summary>
    public IEnumerable<string>? BCC { get; set; }
    /// <summary>
    /// repliers address
    /// </summary>
    public IEnumerable<string>? Repliers { get; set; }
    /// <summary>
    /// message subject
    /// </summary>
    public string? Subject { get; set; }
    /// <summary>
    /// message body
    /// </summary>
    public string? Body { get; set; }
    /// <summary>
    /// attachment list
    /// </summary>
    public IEnumerable<EmailAttachment>? Attachments { get; set; }
    /// <summary>
    /// email priority
    /// </summary>
    public MailPriority? Priority { get; set; }
    /// <summary>
    /// body encoding
    /// </summary>
    public Encoding? BodyEncoding { get; set; }
    /// <summary>
    /// header encoding
    /// </summary>
    public Encoding? HeaderEncoding { get; set; }
    /// <summary>
    /// indicate body is html
    /// </summary>
    public bool IsHtml { get; set; }
}

/// <summary>
/// email attachment
/// </summary>
public class EmailAttachment
{
    /// <summary>
    /// prevent use instantient default
    /// </summary>
    private EmailAttachment() { }

    /// <summary>
    /// instantiate email attachment
    /// </summary>
    /// <param name="path">file path</param>
    public EmailAttachment(string path)
    {
        path.EnsureFileExist();
        Path = path;
        Name = new FileInfo(path).Name;
        Bytes = File.ReadAllBytes(path);
    }

    /// <summary>
    /// instantiate email attachment
    /// </summary>
    /// <param name="name">file name</param>
    /// <param name="bytes">file binary data</param>
    public EmailAttachment(string name, byte[] bytes)
    {
        Name = name;
        Bytes = bytes;
    }

    /// <summary>
    /// file name
    /// </summary>
    public string? Name { get; set; }
    /// <summary>
    /// file data,priority is high than [Path] protperty
    /// </summary>
    public byte[]? Bytes { get; set; }
    /// <summary>
    /// file path,priority is lower than [Path] protperty
    /// </summary>
    public string? Path { get; set; }
}