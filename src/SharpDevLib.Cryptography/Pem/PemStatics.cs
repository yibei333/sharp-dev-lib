namespace SharpDevLib.Cryptography;

internal static class PemStatics
{
    public const string RsaPkcs1PrivateStart = "-----BEGIN RSA PRIVATE KEY-----";
    public const string RsaPkcs1PrivateEnd = "-----END RSA PRIVATE KEY-----";

    public const string RsaPkcs8PrivateStart = "-----BEGIN PRIVATE KEY-----";
    public const string RsaPkcs8PrivateEnd = "-----END PRIVATE KEY-----";

    public const string RsaEncryptedPkcs8PrivateStart = "-----BEGIN ENCRYPTED PRIVATE KEY-----";
    public const string RsaEncryptedPkcs8PrivateEnd = "-----END ENCRYPTED PRIVATE KEY-----";

    public const string RsaPublicStart = "-----BEGIN PUBLIC KEY-----";
    public const string RsaPublicEnd = "-----END PUBLIC KEY-----";
}
