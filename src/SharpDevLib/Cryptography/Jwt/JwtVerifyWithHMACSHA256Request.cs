namespace SharpDevLib;

/// <summary>
/// 使用HMAC SHA256算法验证JWT的请求模型
/// </summary>
/// <param name="token">要验证的JWT令牌字符串</param>
/// <param name="secret">HMAC算法使用的密钥</param>
public class JwtVerifyWithHMACSHA256Request(string token, byte[] secret) : JwtVerifyRequest(JwtAlgorithm.HS256, token, secret.HexStringEncode(), null)
{
}