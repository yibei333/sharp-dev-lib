using System.Collections.Concurrent;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SharpDevLib;

/// <summary>
/// JSON序列化和反序列化配置选项
/// </summary>
public class JsonOption
{
    readonly ConcurrentDictionary<string, JsonSerializerOptions> _cache = new();
    static readonly AlphabeticalOrderContractResolver _propertyNameOrderResolver = new();
    internal static JsonOption DefaultFormatJson = new() { FormatJson = true };
    internal static JsonOption DefaultFormatJsonWithoutOrder = new() { FormatJson = true, OrderByNameProperty = false };
    internal static JsonOption DefaultCompressJson = new() { FormatJson = false };
    internal static JsonOption DefaultCompressJsonWithoutOrder = new() { FormatJson = false, OrderByNameProperty = false };

    /// <summary>
    /// 默认JSON配置选项，所有序列化和反序列化操作都可以使用此默认配置
    /// </summary>
    public static JsonOption Default { get; set; } = new();

    /// <summary>
    /// 是否格式化JSON输出，true表示使用缩进格式化，false表示压缩格式，默认为false
    /// </summary>
    public bool FormatJson { get; set; }
    /// <summary>
    /// 反序列化时是否忽略属性名称大小写，默认为true
    /// </summary>
    public bool CaseInsensitive { get; set; } = true;
    /// <summary>
    /// JSON属性命名格式，默认为大驼峰格式(CamelCaseUpper)
    /// </summary>
    public JsonNameFormat NameFormat { get; set; } = JsonNameFormat.CamelCaseUpper;
    /// <summary>
    /// 序列化时是否按属性名称字母顺序排序，默认为true
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
        var addResult = _cache.TryAdd(key, optoins);
        if (!addResult) throw new Exception($"json option add key '{key}' failed");
        return optoins;
    }

    /// <summary>
    /// 获取配置选项的字符串表示
    /// </summary>
    /// <returns>配置选项的键值对字符串</returns>
    public override string ToString() => $"FormatJson->{FormatJson},CaseInsensitive->{CaseInsensitive},NameFormat->{NameFormat},OrderByNameProperty->{OrderByNameProperty}";
}