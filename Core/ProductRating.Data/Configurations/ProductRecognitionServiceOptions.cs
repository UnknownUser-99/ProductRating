namespace ProductRating.Data.Configurations
{
    public record ProductRecognitionServiceOptions
    {
        public float HighConfidence { get; init; }
        public float MediumConfidence { get; init; }
        public float LowConfidence { get; init; }
    }
}