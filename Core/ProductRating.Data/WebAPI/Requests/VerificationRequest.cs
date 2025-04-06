namespace ProductRating.Data.WebAPI.Requests
{
    public record VerificationRequest
    {
        public string Token { get; init; }
    }
}