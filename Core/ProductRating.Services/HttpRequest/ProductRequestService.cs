using ProductRating.Contracts.HttpRequest;
using ProductRating.Data.WebAPI.Results;

namespace ProductRating.Services.HttpRequest
{
    public class ProductRequestService : IProductRequestService
    {
        private readonly HttpClient _httpClient;

        public ProductRequestService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ProductCardsResult> GetProductCardsAsync()
        {
            var response = await _httpClient.GetAsync("ProductCards");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var result = await response.Content.ReadFromJsonConfiguredAsync<ProductCardsResult>();

            return result;
        }
    }
}