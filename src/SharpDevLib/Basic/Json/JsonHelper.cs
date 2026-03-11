using System.Text.Json;

namespace SharpDevLib;

/// <summary>
/// JSON序列化和反序列化扩展，提供对象的JSON序列化与反序列化功能
/// </summary>
public static class JsonHelper
{
    /// <summary>
    /// 使用默认配置将对象序列化为JSON字符串
    /// </summary>
    /// <param name="obj">需要序列化的对象</param>
    /// <returns>JSON字符串</returns>
    public static string Serialize(this object obj) => JsonSerializer.Serialize(obj, JsonOption.Default.Create());

    /// <summary>
    /// 使用指定配置将对象序列化为JSON字符串
    /// </summary>
    /// <param name="obj">需要序列化的对象</param>
    /// <param name="option">JSON序列化选项</param>
    /// <returns>JSON字符串</returns>
    public static string Serialize(this object obj, JsonOption option) => JsonSerializer.Serialize(obj, (option ?? JsonOption.Default).Create());

    /// <summary>
    /// 尝试使用默认配置将对象序列化为JSON字符串
    /// </summary>
    /// <param name="obj">需要序列化的对象</param>
    /// <param name="jsonResult">输出的JSON字符串，失败时为空字符串</param>
    /// <returns>序列化是否成功</returns>
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
    /// 尝试使用指定配置将对象序列化为JSON字符串
    /// </summary>
    /// <param name="obj">需要序列化的对象</param>
    /// <param name="option">JSON序列化选项</param>
    /// <param name="jsonResult">输出的JSON字符串，失败时为空字符串</param>
    /// <returns>序列化是否成功</returns>
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
    /// 使用默认配置将JSON字符串反序列化为指定类型的对象
    /// </summary>
    /// <typeparam name="T">目标类型</typeparam>
    /// <param name="json">JSON字符串</param>
    /// <returns>反序列化后的对象</returns>
    /// <exception cref="ArgumentNullException">当json参数为空或仅包含空白字符时引发异常</exception>
    /// <exception cref="JsonException">当反序列化失败时引发异常</exception>
    public static T DeSerialize<T>(this string json) where T : class
    {
        if (json.IsNullOrWhiteSpace()) throw new ArgumentNullException(nameof(json));
        return JsonSerializer.Deserialize<T>(json, JsonOption.Default.Create()) ?? throw new JsonException($"无法将JSON反序列化为类型'{typeof(T).FullName}'的对象");
    }

    /// <summary>
    /// 使用默认配置将JSON字符串反序列化为指定类型的对象
    /// </summary>
    /// <param name="json">JSON字符串</param>
    /// <param name="type">目标类型</param>
    /// <returns>反序列化后的对象</returns>
    /// <exception cref="ArgumentNullException">当json参数为空或仅包含空白字符时引发异常</exception>
    /// <exception cref="JsonException">当反序列化失败时引发异常</exception>
    public static object DeSerialize(this string json, Type type)
    {
        if (json.IsNullOrWhiteSpace()) throw new ArgumentNullException(nameof(json));
        return JsonSerializer.Deserialize(json, type, JsonOption.Default.Create()) ?? throw new JsonException($"无法将JSON反序列化为类型'{type.FullName}'的对象");
    }

    /// <summary>
    /// 使用默认配置将JSON字符串反序列化为指定类型的对象
    /// </summary>
    /// <param name="json">JSON字符串</param>
    /// <param name="type">目标类型</param>
    /// <param name="option">配置</param>
    /// <returns>反序列化后的对象</returns>
    /// <exception cref="ArgumentNullException">当json参数为空或仅包含空白字符时引发异常</exception>
    /// <exception cref="JsonException">当反序列化失败时引发异常</exception>
    public static object DeSerialize(this string json, Type type, JsonOption? option)
    {
        if (json.IsNullOrWhiteSpace()) throw new ArgumentNullException(nameof(json));
        return JsonSerializer.Deserialize(json, type, (option ?? JsonOption.Default).Create()) ?? throw new JsonException($"无法将JSON反序列化为类型'{type.FullName}'的对象");
    }

