namespace SharpDevLib;

/// <summary>
/// 邮件验证异常
/// </summary>
/// <param name="errorMessage">错误消息</param>
public class EmailVerifyException(string errorMessage) : Exception($"邮件服务验证失败,{errorMessage}")
{
}
