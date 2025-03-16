namespace ProductRating.Data.Entities.WebAPI.Results
{
    public record ProductRecognitionResult
    {
        public ProductWithFullInfoResult Product { get; init; }
        public string Confidence { get; init; }
    }
}