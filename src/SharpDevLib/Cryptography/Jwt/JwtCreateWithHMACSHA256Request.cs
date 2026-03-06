namespace SharpDevLib;

/// <summary>
/// 使用HMACSHA256算法创建JWT请求模型
/// </summary>
/// <remarks>
/// 实例化请求模型
/// </remarks>
/// <param name="payload">载荷</param>
/// <param name="secret">密钥</param>
public class JwtCreateWithHMACSHA256Request(object payload, byte[] secret) : JwtCreateRequest(JwtAlgorithm.HS256, payload, secret.HexStringEncode(), null, null)
{
}