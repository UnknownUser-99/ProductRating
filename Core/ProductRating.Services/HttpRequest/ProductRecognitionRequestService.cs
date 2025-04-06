using System.Net.Http.Json;
using ProductRating.Contracts.HttpRequest;
using ProductRating.Data.WebAPI.Requests;
using ProductRating.Data.WebAPI.Results;

namespace ProductRating.Services.HttpRequest
{
    public class ProductRecognitionRequestService : IProductRecognitionRequestService
    {
        private readonly HttpClient _httpClient;

        public ProductRecognitionRequestService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ProductRecognitionResult> RecognizeAsync(string imageBase64)
        {
            ProductRecognitionRequest request = new ProductRecognitionRequest
            {
                ImageBase64 = imageBase64
            };

            var response = await _httpClient.PostAsJsonAsync("ProductRecognition", request);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            Console.WriteLine($"Response Body: {response}");

            var result = await response.Content.ReadFromJsonConfiguredAsync<ProductRecognitionResult>();

            return result;
        }
    }
}