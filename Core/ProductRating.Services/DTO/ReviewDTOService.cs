using ProductRating.Contracts.DTO;
using ProductRating.Data.WebAPI.Results;

namespace ProductRating.Services.DTO
{
    public class ReviewDTOService : IReviewDTOService
    {
        public ReviewsForUpdateRatingResult CreateReviewsForUpdateRatingResult(ReviewRatingResult[] reviews)
        {
            var groupedReviews = reviews
                .GroupBy(r => r.Product)
                .Select(g => new ReviewForUpdateRatingResult
                {
                    Product = g.Key,
                    Ratings = g.Select(r => r.Rating).ToArray()
                })
                .ToArray();

            return new ReviewsForUpdateRatingResult
            {
                Reviews = groupedReviews
            };
        }

        public ReviewsForRecognitionResult CreateReviewsForRecognitionResult(ReviewForRecognitionResult[] reviews)
        {
            return new ReviewsForRecognitionResult
            {
                Reviews = reviews
            };
        }
    }
}