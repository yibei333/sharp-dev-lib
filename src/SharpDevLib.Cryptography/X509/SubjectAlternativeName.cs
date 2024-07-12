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

/// <summary>
/// subject alternative name option
/// </summary>
public class SubjectAlternativeName
{
    /// <summary>
    /// create instance of type SubjectAlternativeName
    /// </summary>
    /// <param name="type">subject alternative name type</param>
    /// <param name="value">subject alternative value</param>
    public SubjectAlternativeName(SubjectAlternativeNameType type, string value)
    {
        Type = type;
        Value = value;
    }

    /// <summary>
    /// subject alternative name type
    /// </summary>
    public SubjectAlternativeNameType Type { get; }

    /// <summary>
    /// subject alternative value
    /// </summary>
    public string Value { get; }
}