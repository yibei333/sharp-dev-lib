namespace SharpDevLib;

/// <summary>
/// subject alternative name option
/// </summary>
/// <remarks>
/// create instance of type SubjectAlternativeName
/// </remarks>
/// <param name="type">subject alternative name type</param>
/// <param name="value">subject alternative value</param>
public class SubjectAlternativeName(SubjectAlternativeNameType type, string value)
{

    /// <summary>
    /// subject alternative name type
    /// </summary>
    public SubjectAlternativeNameType Type { get; } = type;

    /// <summary>
    /// subject alternative value
    /// </summary>
    public string Value { get; } = value;
}