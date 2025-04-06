namespace ProductRating.Data.WebAPI.Results
{
    public record VerificationResult
    {
        public string Id { get; init; }
        public string Role { get; init; }
    }
}