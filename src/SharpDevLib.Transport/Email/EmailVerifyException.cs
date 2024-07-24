namespace SharpDevLib.Transport;

/// <summary>
/// 邮件验证异常
/// </summary>
public class EmailVerifyException : Exception
{
    /// <summary>
    /// 实例化邮件验证异常
    /// </summary>
    /// <param name="errorMessage">error message</param>
    public EmailVerifyException(string errorMessage) : base($"email service verify failed,{errorMessage}")
    {
    }
}
