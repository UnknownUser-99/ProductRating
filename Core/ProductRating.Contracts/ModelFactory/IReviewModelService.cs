using ProductRating.Data.Models;
using ProductRating.Data.WebAPI.Results;

namespace ProductRating.Contracts.ModelFactory
{
    public interface IReviewModelService
    {
        ReviewModel[] CreateReviewModels(ReviewsForRecognitionResult reviewsForRecognition);
    }
}