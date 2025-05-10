using ProductRating.Contracts.HttpRequest;
using ProductRating.Contracts.UpdateRating;
using ProductRating.Data.UpdateRating;

namespace ProductRating.Services.UpdateRating
{
    public class UpdateRatingService : IUpdateRatingService
    {
        private readonly IRatingCalculatorService _ratingCalculatorService;
        private readonly IReviewRequestService _reviewRequestService;
        private readonly IProductRatingRequestService _productRatingRequestService;

        public UpdateRatingService(IRatingCalculatorService ratingCalculatorService, IReviewRequestService reviewRequestService, IProductRatingRequestService productRatingRequestService)
        {
            _ratingCalculatorService = ratingCalculatorService;
            _reviewRequestService = reviewRequestService;
            _productRatingRequestService = productRatingRequestService;
        }

        public async Task<int> UpdateRatingAsync(UpdateRatingType type)
        {
            return type switch
            {
                UpdateRatingType.InitialRating => await UpdateInitialRatingAsync(),
                UpdateRatingType.OverallRating => await UpdateOverallRatingAsync(),
                UpdateRatingType.YearlyRating => await UpdateYearlyRatingAsync(),
                UpdateRatingType.MonthlyRating => await UpdateMonthlyRatingAsync()
            };
        }

        private async Task<int> UpdateInitialRatingAsync()
        {
            var reviewResult = await _reviewRequestService.GetReviewsForUpdateRatingAsync();

            if (reviewResult == null)
            {
                throw new Exception("Не удалось получить пользовательские оценки.");
            }

            NewProductRating[] ratings = new NewProductRating[reviewResult.Reviews.Length];

            for (int i = 0; i < reviewResult.Reviews.Length; i++)
            {
                ratings[i] = new NewProductRating
                {
                    Product = reviewResult.Reviews[i].Product,
                    Rating = _ratingCalculatorService.CalculateRating(reviewResult.Reviews[i].Ratings)
                };
            }

            var result = await _productRatingRequestService.UpdateInitialProductRatingsAsync(ratings);

            if (result == false)
            {
                return 0;
            }

            return ratings.Length;
        }

        private async Task<int> UpdateOverallRatingAsync()
        {
            var reviewResult = await _reviewRequestService.GetReviewsForUpdateOverallRatingAsync();

            if (reviewResult == null)
            {
                throw new Exception("Не удалось получить пользовательские оценки.");
            }

            NewProductRating[] ratings = new NewProductRating[reviewResult.Reviews.Length];

            for (int i = 0; i < reviewResult.Reviews.Length; i++)
            {
                ratings[i] = new NewProductRating
                {
                    Product = reviewResult.Reviews[i].Product,
                    Rating = _ratingCalculatorService.CalculateRating(reviewResult.Reviews[i].Ratings)
                };
            }

            var result = await _productRatingRequestService.UpdateOverallProductRatingsAsync(ratings);

            if (result == false)
            {
                return 0;
            }

            return ratings.Length;
        }

        private async Task<int> UpdateYearlyRatingAsync()
        {
            var reviewResult = await _reviewRequestService.GetReviewsForUpdateYearlyRatingAsync();

            if (reviewResult == null)
            {
                throw new Exception("Не удалось получить пользовательские оценки.");
            }

            NewProductRating[] ratings = new NewProductRating[reviewResult.Reviews.Length];

            for (int i = 0; i < reviewResult.Reviews.Length; i++)
            {
                ratings[i] = new NewProductRating
                {
                    Product = reviewResult.Reviews[i].Product,
                    Rating = _ratingCalculatorService.CalculateRating(reviewResult.Reviews[i].Ratings)
                };
            }

            var result = await _productRatingRequestService.UpdateYearlyProductRatingsAsync(ratings);

            if (result == false)
            {
                return 0;
            }

            return ratings.Length;
        }

        private async Task<int> UpdateMonthlyRatingAsync()
        {
            var reviewResult = await _reviewRequestService.GetReviewsForUpdateMonthlyRatingAsync();

            if (reviewResult == null)
            {
                throw new Exception("Не удалось получить пользовательские оценки.");
            }

            NewProductRating[] ratings = new NewProductRating[reviewResult.Reviews.Length];

            for (int i = 0; i < reviewResult.Reviews.Length; i++)
            {
                ratings[i] = new NewProductRating
                {
                    Product = reviewResult.Reviews[i].Product,
                    Rating = _ratingCalculatorService.CalculateRating(reviewResult.Reviews[i].Ratings)
                };
            }

            var result = await _productRatingRequestService.UpdateMonthlyProductRatingsAsync(ratings);

            if (result == false)
            {
                return 0;
            }

            return ratings.Length;
        }
    }
}