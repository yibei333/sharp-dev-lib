namespace SharpDevLib;

/// <summary>
/// JSON属性命名格式枚举
/// </summary>
public enum JsonNameFormat
{
    /// <summary>
    /// 小驼峰命名，如SomeProperty->someProperty
    /// </summary>
    CamelCaseLower,
    /// <summary>
    /// 大驼峰命名，如SomeProperty->SomeProperty
    /// </summary>
    CamelCaseUpper,
    /// <summary>
    /// 短横线命名小写，如SomeProperty->some-property
    /// </summary>
    KebabCaseLower,
    /// <summary>
    /// 短横线命名大写，如SomeProperty->SOME-PROPERTY
    /// </summary>
    KebabCaseUpper,
    /// <summary>
    /// 蛇形命名小写，如SomeProperty->some_property
    /// </summary>
    SnakeCaseLower,
    /// <summary>
    /// 蛇形命名大写，如SomeProperty->SOME_PROPERTY
    /// </summary>
    SnakeCaseUpper
}