namespace SharpDevLib.Standard;

/// <summary>
/// 邮件配置
/// </summary>
public class EmailOptions
{
    /// <summary>
    /// 主机(一般为smtp.xx.com)
    /// </summary>
    public string? Host { get; set; }

    /// <summary>
    /// 端口(一般为25,587,465)
    /// </summary>
    public int Port { get; set; }

    /// <summary>
    /// 是否使用ssl
    /// </summary>
    public bool UseSSL { get; set; }

    /// <summary>
    /// 发件人地址
    /// </summary>
    public string? Sender { get; set; }

    /// <summary>
    /// 发件人密码(有些邮箱为单独的授权码)
    /// </summary>
    public string? SenderPassword { get; set; }

    /// <summary>
    /// 发件人显示名称
    /// </summary>
    public string? SenderDisplayName { get; set; }
}

/// <summary>
/// 全局邮件配置
/// </summary>
public static class EmailGlobalOptions
{
    /// <summary>
    /// 主机(一般为smtp.xx.com)
    /// </summary>
    public static string? Host { get; set; }

    /// <summary>
    /// 端口(一般为25,587,465)
    /// </summary>
    public static int Port { get; set; }

    /// <summary>
    /// 是否使用ssl
    /// </summary>
    public static bool UseSSL { get; set; }

    /// <summary>
    /// 发件人地址
    /// </summary>
    public static string? Sender { get; set; }

    /// <summary>
    /// 发件人密码(有些邮箱为单独的授权码)
    /// </summary>
    public static string? SenderPassword { get; set; }

    /// <summary>
    /// 发件人显示名称
    /// </summary>
    public static string? SenderDisplayName { get; set; }
}