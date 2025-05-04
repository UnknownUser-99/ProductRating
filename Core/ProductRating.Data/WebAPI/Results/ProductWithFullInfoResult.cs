namespace ProductRating.Data.WebAPI.Results
{
    public record ProductWithFullInfoResult
    {
        public int Id { get; init; }
        public string Product { get; init; }
        public string Brand { get; init; }
        public string Type { get; init; }
        public string Image { get; init; }
        public string OverallRating { get; init; }
        public string YearlyRating { get; init; }
        public string MonthlyRating { get; init; }
    }
}