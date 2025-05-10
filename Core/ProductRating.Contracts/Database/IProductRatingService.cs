namespace ProductRating.Contracts.Database
{
    public interface IProductRatingService
    {
        Task<bool> UpdateAllProductRatingAsync(int product, decimal rating);
        Task<bool> UpdateOverallProductRatingAsync(int product, decimal rating);
        Task<bool> UpdateYearlyProductRatingAsync(int product, decimal rating);
        Task<bool> UpdateMonthlyProductRatingAsync(int product, decimal rating);
    }
}