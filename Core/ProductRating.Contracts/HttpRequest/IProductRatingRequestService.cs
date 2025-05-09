using ProductRating.Data.UpdateRating;

namespace ProductRating.Contracts.HttpRequest
{
    public interface IProductRatingRequestService
    {
        Task<bool> UpdateInitialProductRatingsAsync(NewProductRating[] ratings);
    }
}