using System.Text.Json.Serialization;

namespace SharpDevLib.Cryptography;

internal class JwtHeader
{
    public JwtHeader(string algorithm, string type)
    {
        Algorithm = algorithm;
        Type = type;

        if (!Enum.TryParse<JwtAlgorithm>(algorithm, out var value)) throw new NotSupportedException($"algorithm '{algorithm}' not supported yet");
        JwtAlgorithm = value;
    }

    [JsonPropertyName("alg")]
    public string Algorithm { get; set; }

    [JsonPropertyName("typ")]
    public string Type { get; set; }

    [JsonIgnore]
    public JwtAlgorithm JwtAlgorithm { get; }
}
