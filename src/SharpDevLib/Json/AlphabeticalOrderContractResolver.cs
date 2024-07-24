using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

namespace SharpDevLib;

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