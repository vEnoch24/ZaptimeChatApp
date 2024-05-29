using System.Text.Json;

namespace ZaptimeChatApp.Client.Helpers
{
    public static class JsonConverter
    {
        public static JsonSerializerOptions JsonSerializerOptions => new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            //PropertyNameCaseInsensitive = true,
        };

        public static string Serialize(object value, JsonSerializerOptions? jsonSerializerOptions = null)
        {
            return JsonSerializer.Serialize(value, jsonSerializerOptions ?? JsonSerializerOptions);
        }

        public static TResult? Deserialize<TResult>( string serializeString, JsonSerializerOptions? jsonSerializerOptions = null)
        {
            return JsonSerializer.Deserialize<TResult>(serializeString, jsonSerializerOptions ?? JsonSerializerOptions);
        }
    }
}
