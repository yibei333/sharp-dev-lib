using SharpDevLib.Cryptography.Internal.References;

namespace SharpDevLib.Cryptography;

/// <summary>
/// 使用HMACSHA256算法验证JWT请求模型
/// </summary>
public class JwtVerifyWithHMACSHA256Request : JwtVerifyRequest
{
    /// <summary>
    /// 实例化请求模型
    /// </summary>
    /// <param name="token">token</param>
    /// <param name="secret">密钥</param>
    public JwtVerifyWithHMACSHA256Request(string token, byte[] secret) : base(JwtAlgorithm.HS256, token, secret.HexStringEncode(), null)
    {

    }
}