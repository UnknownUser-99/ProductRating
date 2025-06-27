namespace ProductRating.Data.Configurations
{
    public record UrlAPISchedulerOptions
    {
        public string ProductRatingRequest { get; init; }
        public string ReviewRequest { get; init; }
    }
}