using System.Text.Json;
using System.Text.Json.Serialization;

namespace SharpDevLib.Standard;

/// <summary>
/// json扩展
/// </summary>
public static class JsonExtension
{
    /// <summary>
    /// 序列化对象
    /// </summary>
    /// <param name="obj">需要序列化的对象</param>
    /// <param name="formatJson">是否要格式化</param>
    /// <returns>json结果</returns>
    public static string Serialize(this object? obj, bool formatJson = false)
    {
        if (obj is null) return string.Empty;
        return JsonSerializer.Serialize(obj, new JsonSerializerOptions { ReferenceHandler = ReferenceHandler.IgnoreCycles, WriteIndented = formatJson });
    }

    /// <summary>
    /// Json反序列化
    /// </summary>
    /// <typeparam name="T">要反序列化的类型</typeparam>
    /// <param name="json">json</param>
    /// <returns>反序列化对象结果</returns>
    public static T? DeSerialize<T>(this string? json) where T : class
    {
        if (json.IsNullOrWhiteSpace()) return null;
        return JsonSerializer.Deserialize<T>(json);
    }

    /// <summary>
    /// Json格式化
    /// </summary>
    /// <param name="json">需要格式化的json</param>
    /// <returns>格式化的json</returns>
    public static string? FormatJson(this string? json)
    {
        if (json.IsNullOrWhiteSpace()) return json;
        var obj = JsonSerializer.Deserialize<object>(json);
        if (obj is null) return null;
        return JsonSerializer.Serialize(obj, new JsonSerializerOptions { WriteIndented = true });
    }

    /// <summary>
    /// Json压缩
    /// </summary>
    /// <param name="json">需要压缩的json</param>
    /// <returns>压缩的json</returns>
    public static string? CompressJson(this string? json)
    {
        if (json.IsNullOrWhiteSpace()) return json;
        var obj = JsonSerializer.Deserialize<object>(json);
        if (obj is null) return null;
        return JsonSerializer.Serialize(obj);
    }
}
