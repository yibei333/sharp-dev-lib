using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

namespace SharpDevLib;

/// <summary>
/// json扩展
/// </summary>
public static class JsonExtension
{
    /// <summary>
    /// 序列化对象
    /// </summary>
    /// <param name="obj">需要序列化的对象</param>
    /// <returns>json结果</returns>
    public static string Serialize(this object obj) => JsonSerializer.Serialize(obj, JsonOption.Default.Create());

    /// <summary>
    /// 序列化对象
    /// </summary>
    /// <param name="obj">需要序列化的对象</param>
    /// <param name="option">选项</param>
    /// <returns>json结果</returns>
    public static string Serialize(this object obj, JsonOption option) => JsonSerializer.Serialize(obj, (option ?? JsonOption.Default).Create());

    /// <summary>
    /// 尝试序列化对象,失败返回false,成功返回true且结果放在jsonResult参数中
    /// </summary>
    /// <param name="obj">需要序列化的对象</param>
    /// <param name="jsonResult">json结果</param>
    /// <returns>是否成功</returns>
    public static bool TrySerialize(this object obj, out string jsonResult)
    {
        try
        {
            jsonResult = JsonSerializer.Serialize(obj, JsonOption.Default.Create());
            return true;
        }
        catch
        {
            jsonResult = string.Empty;
            return false;
        }
    }

    /// <summary>
    /// 尝试序列化对象,失败返回false,成功返回true且结果放在jsonResult参数中
    /// </summary>
    /// <param name="obj">需要序列化的对象</param>
    /// <param name="option">选项</param>
    /// <param name="jsonResult">json结果</param>
    /// <returns>是否成功</returns>
    public static bool TrySerialize(this object obj, JsonOption option, out string jsonResult)
    {
        try
        {
            jsonResult = JsonSerializer.Serialize(obj, (option ?? JsonOption.Default).Create());
            return true;
        }
        catch
        {
            jsonResult = string.Empty;
            return false;
        }
    }

    /// <summary>
    /// Json反序列化
    /// </summary>
    /// <typeparam name="T">要反序列化的类型</typeparam>
    /// <param name="json">json</param>
    /// <returns>反序列化对象结果</returns>
    public static T DeSerialize<T>(this string json) where T : class
    {
        if (json.IsNullOrWhiteSpace()) throw new ArgumentNullException(nameof(json));
        return JsonSerializer.Deserialize<T>(json, JsonOption.Default.Create()) ?? throw new JsonException($"unable to deserialize json to object of type '{typeof(T).FullName}'");
    }

    /// <summary>
    /// Json反序列化
    /// </summary>
    /// <param name="json">json</param>
    /// <param name="type">type</param>
    /// <returns>反序列化对象结果</returns>
    public static object DeSerialize(this string json, Type type)
    {
        if (json.IsNullOrWhiteSpace()) throw new ArgumentNullException(nameof(json));
        return JsonSerializer.Deserialize(json, type, JsonOption.Default.Create()) ?? throw new JsonException($"unable to deserialize json to object of type '{type.FullName}'");
    }

    /// <summary>
    /// Json反序列化
    /// </summary>
    /// <typeparam name="T">要反序列化的类型</typeparam>
    /// <param name="json">json</param>
    /// <param name="option">选项</param>
    /// <returns>反序列化对象结果</returns>
    public static T DeSerialize<T>(this string json, JsonOption option) where T : class
    {
        if (json.IsNullOrWhiteSpace()) throw new ArgumentNullException(nameof(json));
        return JsonSerializer.Deserialize<T>(json, (option ?? JsonOption.Default).Create()) ?? throw new JsonException($"unable to deserialize json to object of type '{typeof(T).FullName}'");
    }

