namespace ProductRating.Contracts.Database
{
    public interface IProductRatingService
    {
        Task<bool> UpdateAllProductRatingAsync(int product, decimal rating);
    }
}