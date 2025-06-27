using ProductRating.Data.WebAPI.Results;

namespace ProductRating.Contracts.Database
{
    public interface IReviewService
    {
        Task<int> AddReviewAsync(int user, int product, int rating, string description);
        Task<ReviewRatingResult[]> GetReviewsForUpdateRatingAsync();
        Task<ReviewRatingResult[]> GetReviewsForUpdateOverallRatingAsync();
        Task<ReviewRatingResult[]> GetReviewsForUpdateYearlyRatingAsync();
        Task<ReviewRatingResult[]> GetReviewsForUpdateMonthlyRatingAsync();
        Task<ReviewForRecognitionResult[]> GetReviewsForRecognitionAsync(int product, int count = 5);
    }
}