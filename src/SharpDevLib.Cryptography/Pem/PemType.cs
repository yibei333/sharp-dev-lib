namespace SharpDevLib.Cryptography;

internal enum PemType
{
    UnKnown,
    RsaPkcs1PrivateKey,
    RsaEncryptedPkcs1PrivateKey,
    RsaPkcs8PrivateKey,
    RsaEncryptedPkcs8PrivateKey,
    RsaPublicKey,
    RsaX509PublicKey,
}
