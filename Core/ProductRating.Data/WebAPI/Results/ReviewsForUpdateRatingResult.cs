namespace ProductRating.Data.WebAPI.Results
{
    public record ReviewsForUpdateRatingResult
    {
        public ReviewForUpdateRatingResult[] Reviews { get; init; }
    }
}