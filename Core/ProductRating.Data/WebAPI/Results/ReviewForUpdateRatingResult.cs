namespace ProductRating.Data.WebAPI.Results
{
    public record ReviewForUpdateRatingResult
    {
        public int Product { get; init; }
        public int[] Ratings { get; init; }
    }
}