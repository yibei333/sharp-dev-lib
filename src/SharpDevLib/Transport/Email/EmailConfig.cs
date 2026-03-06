namespace SharpDevLib;

/// <summary>
/// 邮件配置
/// </summary>
public class EmailConfig
{
    /// <summary>
    /// SMTP服务器地址
    /// </summary>
    /// <remarks>一般为smtp.xx.com格式</remarks>
    public string? Host { get; set; }

    /// <summary>
    /// SMTP服务器端口
    /// </summary>
    /// <remarks>常用端口：25（不加密）、587（TLS）、465（SSL）</remarks>
    public int Port { get; set; }

    /// <summary>
    /// 是否使用SSL安全连接
    /// </summary>
    public bool UseSSL { get; set; }

    /// <summary>
    /// 发件人地址
    /// </summary>
    public string? Sender { get; set; }

    /// <summary>
    /// 发件人邮箱密码
    /// </summary>
    /// <remarks>部分邮箱服务提供商使用单独的授权码而非登录密码</remarks>
    public string? SenderPassword { get; set; }

    /// <summary>
    /// 发件人显示名称
    /// </summary>
    public string? SenderDisplayName { get; set; }
}