using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SharpDevLib.Cryptography;

/// <summary>
/// jwt扩展
/// </summary>
public static class JwtExtension
{
    /// <summary>
    /// 创建jwt
    /// </summary>
    /// <param name="request">使用HMACSHA256算法创建JWT请求模型</param>
    /// <returns>jwt</returns>
    public static string Create(this JwtCreateWithHMACSHA256Request request) => InternalCreate(request);

    /// <summary>
    /// 创建jwt
    /// </summary>
    /// <param name="request">使用RSA SHA256算法创建JWT请求模型</param>
    /// <returns>jwt</returns>
    public static string Create(this JwtCreateWithRS256Request request) => InternalCreate(request);

    /// <summary>
    /// 验证jwt
    /// </summary>
    /// <param name="request">使用HMACSHA256算法验证JWT请求模型</param>
    /// <returns>jwt验签结果</returns>
    public static JwtVerifyResult Verify(this JwtVerifyWithHMACSHA256Request request) => InternalVerify(request);

    /// <summary>
    /// 验证jwt
    /// </summary>
    /// <param name="request">使用RSA SHA256算法验证JWT请求模型</param>
    /// <returns>jwt验签结果</returns>
    public static JwtVerifyResult Verify(this JwtVerifyWithRS256Request request) => InternalVerify(request);

    static string InternalCreate(JwtCreateRequest request)
    {
        var header = CreateHeader(request.Algorithm);
        var payload = CreatePayload(request.Payload);
        var signature = CreateSignature(header, payload, request.Algorithm, request.Key, request.KeyPassword, request.Padding);
        return $"{header}.{payload}.{signature}";
    }

    static string CreateHeader(JwtAlgorithm algorithm)
    {
        if (!Enum.IsDefined(typeof(JwtAlgorithm), algorithm)) throw new NotSupportedException($"{algorithm} not supported yet");
        var header = new JwtHeader(algorithm.ToString(), "JWT");
        var headerSegment = JsonSerializer.Serialize(header).ToUtf8Bytes().Base64UrlEncode();
        return headerSegment;
    }

    static string CreatePayload(object payload)
    {
        if (payload is null) throw new ArgumentNullException(nameof(payload));
        return JsonSerializer.Serialize(payload).ToUtf8Bytes().Base64UrlEncode();
    }

    static string CreateSignature(string header, string payload, JwtAlgorithm algorithm, string key, byte[]? keyPassword, RSASignaturePadding? padding)
    {
        if (algorithm == JwtAlgorithm.HS256)
        {
            return new HMACSHA256(key.FromHexString()).ComputeHash($"{header}.{payload}".ToUtf8Bytes()).Base64UrlEncode();
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
        var str = request.Token.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
        if (str.Length != 3) return new JwtVerifyResult(false);

        var headerSegment = str[0];
        var payloadSegment = str[1];
        var signatureSegment = str[2];

        var headerObject = JsonSerializer.Deserialize<JwtHeader>(headerSegment.Base64UrlDecode()) ?? throw new NullReferenceException($"unable to deserialize jwt header");
        if (headerObject.JwtAlgorithm == JwtAlgorithm.HS256)
        {
            var signatureToVerify = new HMACSHA256(request.Key.FromHexString()).ComputeHash($"{headerSegment}.{payloadSegment}".ToUtf8Bytes()).Base64UrlEncode();
            var verified = signatureToVerify == signatureSegment;
            return verified ? new JwtVerifyResult(true, headerObject.JwtAlgorithm, headerSegment.Base64UrlDecode().ToUtf8String(), payloadSegment.Base64UrlDecode().ToUtf8String(), signatureSegment) : new JwtVerifyResult(false);
        }
        else if (headerObject.JwtAlgorithm == JwtAlgorithm.RS256)
        {
            using var rsa = RSA.Create();
            rsa.ImportPem(request.Key);
            var isVerified = rsa.VerifyData($"{headerSegment}.{payloadSegment}".ToUtf8Bytes(), signatureSegment.Base64UrlDecode(), HashAlgorithmName.SHA256, request.Padding ?? RSASignaturePadding.Pkcs1);
            return isVerified ? new JwtVerifyResult(true, headerObject.JwtAlgorithm, headerSegment.Base64UrlDecode().ToUtf8String(), payloadSegment.Base64UrlDecode().ToUtf8String(), signatureSegment) : new JwtVerifyResult(false);
        }
        else
        {
            throw new NotImplementedException();
        }
    }

    class JwtHeader
    {
        public JwtHeader(string algorithm, string type)
        {
            Algorithm = algorithm;
            Type = type;

            if (!Enum.TryParse<JwtAlgorithm>(algorithm, out var value)) throw new NotSupportedException($"algorithm '{algorithm}' not supported yet");
            JwtAlgorithm = value;
        }

        [JsonPropertyName("alg")]
        public string Algorithm { get; set; }

        [JsonPropertyName("typ")]
        public string Type { get; set; }

        [JsonIgnore]
        public JwtAlgorithm JwtAlgorithm { get; }
    }
}
