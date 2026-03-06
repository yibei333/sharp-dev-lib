namespace SharpDevLib;

/// <summary>
/// 邮件验证异常
/// </summary>
/// <remarks>
/// 实例化邮件验证异常
/// </remarks>
/// <param name="errorMessage">error message</param>
public class EmailVerifyException(string errorMessage) : Exception($"email service verify failed,{errorMessage}")
{
}
