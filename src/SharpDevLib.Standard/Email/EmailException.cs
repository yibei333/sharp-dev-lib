namespace SharpDevLib.Extensions.Email;

/// <summary>
/// email verify exception
/// </summary>
public class EmailVerifyException : Exception
{
    /// <summary>
    /// instantient email verify exception
    /// </summary>
    /// <param name="errorMessage">error message</param>
    public EmailVerifyException(string errorMessage) : base($"email service verify failed,{errorMessage}")
    {

    }
}
