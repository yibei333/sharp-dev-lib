namespace SharpDevLib.Cryptography;

internal static class PemStatics
{
    public const string RsaPkcs1PrivateStart = "-----BEGIN RSA PRIVATE KEY-----";
    public const string RsaPkcs1PrivateEnd = "-----END RSA PRIVATE KEY-----";

    public const string RsaPkcs8PrivateStart = "-----BEGIN PRIVATE KEY-----";
    public const string RsaPkcs8PrivateEnd = "-----END PRIVATE KEY-----";

    public const string RsaEncryptedPkcs8PrivateStart = "-----BEGIN ENCRYPTED PRIVATE KEY-----";
    public const string RsaEncryptedPkcs8PrivateEnd = "-----END ENCRYPTED PRIVATE KEY-----";

    public const string RsaPublicStart = "-----BEGIN RSA PUBLIC KEY-----";
    public const string RsaPublicEnd = "-----END RSA PUBLIC KEY-----";

    public const string RsaX509SubjectPublicStart = "-----BEGIN PUBLIC KEY-----";
    public const string RsaX509SubjectPublicEnd = "-----END PUBLIC KEY-----";

    public const string X509CertificateStart = "-----BEGIN CERTIFICATE-----";
    public const string X509CertificateEnd = "-----END CERTIFICATE-----";
}
