using ProductRating.Contracts.ModelFactory;
using ProductRating.Data.Models;
using ProductRating.Data.WebAPI.Results;

namespace ProductRating.Services.ModelFactory
{
    public class ReviewModelService : IReviewModelService
    {
        public ReviewModel[] CreateReviewModels(ReviewsForRecognitionResult reviewsForRecognition)
        {
            if (reviewsForRecognition == null)
            {
                return Array.Empty<ReviewModel>();
            }

            ReviewModel[] models = new ReviewModel[reviewsForRecognition.Reviews.Length];

            for (int i = 0; i < reviewsForRecognition.Reviews.Length; i++)
            {
                models[i] = new ReviewModel
                {
                    User = reviewsForRecognition.Reviews[i].User,
                    Rating = reviewsForRecognition.Reviews[i].Rating,
                    Description = reviewsForRecognition.Reviews[i].Description,
                    Date = reviewsForRecognition.Reviews[i].Date
                };
            }

            return models;
        }
    }
}