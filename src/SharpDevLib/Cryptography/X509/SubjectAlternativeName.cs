namespace SharpDevLib;

/// <summary>
/// 主体备用名称选项，用于X.509证书扩展
/// </summary>
/// <param name="type">主体备用名称类型</param>
/// <param name="value">主体备用名称的值</param>
public class SubjectAlternativeName(SubjectAlternativeNameType type, string value)
{
    /// <summary>
    /// 获取主体备用名称类型
    /// </summary>
    public SubjectAlternativeNameType Type { get; } = type;

    /// <summary>
    /// 获取主体备用名称的值
    /// </summary>
    public string Value { get; } = value;
}