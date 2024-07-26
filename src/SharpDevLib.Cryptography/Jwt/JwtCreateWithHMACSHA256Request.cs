namespace SharpDevLib.Cryptography;

/// <summary>
/// 使用HMACSHA256算法创建JWT请求模型
/// </summary>
public class JwtCreateWithHMACSHA256Request : JwtCreateRequest
{
    /// <summary>
    /// 实例化请求模型
    /// </summary>
    /// <param name="payload">载荷</param>
    /// <param name="secret">密钥</param>
    public JwtCreateWithHMACSHA256Request(object payload, byte[] secret) : base(JwtAlgorithm.HS256, payload, secret.HexStringEncode(), null, null)
    {

    }
}