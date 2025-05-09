using ProductRating.Data.WebAPI.Results;

namespace ProductRating.Contracts.DTO
{
    public interface IReviewDTOService
    {
        ReviewsForUpdateRatingResult CreateReviewsForUpdateRatingResult(ReviewRatingResult[] reviews);
        ReviewsForRecognitionResult CreateReviewsForRecognitionResult(ReviewForRecognitionResult[] reviews);
    }
}