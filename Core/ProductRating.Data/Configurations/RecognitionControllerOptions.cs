namespace ProductRating.Data.Configurations
{
    public record RecognitionControllerOptions
    {
        public string Url { get; init; }
        public string View { get; init; }
    }
}