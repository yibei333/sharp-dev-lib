using System.Text.Json;

namespace SharpDevLib;

/// <summary>
/// json扩展
/// </summary>
internal static class Json
{
    /// <summary>
    /// 序列化对象
    /// </summary>
    /// <param name="obj">需要序列化的对象</param>
    /// <returns>json结果</returns>
    public static string Serialize(this object obj) => JsonSerializer.Serialize(obj);

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
            jsonResult = JsonSerializer.Serialize(obj);
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
    /// <exception cref="ArgumentNullException">当json参数为空时引发异常</exception>
    /// <exception cref="JsonException">当反序列化失败时引发异常</exception>
    public static T DeSerialize<T>(this string json) where T : class
    {
        if (json.IsNullOrWhiteSpace()) throw new ArgumentNullException(nameof(json));
        return JsonSerializer.Deserialize<T>(json) ?? throw new JsonException($"unable to deserialize json to object of type '{typeof(T).FullName}'");
    }

    /// <summary>
    /// Json反序列化
    /// </summary>
    /// <param name="json">json</param>
    /// <param name="type">type</param>
    /// <returns>反序列化对象结果</returns>
    /// <exception cref="ArgumentNullException">当json参数为空时引发异常</exception>
    /// <exception cref="JsonException">当反序列化失败时引发异常</exception>
    public static object DeSerialize(this string json, Type type)
    {
        if (json.IsNullOrWhiteSpace()) throw new ArgumentNullException(nameof(json));
        return JsonSerializer.Deserialize(json, type) ?? throw new JsonException($"unable to deserialize json to object of type '{type.FullName}'");
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
}