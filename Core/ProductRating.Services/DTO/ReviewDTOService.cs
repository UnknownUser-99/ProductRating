using ProductRating.Contracts.DTO;
using ProductRating.Data.WebAPI.Results;

namespace ProductRating.Services.DTO
{
    public class ReviewDTOService : IReviewDTOService
    {
        public ReviewsForRecognitionResult CreateReviewsForRecognitionResult(ReviewForRecognitionResult[] reviews)
        {
            return new ReviewsForRecognitionResult
            {
                Reviews = reviews
            };
        }
    }
}