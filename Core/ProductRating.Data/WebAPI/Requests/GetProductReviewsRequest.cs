namespace ProductRating.Data.WebAPI.Requests
{
    public record GetProductReviewsRequest
    {
        public int Product { get; init; }
        public int Count { get; init; }
    }
}