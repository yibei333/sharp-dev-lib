using Newtonsoft.Json;

namespace SharpDevLib;

/// <summary>
/// json util
/// </summary>
public static class JsonUtil
{
    /// <summary>
    /// serialize a object
    /// </summary>
    /// <param name="obj">object to serialize</param>
    /// <returns>json</returns>
    public static string Serialize(this object obj) => JsonConvert.SerializeObject(obj, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });

    /// <summary>
    /// deserialize a json to object
    /// </summary>
    /// <typeparam name="T">object type</typeparam>
    /// <param name="json">json to deserialize</param>
    /// <returns>object</returns>
    /// <exception cref="ArgumentException">if json is empty or null</exception>
    /// <exception cref="JsonSerializationException">if json can't deserialize to a object</exception>
    public static T DeSerialize<T>(this string json) where T : class
    {
        if (json.IsNull()) throw new ArgumentException($"unable to deserialize empty json to a object");
        try
        {
            return JsonConvert.DeserializeObject<T>(json)!;
        }
        catch (Exception ex)
        {
            throw new JsonSerializationException($"unable to deserialize json('{json}') to a object of type '{typeof(T).FullName}',{ex.Message}", ex);
        }
    }

    /// <summary>
    /// beautify the json
    /// </summary>
    /// <param name="json">json to beautify</param>
    /// <returns>beautified json</returns>
    public static string FormatJson(this string? json)
    {
        if (json.IsNull()) return string.Empty;

        var serializer = new JsonSerializer();
        using var jsonReader = new JsonTextReader(new StringReader(json));
        var obj = serializer.Deserialize(jsonReader);
        if (obj.IsNull()) return json ?? string.Empty;

        using var textWriter = new StringWriter();
        using var jsonWriter = new JsonTextWriter(textWriter)
        {
            Formatting = Formatting.Indented,
            Indentation = 4,
            IndentChar = ' '
        };
        serializer.Serialize(jsonWriter, obj);
        return textWriter.ToString();
    }
}
