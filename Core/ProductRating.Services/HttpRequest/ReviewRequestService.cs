using System.Net.Http.Json;
using ProductRating.Contracts.HttpRequest;
using ProductRating.Data.WebAPI.Requests;
using ProductRating.Data.WebAPI.Results;

namespace ProductRating.Services.HttpRequest
{
    public class ReviewRequestService : IReviewRequestService
    {
        private readonly HttpClient _httpClient;

        public ReviewRequestService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> AddReviewAsync(int product, int rating, string description)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                description = null;
            }

            AddReviewRequest request = new AddReviewRequest
            {
                Product = product,
                Rating = rating,
                Description = description
            };

            var response = await _httpClient.PostAsJsonAsync("", request);

            return response.IsSuccessStatusCode;
        }

        public async Task<ReviewsForRecognitionResult> GetReviewsForRecognitionAsync(int product)
        {
            var response = await _httpClient.GetAsync($"ReviewsForRecognition?product={product}");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var result = await response.Content.ReadFromJsonConfiguredAsync<ReviewsForRecognitionResult>();

            return result;
        }

        public async Task<ReviewsForUpdateRatingResult> GetReviewsForUpdateRatingAsync()
        {
            var response = await _httpClient.GetAsync("ReviewsForUpdateRating");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var result = await response.Content.ReadFromJsonConfiguredAsync<ReviewsForUpdateRatingResult>();

            return result;
        }

        public async Task<ReviewsForUpdateRatingResult> GetReviewsForUpdateOverallRatingAsync()
        {
            var response = await _httpClient.GetAsync("ReviewsForUpdateOverallRating");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var result = await response.Content.ReadFromJsonConfiguredAsync<ReviewsForUpdateRatingResult>();

            return result;
        }

        public async Task<ReviewsForUpdateRatingResult> GetReviewsForUpdateYearlyRatingAsync()
        {
            var response = await _httpClient.GetAsync("ReviewsForUpdateYearlyRating");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var result = await response.Content.ReadFromJsonConfiguredAsync<ReviewsForUpdateRatingResult>();

            return result;
        }

        public async Task<ReviewsForUpdateRatingResult> GetReviewsForUpdateMonthlyRatingAsync()
        {
            var response = await _httpClient.GetAsync("ReviewsForUpdateMonthlyRating");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var result = await response.Content.ReadFromJsonConfiguredAsync<ReviewsForUpdateRatingResult>();

            return result;
        }
    }
}