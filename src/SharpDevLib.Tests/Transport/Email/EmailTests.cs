using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MimeKit;
using SharpDevLib.Tests.Transport.Email.EmailHost;
using SharpDevLib.Tests.Transport.Email.EmailHost.Service;
using SharpDevLib.Transport;
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace SharpDevLib.Tests.Transport.Email;

[TestClass]
public class EmailTests
{
    static SmtpHost? _smtpHost;
    static Pop3Host? _pop3Host;
    static EmailUserService? _emailUserService;
    static EmailSerivce? _emailService;
    static EmailDetailSerivce? _emailDetailService;
    static readonly string _baseDirectory = AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/Email");

    [ClassInitialize(InheritanceBehavior.BeforeEachDerivedClass)]
    public static void Initialize(TestContext context)
    {
        if (Directory.Exists(_baseDirectory)) Directory.Delete(_baseDirectory, true);
        Console.WriteLine(context is null);
        IServiceCollection services = new ServiceCollection();
        services.AddSingleton<SmtpHost>();
        services.AddSingleton<Pop3Host>();
        services.AddSingleton<EmailUserService>();
        services.AddSingleton<EmailSerivce>();
        services.AddSingleton<EmailDetailSerivce>();

        var provider = services.BuildServiceProvider();
        _smtpHost = provider.GetRequiredService<SmtpHost>();
        _pop3Host = provider.GetRequiredService<Pop3Host>();
        _emailUserService = provider.GetRequiredService<EmailUserService>();
        _emailService = provider.GetRequiredService<EmailSerivce>();
        _emailDetailService = provider.GetRequiredService<EmailDetailSerivce>();

        _smtpHost.StartAsync();
        _pop3Host.StartAsync();
    }

    [ClassCleanup(InheritanceBehavior.BeforeEachDerivedClass)]
    public static void Cleanup()
    {
        _smtpHost?.Stop();
        _pop3Host?.Stop();
    }

    [TestMethod]
    public void SendTest()
    {
        var options = new EmailOptions
        {
            Host = "localhost",
            Port = 25,
            Sender = "foo@localhost",
            SenderDisplayName = "foo",
            SenderPassword = "foo_password"
        };
        var content = new EmailContent(["bar@localhost"], "send test", "testbody")
        {
            Attachments = [
                new EmailAttachment(AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/TestFile.txt"))
            ],
            CC = ["baz@localhost"],
            BCC = ["qux@localhost"]
        };
        options.Send(content);

        Assert.IsNotNull(_emailUserService);
        Assert.IsNotNull(_emailService);
        Assert.IsNotNull(_emailDetailService);

        var email = (from a in _emailService.All join b in _emailDetailService.All on a.Id equals b.EmailId join c in _emailUserService.All on b.UserId equals c.Id where b.Type == 1 && c.Name == options.SenderDisplayName && a.Subject == content.Subject select a).FirstOrDefault();
        Assert.IsNotNull(email);
        Assert.IsTrue(File.Exists(email.FilePath));

        using var stream = File.OpenRead(email.FilePath);
        using var message = MimeMessage.Load(new ParserOptions { CharsetEncoding = Encoding.UTF8 }, stream);
        Assert.IsNotNull(message);

        Console.WriteLine(message.ToString());

        Assert.IsTrue(message.Body.ToString().Contains(content.Body!));
        Assert.AreEqual(content.Receivers?.First(), message.To.First().ToString());
        Assert.AreEqual(content.CC?.First(), message.Cc.First().ToString());
        Assert.AreEqual(true, message.Bcc.IsNullOrEmpty());
        var bccEmail = (from a in _emailService.All join b in _emailDetailService.All on a.Id equals b.EmailId join c in _emailUserService.All on b.UserId equals c.Id where b.Type == 2 && c.Name == "qux" && a.Subject == content.Subject select a).FirstOrDefault();
        Assert.IsNotNull(bccEmail);

        Assert.AreEqual(1, message.Attachments.Count());
        using var attachment = message.Attachments.First() as MimePart;
        Assert.IsNotNull(attachment);
        using var attachStream = new MemoryStream();
        attachment.Content.WriteTo(attachStream);
        var attachmentText = attachStream.ToArray().ToUtf8String();
        Assert.AreEqual("Hello,World!", attachmentText.Base64Decode().ToUtf8String());
    }

    [TestMethod]
    public void SendFromServiceTest()
    {
        EmailGlobalOptions.Host = "localhost";
        EmailGlobalOptions.Port = 25;
        EmailGlobalOptions.Sender = "baz@localhost";
        EmailGlobalOptions.SenderDisplayName = "baz";
        EmailGlobalOptions.SenderPassword = "baz_password";

        IServiceCollection services = new ServiceCollection();
        services.AddEmail();
        var serviceProvider = services.BuildServiceProvider().CreateScope().ServiceProvider;
        var service = serviceProvider.GetRequiredService<IEmailService>();

        var content = new EmailContent(["qux@localhost"], "send test", "testbody")
        {
            Attachments = [
                new EmailAttachment(AppDomain.CurrentDomain.BaseDirectory.CombinePath("TestData/TestFile.txt"))
            ],
            CC = ["foo@localhost"],
            BCC = ["bar@localhost"]
        };
        service.Send(content);

        Assert.IsNotNull(_emailUserService);
        Assert.IsNotNull(_emailService);
        Assert.IsNotNull(_emailDetailService);

        var email = (from a in _emailService.All join b in _emailDetailService.All on a.Id equals b.EmailId join c in _emailUserService.All on b.UserId equals c.Id where b.Type == 1 && c.Name == EmailGlobalOptions.SenderDisplayName && a.Subject == content.Subject select a).FirstOrDefault();
        Assert.IsNotNull(email);
        Assert.IsTrue(File.Exists(email.FilePath));

        using var stream = File.OpenRead(email.FilePath);
        using var message = MimeMessage.Load(new ParserOptions { CharsetEncoding = Encoding.UTF8 }, stream);
        Assert.IsNotNull(message);

        Console.WriteLine(message.ToString());

        Assert.IsTrue(message.Body.ToString().Contains(content.Body!));
        Assert.AreEqual(content.Receivers?.First(), message.To.First().ToString());
        Assert.AreEqual(content.CC?.First(), message.Cc.First().ToString());
        Assert.AreEqual(true, message.Bcc.IsNullOrEmpty());
        var bccEmail = (from a in _emailService.All join b in _emailDetailService.All on a.Id equals b.EmailId join c in _emailUserService.All on b.UserId equals c.Id where b.Type == 2 && c.Name == "qux" && a.Subject == content.Subject select a).FirstOrDefault();
        Assert.IsNotNull(bccEmail);

        Assert.AreEqual(1, message.Attachments.Count());
        using var attachment = message.Attachments.First() as MimePart;
        Assert.IsNotNull(attachment);
        using var attachStream = new MemoryStream();
        attachment.Content.WriteTo(attachStream);
        var attachmentText = attachStream.ToArray().ToUtf8String();
        Assert.AreEqual("Hello,World!", attachmentText.Base64Decode().ToUtf8String());
    }
}
