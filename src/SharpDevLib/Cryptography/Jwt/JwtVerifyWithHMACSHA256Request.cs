namespace SharpDevLib;

/// <summary>
/// 使用HMACSHA256算法验证JWT请求模型
/// </summary>
/// <remarks>
/// 实例化请求模型
/// </remarks>
/// <param name="token">token</param>
/// <param name="secret">密钥</param>
public class JwtVerifyWithHMACSHA256Request(string token, byte[] secret) : JwtVerifyRequest(JwtAlgorithm.HS256, token, secret.HexStringEncode(), null)
{
}