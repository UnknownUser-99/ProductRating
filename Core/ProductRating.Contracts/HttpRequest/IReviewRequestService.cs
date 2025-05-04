using ProductRating.Data.WebAPI.Results;

namespace ProductRating.Contracts.HttpRequest
{
    public interface IReviewRequestService
    {
        Task<bool> AddReviewAsync(int product, int rating, string description);
        Task<ReviewsForRecognitionResult> GetReviewForRecognition(int product);
    }
}