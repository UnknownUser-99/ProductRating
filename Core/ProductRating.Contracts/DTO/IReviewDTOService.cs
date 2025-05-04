using ProductRating.Data.WebAPI.Results;

namespace ProductRating.Contracts.DTO
{
    public interface IReviewDTOService
    {
        ReviewsForRecognitionResult CreateReviewsForRecognitionResult(ReviewForRecognitionResult[] reviews);
    }
}