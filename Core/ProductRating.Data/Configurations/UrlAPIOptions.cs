namespace ProductRating.Data.Configurations
{
    public record UrlAPIOptions
    {
        public string AuthRequest { get; init; }
        public string ProductRecognitionRequest { get; init; }
        public string ProductRequest { get; init; }
        public string ReviewRequest { get; init; }
    }
}