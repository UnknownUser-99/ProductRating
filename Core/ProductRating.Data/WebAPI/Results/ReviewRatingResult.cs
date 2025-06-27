namespace ProductRating.Data.WebAPI.Results
{
    public record ReviewRatingResult
    {
        public int Product { get; init; }
        public int Rating { get; init; }
    }
}