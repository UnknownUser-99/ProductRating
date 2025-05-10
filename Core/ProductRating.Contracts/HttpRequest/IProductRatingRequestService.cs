using ProductRating.Data.UpdateRating;

namespace ProductRating.Contracts.HttpRequest
{
    public interface IProductRatingRequestService
    {
        Task<bool> UpdateInitialProductRatingsAsync(NewProductRating[] ratings);
        Task<bool> UpdateOverallProductRatingsAsync(NewProductRating[] ratings);
        Task<bool> UpdateYearlyProductRatingsAsync(NewProductRating[] ratings);
        Task<bool> UpdateMonthlyProductRatingsAsync(NewProductRating[] ratings);
    }
}