using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

namespace SharpDevLib.Standard;

/// <summary>
/// json扩展
/// </summary>
public static class JsonExtension
{
    static readonly AlphabeticalOrderContractResolver _propertyNameOrderResolver = new();

    /// <summary>
    /// 序列化对象
    /// </summary>
    /// <param name="obj">需要序列化的对象</param>
    /// <param name="formatJson">是否要格式化</param>
    /// <param name="orderPropertyByName">是否按照属性名称排序</param>
    /// <returns>json结果</returns>
    public static string Serialize(this object obj, bool formatJson = false, bool orderPropertyByName = true) => JsonSerializer.Serialize(obj, CreateOption(formatJson, orderPropertyByName));

    /// <summary>
    /// Json反序列化
    /// </summary>
    /// <typeparam name="T">要反序列化的类型</typeparam>
    /// <param name="json">json</param>
    /// <returns>反序列化对象结果</returns>
    public static T DeSerialize<T>(this string json) where T : class
    {
        if (json.IsNullOrWhiteSpace()) throw new ArgumentNullException(nameof(json));
        return JsonSerializer.Deserialize<T>(json) ?? throw new JsonException($"unable to deserialize json to object of type '{typeof(T).FullName}'");
    }

    /// <summary>
    /// Json格式化
    /// </summary>
    /// <param name="json">需要格式化的json</param>
    /// <param name="orderPropertyByName">是否按照属性名称排序</param>
    /// <returns>格式化的json</returns>
    public static string FormatJson(this string json, bool orderPropertyByName = true)
    {
        var obj = json.DeSerialize<object>();
        return JsonSerializer.Serialize(obj, CreateOption(true, orderPropertyByName));
    }

    /// <summary>
    /// Json压缩
    /// </summary>
    /// <param name="json">需要压缩的json</param>
    /// <param name="orderPropertyByName">是否按照属性名称排序</param>
    /// <returns>压缩的json</returns>
    public static string CompressJson(this string json, bool orderPropertyByName = true)
    {
        var obj = json.DeSerialize<object>();
        return obj.Serialize(false, orderPropertyByName);
    }

    static JsonSerializerOptions CreateOption(bool writeIndented, bool orderPropertyByName = true)
    {
        var option = new JsonSerializerOptions
        {
            ReferenceHandler = ReferenceHandler.IgnoreCycles,
            WriteIndented = writeIndented
        };
        if (orderPropertyByName) option.TypeInfoResolver = _propertyNameOrderResolver;
        return option;
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
