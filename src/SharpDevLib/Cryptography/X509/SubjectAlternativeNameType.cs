namespace SharpDevLib;

/// <summary>
/// 主体备用名称类型枚举
/// </summary>
public enum SubjectAlternativeNameType
{
    /// <summary>
    /// DNS名称
    /// </summary>
    Dns,
    /// <summary>
    /// IP地址
    /// </summary>
    IP,
    /// <summary>
    /// URI地址
    /// </summary>
    Uri,
    /// <summary>
    /// 电子邮件地址
    /// </summary>
    Email,
    /// <summary>
    /// 用户主体名称（User Principal Name）
    /// </summary>
    UPN
}