    /// <summary>
    /// 尝试反序列化,失败返回false,成功返回true且结果放在result参数中
    /// </summary>
    /// <typeparam name="T">要反序列化的类型</typeparam>
    /// <param name="json">json</param>
    /// <param name="result">反序列化对象结果</param>
    /// <returns>是否成功</returns>
    public static bool TryDeSerialize<T>(this string json, out T result) where T : class
    {
        try
        {
            if (json.IsNullOrWhiteSpace()) throw new ArgumentNullException(nameof(json));
            result = JsonSerializer.Deserialize<T>(json, JsonOption.Default.Create()) ?? throw new JsonException($"unable to deserialize json to object of type '{typeof(T).FullName}'");
            return true;
        }
        catch
        {
            result = default!;
            return false;
        }
    }

    /// <summary>
    /// 尝试反序列化,失败返回false,成功返回true且结果放在result参数中
    /// </summary>
    /// <typeparam name="T">要反序列化的类型</typeparam>
    /// <param name="json">json</param>
    /// <param name="option">选项</param>
    /// <param name="result">反序列化对象结果</param>
    /// <returns>是否成功</returns>
    public static bool TryDeSerialize<T>(this string json, JsonOption option, out T result) where T : class
    {
        try
        {
            if (json.IsNullOrWhiteSpace()) throw new ArgumentNullException(nameof(json));
            result = JsonSerializer.Deserialize<T>(json, (option ?? JsonOption.Default).Create()) ?? throw new JsonException($"unable to deserialize json to object of type '{typeof(T).FullName}'");
            return true;
        }
        catch
        {
            result = default!;
            return false;
        }
    }

    /// <summary>
    /// Json格式化
    /// </summary>
    /// <param name="json">需要格式化的json</param>
    /// <param name="orderByNameProperty">是否根据属性名称排序,默认为true</param>
    /// <returns>格式化的json</returns>
    public static string FormatJson(this string json, bool orderByNameProperty = true)
    {
        try
        {
            var option = orderByNameProperty ? JsonOption.DefaultFormatJson : JsonOption.DefaultFormatJsonWithoutOrder;
            var obj = json.DeSerialize<object>(option);
            return obj.Serialize(option);
        }
        catch
        {
            return json;
        }
    }

    /// <summary>
    /// Json压缩
    /// </summary>
    /// <param name="json">需要压缩的json</param>
    /// <param name="orderByNameProperty">是否根据属性名称排序,默认为true</param>
    /// <returns>压缩的json</returns>
    public static string CompressJson(this string json, bool orderByNameProperty = true)
    {
        try
        {
            var option = orderByNameProperty ? JsonOption.DefaultCompressJson : JsonOption.DefaultCompressJsonWithoutOrder;
            var obj = json.DeSerialize<object>(option);
            return obj.Serialize(option);
        }
        catch
        {
            return json;
        }
    }
}

class AlphabeticalOrderContractResolver : DefaultJsonTypeInfoResolver
{
    public override JsonTypeInfo GetTypeInfo(Type type, JsonSerializerOptions options)
    {
        var jsonTypeInfo = base.GetTypeInfo(type, options);
        int order = 1;

        foreach (var property in jsonTypeInfo.Properties.OrderBy(x => x.Name))
        {
            property.Order = order++;
        }

        return jsonTypeInfo;
    }
}

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

/// <summary>
/// json命名格式
/// </summary>
public enum JsonNameFormat
{
    /// <summary>
    /// 小驼峰命名(如SomeProperty->someProperty)
    /// </summary>
    CamelCaseLower,
    /// <summary>
    /// 大驼峰命名(如SomeProperty->SomeProperty)
    /// </summary>
    CamelCaseUpper,
    /// <summary>
    /// 串式命名小写(如SomeProperty->some-property)
    /// </summary>
    KebabCaseLower,
    /// <summary>
    /// 串式命名大写(如SomeProperty->SOME-PROPERTY)
    /// </summary>
    KebabCaseUpper,
    /// <summary>
    /// 蛇形命名小写(如SomeProperty->some_property)
    /// </summary>
    SnakeCaseLower,
    /// <summary>
    /// 蛇形命名大写(如SomeProperty->SOME_PROPERTY)
    /// </summary>
    SnakeCaseUpper
}