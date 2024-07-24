using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SharpDevLib;

/// <summary>
/// json选项
/// </summary>
public class JsonOption
{
    readonly Dictionary<string, JsonSerializerOptions> _cache = new();
    static readonly AlphabeticalOrderContractResolver _propertyNameOrderResolver = new();
    internal static JsonOption DefaultFormatJson = new() { FormatJson = true };
    internal static JsonOption DefaultFormatJsonWithoutOrder = new() { FormatJson = true, OrderByNameProperty = false };
    internal static JsonOption DefaultCompressJson = new() { FormatJson = false };
    internal static JsonOption DefaultCompressJsonWithoutOrder = new() { FormatJson = false, OrderByNameProperty = false };

    /// <summary>
    /// 默认选项
    /// </summary>
    public static JsonOption Default = new();

    /// <summary>
    /// 默认格式化选项
    /// </summary>
    public static JsonOption DefaultWithFormat = new() { FormatJson = true };

    /// <summary>
    /// 默认小驼峰选项
    /// </summary>
    public static JsonOption DefaultWithCamelCaseLower = new() { NameFormat = JsonNameFormat.CamelCaseLower };

    /// <summary>
    /// 是否格式华Json,默认为false
    /// </summary>
    public bool FormatJson { get; set; }
    /// <summary>
    /// 是否忽略大小写,默认为true
    /// </summary>
    public bool CaseInsensitive { get; set; } = true;
    /// <summary>
    /// 命名格式,默认为大驼峰
    /// </summary>
    public JsonNameFormat NameFormat { get; set; } = JsonNameFormat.CamelCaseUpper;
    /// <summary>
    /// 是否根据属性名称排序,默认为true
    /// </summary>
    public bool OrderByNameProperty { get; set; } = true;

    internal JsonSerializerOptions Create()
    {
        var key = this.ToString();
        if (_cache.TryGetValue(key, out var value)) return value;

        var namePolicy = NameFormat switch
        {
            JsonNameFormat.CamelCaseLower => JsonNamingPolicy.CamelCase,
            JsonNameFormat.KebabCaseLower => JsonNamingPolicy.KebabCaseLower,
            JsonNameFormat.KebabCaseUpper => JsonNamingPolicy.KebabCaseUpper,
            JsonNameFormat.SnakeCaseLower => JsonNamingPolicy.SnakeCaseLower,
            JsonNameFormat.SnakeCaseUpper => JsonNamingPolicy.SnakeCaseUpper,
            _ => null
        };

        var optoins = new JsonSerializerOptions
        {
            ReferenceHandler = ReferenceHandler.IgnoreCycles,
            WriteIndented = FormatJson,
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            PropertyNameCaseInsensitive = CaseInsensitive,
            PropertyNamingPolicy = namePolicy,
            TypeInfoResolver = _propertyNameOrderResolver
        };
        _cache.Add(key, optoins);
        return optoins;
    }

    /// <summary>
    /// 重写ToString
    /// </summary>
    /// <returns>string</returns>
    public override string ToString() => $"FormatJson->{FormatJson},CaseInsensitive->{CaseInsensitive},NameFormat->{NameFormat},OrderByNameProperty->{OrderByNameProperty}";
}