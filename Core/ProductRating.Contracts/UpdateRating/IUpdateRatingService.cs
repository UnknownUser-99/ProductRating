using ProductRating.Data.UpdateRating;

namespace ProductRating.Contracts.UpdateRating
{
    public interface IUpdateRatingService
    {
        Task<int> UpdateRatingAsync(UpdateRatingType type);
    }
}