    /// <summary>
    /// 使用指定配置将JSON字符串反序列化为指定类型的对象
    /// </summary>
    /// <typeparam name="T">目标类型</typeparam>
    /// <param name="json">JSON字符串</param>
    /// <param name="option">JSON反序列化选项</param>
    /// <returns>反序列化后的对象</returns>
    /// <exception cref="ArgumentNullException">当json参数为空或仅包含空白字符时引发异常</exception>
    /// <exception cref="JsonException">当反序列化失败时引发异常</exception>
    public static T DeSerialize<T>(this string json, JsonOption option) where T : class
    {
        if (json.IsNullOrWhiteSpace()) throw new ArgumentNullException(nameof(json));
        return JsonSerializer.Deserialize<T>(json, (option ?? JsonOption.Default).Create()) ?? throw new JsonException($"无法将JSON反序列化为类型'{typeof(T).FullName}'的对象");
    }

    /// <summary>
    /// 尝试使用默认配置将JSON字符串反序列化为指定类型的对象
    /// </summary>
    /// <typeparam name="T">目标类型</typeparam>
    /// <param name="json">JSON字符串</param>
    /// <param name="result">输出的反序列化对象，失败时为默认值</param>
    /// <returns>反序列化是否成功</returns>
    public static bool TryDeSerialize<T>(this string json, out T result) where T : class
    {
        try
        {
            if (json.IsNullOrWhiteSpace()) throw new ArgumentNullException(nameof(json));
            result = JsonSerializer.Deserialize<T>(json, JsonOption.Default.Create()) ?? throw new JsonException($"无法将JSON反序列化为类型'{typeof(T).FullName}'的对象");
            return true;
        }
        catch
        {
            result = default!;
            return false;
        }
    }

    /// <summary>
    /// 尝试使用指定配置将JSON字符串反序列化为指定类型的对象
    /// </summary>
    /// <typeparam name="T">目标类型</typeparam>
    /// <param name="json">JSON字符串</param>
    /// <param name="option">JSON反序列化选项</param>
    /// <param name="result">输出的反序列化对象，失败时为默认值</param>
    /// <returns>反序列化是否成功</returns>
    public static bool TryDeSerialize<T>(this string json, JsonOption option, out T result) where T : class
    {
        try
        {
            if (json.IsNullOrWhiteSpace()) throw new ArgumentNullException(nameof(json));
            result = JsonSerializer.Deserialize<T>(json, (option ?? JsonOption.Default).Create()) ?? throw new JsonException($"无法将JSON反序列化为类型'{typeof(T).FullName}'的对象");
            return true;
        }
        catch
        {
            result = default!;
            return false;
        }
    }

    /// <summary>
    /// 格式化JSON字符串，使其更易读
    /// </summary>
    /// <param name="json">需要格式化的JSON字符串</param>
    /// <returns>格式化后的JSON字符串，格式化失败时返回原字符串</returns>
    public static string FormatJson(this string json)
    {
        try
        {
            var obj = json.DeSerialize<object>(JsonOption.DefaultFormatJson);
            return obj.Serialize(JsonOption.DefaultFormatJson);
        }
        catch
        {
            return json;
        }
    }

    /// <summary>
    /// 压缩JSON字符串，移除空白字符
    /// </summary>
    /// <param name="json">需要压缩的JSON字符串</param>
    /// <returns>压缩后的JSON字符串，压缩失败时返回原字符串</returns>
    public static string CompressJson(this string json)
    {
        try
        {
            var obj = json.DeSerialize<object>(JsonOption.DefaultCompressJson);
            return obj.Serialize(JsonOption.DefaultCompressJson);
        }
        catch
        {
            return json;
        }
    }
}