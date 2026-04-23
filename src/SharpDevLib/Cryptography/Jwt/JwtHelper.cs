using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace SharpDevLib;

/// <summary>
/// JWT（JSON Web Token）扩展，提供JWT的创建和验证功能
/// </summary>
public static class JwtHelper
{
    /// <summary>
    /// 使用HMACSHA256算法创建JWT
    /// </summary>
    /// <param name="payload">JWT负载数据，将被序列化为JSON字符串</param>
    /// <param name="secret">HMAC算法使用的密钥</param>
    /// <returns>JWT令牌字符串</returns>
    public static string CreateWithHmacSha256(object payload, byte[] secret) => InternalCreate(new JwtCreateRequest(JwtAlgorithm.HS256, payload, secret.HexStringEncode(), null, null));

    /// <summary>
    /// 使用RSA SHA256算法创建JWT
    /// </summary>
    /// <param name="payload">JWT负载数据，将被序列化为JSON字符串</param>
    /// <param name="pemKey">PEM格式的RSA私钥</param>
    /// <param name="keyPassword">私钥密码，仅当私钥受密码保护时需要提供</param>
    /// <param name="padding">RSA签名填充方式，默认使用Pkcs1</param>
    /// <returns>JWT令牌字符串</returns>
    public static string CreateWithRsaSha256(object payload, string pemKey, byte[]? keyPassword = null, RSASignaturePadding? padding = null) => InternalCreate(new JwtCreateRequest(JwtAlgorithm.RS256, payload, pemKey, keyPassword, padding));

    /// <summary>
    /// 使用HMACSHA256算法验证JWT
    /// </summary>
    /// <param name="token">要验证的JWT令牌字符串</param>
    /// <param name="secret">HMAC算法使用的密钥</param>
    /// <returns>JWT验证结果，包含验证状态和相关信息</returns>
    public static JwtVerifyResult VerifyWithHmacSha256(string token, byte[] secret) => InternalVerify(new JwtVerifyRequest(JwtAlgorithm.HS256, token, secret.HexStringEncode(), null));

    /// <summary>
    /// 使用RSA SHA256算法验证JWT
    /// </summary>
    /// <param name="token">要验证的JWT令牌字符串</param>
    /// <param name="pemKey">PEM格式的RSA公钥</param>
    /// <param name="padding">RSA签名填充方式，默认使用Pkcs1</param>
    /// <returns>JWT验证结果，包含验证状态和相关信息</returns>
    public static JwtVerifyResult VerifyWithRsaSha256(string token, string pemKey, RSASignaturePadding? padding = null) => InternalVerify(new JwtVerifyRequest(JwtAlgorithm.RS256, token, pemKey, padding));

    static string InternalCreate(JwtCreateRequest request)
    {
        var header = CreateHeader(request.Algorithm);
        var payload = CreatePayload(request.Payload);
        var signature = CreateSignature(header, payload, request.Algorithm, request.Key, request.KeyPassword, request.Padding);
        return $"{header}.{payload}.{signature}";
    }

    static string CreateHeader(JwtAlgorithm algorithm)
    {
        if (!Enum.IsDefined(typeof(JwtAlgorithm), algorithm)) throw new NotSupportedException($"暂不支持的算法: {algorithm}");
        var header = new JwtHeader(algorithm.ToString(), "JWT");
        var headerSegment = JsonSerializer.Serialize(header).Utf8Decode().Base64UrlEncode();
        return headerSegment;
    }

    static string CreatePayload(object payload)
    {
        if (payload is null) throw new ArgumentNullException(nameof(payload));
        return JsonSerializer.Serialize(payload).Utf8Decode().Base64UrlEncode();
    }

    static string CreateSignature(string header, string payload, JwtAlgorithm algorithm, string key, byte[]? keyPassword, RSASignaturePadding? padding)
    {
        if (algorithm == JwtAlgorithm.HS256)
        {
            return new HMACSHA256(key.HexStringDecode()).ComputeHash($"{header}.{payload}".Utf8Decode()).Base64UrlEncode();
        }
        else if (algorithm == JwtAlgorithm.RS256)
        {
            using var rsa = RSA.Create();
            rsa.ImportPem(key, keyPassword);
            return rsa.SignData(Encoding.UTF8.GetBytes($"{header}.{payload}"), HashAlgorithmName.SHA256, padding ?? RSASignaturePadding.Pkcs1).Base64UrlEncode();
        }
        else
        {
            throw new NotImplementedException();
        }
    }

    static JwtVerifyResult InternalVerify(JwtVerifyRequest request)
    {
        if (request.Token.IsNullOrWhiteSpace()) return new JwtVerifyResult(false);
        var str = request.Token.Split(['.'], StringSplitOptions.RemoveEmptyEntries);
        if (str.Length != 3) return new JwtVerifyResult(false);

        var headerSegment = str[0];
        var payloadSegment = str[1];
        var signatureSegment = str[2];
        var header = headerSegment.Base64UrlDecode().Utf8Encode();
        var payload = payloadSegment.Base64UrlDecode().Utf8Encode();

        var headerObject = JsonSerializer.Deserialize<JwtHeader>(headerSegment.Base64UrlDecode()) ?? throw new NullReferenceException($"无法反序列化JWT头部");
        if (headerObject.JwtAlgorithm == JwtAlgorithm.HS256)
        {
            var signatureToVerify = new HMACSHA256(request.Key.HexStringDecode()).ComputeHash($"{headerSegment}.{payloadSegment}".Utf8Decode()).Base64UrlEncode();
            var verified = signatureToVerify == signatureSegment;
            return new JwtVerifyResult(verified, JwtAlgorithm.HS256, header, payload, signatureSegment);
        }
        else if (headerObject.JwtAlgorithm == JwtAlgorithm.RS256)
        {
            using var rsa = RSA.Create();
            rsa.ImportPem(request.Key);
            var verified = rsa.VerifyData($"{headerSegment}.{payloadSegment}".Utf8Decode(), signatureSegment.Base64UrlDecode(), HashAlgorithmName.SHA256, request.Padding ?? RSASignaturePadding.Pkcs1);
            return new JwtVerifyResult(verified, JwtAlgorithm.RS256, header, payload, signatureSegment);
        }
        else
        {
            throw new NotImplementedException();
        }
    }
}
