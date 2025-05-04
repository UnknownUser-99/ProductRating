namespace ProductRating.Data.WebAPI.Results
{
    public record ReviewForRecognitionResult
    {
        public string User { get; init; }
        public int Rating { get; init; }
        public string Description { get; init; }
        public DateOnly Date { get; init; }
    }
}