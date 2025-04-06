namespace ProductRating.Data.WebAPI.Results
{
    public record AuthorizationResult
    {
        public string Token { get; init; }
    }
}