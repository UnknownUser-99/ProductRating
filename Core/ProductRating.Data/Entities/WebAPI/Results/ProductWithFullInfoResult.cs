namespace ProductRating.Data.Entities.WebAPI.Results
{
    public record ProductWithFullInfoResult
    {
        public string Product { get; init; }
        public string Brand { get; init; }
        public string Type { get; init;  }
        public string Image { get; init; }
        public float OverallRating { get; init; }
        public float YearlyRating { get; init; }
        public float MonthlyRating { get; init; }
    }
}