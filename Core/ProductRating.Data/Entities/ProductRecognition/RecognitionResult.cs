namespace ProductRating.Data.Entities.ProductRecognition
{
    public record RecognitionResult
    {
        public int Product { get; init; }
        public float Confidence { get; init; }
    }
}