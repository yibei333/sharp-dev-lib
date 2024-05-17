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
    /// 尝试序列化对象,失败返回false,成功返回true且结果放在jsonResult参数中
    /// </summary>
    /// <param name="obj">需要序列化的对象</param>
    /// <param name="jsonResult">json结果</param>
    /// <param name="formatJson">是否要格式化</param>
    /// <param name="orderPropertyByName">是否按照属性名称排序</param>
    /// <returns>是否成功</returns>
    public static bool TrySerialize(this object obj, out string jsonResult, bool formatJson = false, bool orderPropertyByName = true)
    {
        try
        {
            jsonResult = JsonSerializer.Serialize(obj, CreateOption(formatJson, orderPropertyByName));
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
        return JsonSerializer.Deserialize<T>(json) ?? throw new JsonException($"unable to deserialize json to object of type '{typeof(T).FullName}'");
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
            result = JsonSerializer.Deserialize<T>(json) ?? throw new JsonException($"unable to deserialize json to object of type '{typeof(T).FullName}'");
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
    /// <param name="orderPropertyByName">是否按照属性名称排序</param>
    /// <returns>格式化的json</returns>
    public static string FormatJson(this string json, bool orderPropertyByName = true)
    {
        try
        {
            var obj = json.DeSerialize<object>();
            return JsonSerializer.Serialize(obj, CreateOption(true, orderPropertyByName));
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
    /// <param name="orderPropertyByName">是否按照属性名称排序</param>
    /// <returns>压缩的json</returns>
    public static string CompressJson(this string json, bool orderPropertyByName = true)
    {
        try
        {
            var obj = json.DeSerialize<object>();
            return obj.Serialize(false, orderPropertyByName);
        }
        catch
        {
            return json;
        }
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
