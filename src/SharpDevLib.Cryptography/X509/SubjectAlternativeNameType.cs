namespace SharpDevLib.Cryptography;

/// <summary>
/// subject alternative name type
/// </summary>
public enum SubjectAlternativeNameType
{
    /// <summary>
    /// dns
    /// </summary>
    Dns,
    /// <summary>
    /// ip address
    /// </summary>
    IP,
    /// <summary>
    /// uri
    /// </summary>
    Uri,
    /// <summary>
    /// email
    /// </summary>
    Email,
    /// <summary>
    /// user pricipal name
    /// </summary>
    UPN
}