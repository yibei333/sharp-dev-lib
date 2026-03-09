# Email

提供`Email`邮件发送功能。

##### 实例

```csharp
using SharpDevLib;
using System.Net.Mail;
using System.Text;

//配置邮件服务器
EmailHelper.SetConfig(new EmailConfig
{
    Host = "smtp.example.com",
    Port = 587,
    UseSSL = true,
    Sender = "noreply@example.com",
    SenderPassword = "password123",
    SenderDisplayName = "示例系统"
});

//发送简单文本邮件
var content = new EmailContent(
    ["recipient@example.com"],
    "测试邮件",
    "这是一封测试邮件"
);
await content.SendAsync();

//发送给多个收件人
var content = new EmailContent(
    ["user1@example.com", "user2@example.com", "user3@example.com"],
    "群发邮件",
    "这是一封群发邮件"
);
await content.SendAsync();

//发送带抄送的邮件
var content = new EmailContent(
    ["recipient@example.com"],
    "测试邮件",
    "邮件内容"
)
{
    CC = ["cc1@example.com", "cc2@example.com"]
};
await content.SendAsync();

//发送带密送的邮件
var content = new EmailContent(
    ["recipient@example.com"],
    "测试邮件",
    "邮件内容"
)
{
    BCC = ["bcc@example.com"]
};
await content.SendAsync();

//发送带回复人的邮件
var content = new EmailContent(
    ["recipient@example.com"],
    "测试邮件",
    "邮件内容"
)
{
    Repliers = ["reply@example.com"]
};
await content.SendAsync();

//发送HTML格式邮件
var content = new EmailContent(
    ["recipient@example.com"],
    "HTML邮件",
    "<h1>欢迎</h1><p>这是一封HTML格式的邮件</p>"
)
{
    IsHtml = true
};
await content.SendAsync();

//发送带附件的邮件（从文件）
var content = new EmailContent(
    ["recipient@example.com"],
    "带附件的邮件",
    "请查收附件"
)
{
    Attachments = new[]
    {
        new EmailAttachment("document.pdf"),
        new EmailAttachment("image.jpg")
    }
};
await content.SendAsync();

//发送带附件的邮件（从字节数组）
var fileBytes = File.ReadAllBytes("report.xlsx");
var content = new EmailContent(
    ["recipient@example.com"],
    "带附件的邮件",
    "请查收附件"
)
{
    Attachments = new[]
    {
        new EmailAttachment("report.xlsx", fileBytes)
    }
};
await content.SendAsync();

//设置邮件优先级
var content = new EmailContent(
    ["recipient@example.com"],
    "重要邮件",
    "这是一封重要邮件"
)
{
    Priority = MailPriority.High
};
await content.SendAsync();

//设置邮件编码
var content = new EmailContent(
    ["recipient@example.com"],
    "编码测试",
    "测试邮件编码"
)
{
    BodyEncoding = Encoding.UTF8,
    HeaderEncoding = Encoding.UTF8
};
await content.SendAsync();

//发送完整邮件
var content = new EmailContent(
    ["recipient@example.com"],
    "完整邮件",
    "<h1>欢迎</h1><p>这是一封完整邮件</p>"
)
{
    IsHtml = true,
    CC = ["cc@example.com"],
    BCC = ["bcc@example.com"],
    Repliers = ["reply@example.com"],
    Priority = MailPriority.High,
    BodyEncoding = Encoding.UTF8,
    HeaderEncoding = Encoding.UTF8,
    Attachments = new[]
    {
        new EmailAttachment("document.pdf"),
        new EmailAttachment("report.xlsx", File.ReadAllBytes("report.xlsx"))
    }
};
await content.SendAsync();

//使用授权码（Gmail示例）
EmailHelper.SetConfig(new EmailConfig
{
    Host = "smtp.gmail.com",
    Port = 587,
    UseSSL = true,
    Sender = "yourname@gmail.com",
    SenderPassword = "your_app_password",  // 使用应用专用密码
    SenderDisplayName = "您的名称"
});
var content = new EmailContent(
    ["recipient@gmail.com"],
    "Gmail测试",
    "通过Gmail发送的邮件"
)
{
    IsHtml = true
};
await content.SendAsync();

//使用QQ邮箱（端口465）
EmailHelper.SetConfig(new EmailConfig
{
    Host = "smtp.qq.com",
    Port = 465,
    UseSSL = true,
    Sender = "yourname@qq.com",
    SenderPassword = "your_authorization_code",  // QQ邮箱授权码
    SenderDisplayName = "您的名称"
});
var content = new EmailContent(
    ["recipient@qq.com"],
    "QQ邮箱测试",
    "通过QQ邮箱发送的邮件"
);
await content.SendAsync();

//使用企业邮箱（端口25，不加密）
EmailHelper.SetConfig(new EmailConfig
{
    Host = "smtp.company.com",
    Port = 25,
    UseSSL = false,
    Sender = "noreply@company.com",
    SenderPassword = "password",
    SenderDisplayName = "公司系统"
});
var content = new EmailContent(
    ["employee@company.com"],
    "内部通知",
    "这是一封内部通知邮件"
);
await content.SendAsync();

//批量发送邮件（使用循环）
var recipients = new[]
{
    "user1@example.com",
    "user2@example.com",
    "user3@example.com"
};
foreach (var recipient in recipients)
{
    var content = new EmailContent(
        [recipient],
        "个人通知",
        $"您好 {recipient}，这是您的个人通知"
    );
    await content.SendAsync();
    Console.WriteLine($"已发送至: {recipient}");
}
```

## 相关文档
- [网络传输](../README.md#网络传输)
