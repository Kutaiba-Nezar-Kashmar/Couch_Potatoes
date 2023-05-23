using System.Text.Json;

namespace Search.Infrastructure.Util;

public static class JsonDeserializer
{
    public static TTo? Deserialize<TTo>(string jsonString)
    {
        return JsonSerializer.Deserialize<TTo>(jsonString, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
    }
}