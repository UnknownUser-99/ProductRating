namespace ProductRating.Data.ProductRecognition
{
    public record RecognitionResult
    {
        public int Product { get; init; }
        public RecognitionConfidenceType Confidence { get; init; }
    }
}