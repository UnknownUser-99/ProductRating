namespace ProductRating.Data.WebAPI.Results
{
    public record ProductRatingResult
    {
        public float OverallRating { get; init; }
        public float YearlyRating { get; init; }
        public float MonthlyRating { get; init; }
    }
}