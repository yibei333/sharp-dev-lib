# Email - 邮件发送

提供邮件发送功能。

## 类

### EmailHelper

邮件帮助类，提供邮件发送功能。

### EmailMessage

邮件消息类，用于配置邮件内容。

## 扩展方法

### SendAsync

发送邮件。

#### 方法签名

```csharp
public static async Task SendAsync(this EmailMessage message)
```

#### 参数

| 参数 | 类型 | 说明 |
| --- | --- | --- |
| message | EmailMessage | 邮件消息 |

#### 返回值

表示异步发送任务的 Task。

## 示例

### 发送简单邮件

```csharp
var message = new EmailMessage
{
    From = "sender@example.com",
    To = "recipient@example.com",
    Subject = "测试邮件",
    Body = "这是一封测试邮件"
};
await message.SendAsync();
```

### 发送带附件的邮件

```csharp
var message = new EmailMessage
{
    From = "sender@example.com",
    To = "recipient@example.com",
    Subject = "带附件的邮件",
    Body = "请查收附件",
    Attachments = new[] { "document.pdf", "image.jpg" }
};
await message.SendAsync();
```

### 发送 HTML 邮件

```csharp
var message = new EmailMessage
{
    From = "sender@example.com",
    To = "recipient@example.com",
    Subject = "HTML 邮件",
    Body = "<h1>欢迎</h1><p>这是一封 HTML 邮件</p>",
    IsHtml = true
};
await message.SendAsync();
```

## 特性

- 支持发送文本邮件
- 支持发送 HTML 邮件
- 支持发送带附件的邮件
- 异步操作
