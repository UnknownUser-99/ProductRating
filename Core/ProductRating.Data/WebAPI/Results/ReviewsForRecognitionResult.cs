namespace ProductRating.Data.WebAPI.Results
{
    public record ReviewsForRecognitionResult
    {
        public ReviewForRecognitionResult[] Reviews { get; init; }
    }
}