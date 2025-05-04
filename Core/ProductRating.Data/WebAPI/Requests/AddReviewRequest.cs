namespace ProductRating.Data.WebAPI.Requests
{
    public record AddReviewRequest
    {
        public int Product { get; init; }
        public int Rating { get; init; }
        public string Description { get; init; }
    }
}