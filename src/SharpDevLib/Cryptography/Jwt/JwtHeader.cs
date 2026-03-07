using System.Text.Json.Serialization;

namespace SharpDevLib;

/// <summary>
/// JWT头信息，包含算法和类型信息
/// </summary>
internal class JwtHeader
{
    /// <summary>
    /// 初始化JWT头信息
    /// </summary>
    /// <param name="algorithm">算法名称</param>
    /// <param name="type">类型标识</param>
    /// <exception cref="NotSupportedException">当算法不支持时抛出异常</exception>
    public JwtHeader(string algorithm, string type)
    {
        Algorithm = algorithm;
        Type = type;

        if (!Enum.TryParse<JwtAlgorithm>(algorithm, out var value)) throw new NotSupportedException($"暂不支持的算法: '{algorithm}'");
        JwtAlgorithm = value;
    }

    /// <summary>
    /// 获取或设置算法标识
    /// </summary>
    [JsonPropertyName("alg")]
    public string Algorithm { get; set; }

    /// <summary>
    /// 获取或设置类型标识
    /// </summary>
    [JsonPropertyName("typ")]
    public string Type { get; set; }

    /// <summary>
    /// 获取JWT算法枚举值
    /// </summary>
    [JsonIgnore]
    public JwtAlgorithm JwtAlgorithm { get; }
}
