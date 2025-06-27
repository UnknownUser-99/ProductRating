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

        public async Task<bool> UpdateOverallProductRatingsAsync(NewProductRating[] ratings)
        {
            UpdateProductRatingsRequest request = new UpdateProductRatingsRequest()
            {
                Ratings = ratings
            };

            var response = await _httpClient.PutAsJsonAsync("UpdateOverallProductRatings", request);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateYearlyProductRatingsAsync(NewProductRating[] ratings)
        {
            UpdateProductRatingsRequest request = new UpdateProductRatingsRequest()
            {
                Ratings = ratings
            };

            var response = await _httpClient.PutAsJsonAsync("UpdateYearlyProductRatings", request);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateMonthlyProductRatingsAsync(NewProductRating[] ratings)
        {
            UpdateProductRatingsRequest request = new UpdateProductRatingsRequest()
            {
                Ratings = ratings
            };

            var response = await _httpClient.PutAsJsonAsync("UpdateMonthlyProductRatings", request);

            return response.IsSuccessStatusCode;
        }
    }
}