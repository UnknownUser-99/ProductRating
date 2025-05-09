using System.Net.Http.Json;
using ProductRating.Contracts.HttpRequest;
using ProductRating.Data.UpdateRating;
using ProductRating.Data.WebAPI.Requests;

namespace ProductRating.Services.HttpRequest
{
    public class ProductRatingRequestService : IProductRatingRequestService
    {
        private readonly HttpClient _httpClient;

        public ProductRatingRequestService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> UpdateInitialProductRatingsAsync(NewProductRating[] ratings)
        {
            UpdateProductRatingsRequest request = new UpdateProductRatingsRequest()
            {
                Ratings = ratings
            };

            var response = await _httpClient.PutAsJsonAsync("UpdateInitialProductRatings", request);

            return response.IsSuccessStatusCode;
        }
    }
}