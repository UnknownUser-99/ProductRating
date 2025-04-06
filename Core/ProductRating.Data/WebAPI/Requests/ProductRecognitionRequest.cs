namespace ProductRating.Data.WebAPI.Requests
{
    public record ProductRecognitionRequest
    {
        public string ImageBase64 { get; init; }
    }
}