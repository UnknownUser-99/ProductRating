namespace ProductRating.Data.WebAPI.Results
{
    public record ProductWithFullInfoResult
    {
        public string Product { get; init; }
        public string Brand { get; init; }
        public string Type { get; init; }
        public string Image { get; init; }
        public decimal OverallRating { get; init; }
        public decimal YearlyRating { get; init; }
        public decimal MonthlyRating { get; init; }
    }
}