using System.Text.Json;
using System.Net.Http.Json;

namespace ProductRating.Services.HttpRequest
{
    public static class HttpContentExtensions
    {
        private static readonly JsonSerializerOptions _jsonSerializerOptions;

        static HttpContentExtensions()
        {
            _jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public static Task<T> ReadFromJsonConfiguredAsync<T>(this HttpContent content)
        {
            return content.ReadFromJsonAsync<T>(_jsonSerializerOptions);
        }
    }
}