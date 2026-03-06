namespace SharpDevLib;

/// <summary>
/// 使用HMAC SHA256算法创建JWT的请求模型
/// </summary>
/// <param name="payload">JWT负载数据，将被序列化为JSON字符串</param>
/// <param name="secret">HMAC算法使用的密钥</param>
public class JwtCreateWithHMACSHA256Request(object payload, byte[] secret) : JwtCreateRequest(JwtAlgorithm.HS256, payload, secret.HexStringEncode(), null, null)
{
}