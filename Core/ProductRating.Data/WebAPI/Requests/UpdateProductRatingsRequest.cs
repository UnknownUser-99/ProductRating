using ProductRating.Data.UpdateRating;

namespace ProductRating.Data.WebAPI.Requests
{
    public record UpdateProductRatingsRequest
    {
        public NewProductRating[] Ratings { get; init; }
    }
}