# Email

提供`Email`邮件发送功能。

##### 示例

```csharp
using SharpDevLib;

EmailHelper.SetConfig(new EmailConfig
{
    Host="smtp.qq.com",
    Port=587,
    Sender="xx@qq.com",
    SenderPassword="xx",
    SenderDisplayName="test user",
    UseSSL=true
});

//https://10minutemail.com/?lang=zh
await new EmailContent(["hdodeaeokgfaujkrvk@nespf.com"],"test subject","test body").SendAsync();
```

## 相关文档
- [网络传输](../README.md#网络传输)
