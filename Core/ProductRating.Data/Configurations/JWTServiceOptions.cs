namespace ProductRating.Data.Configurations
{
    public record JWTServiceOptions
    {
        public string SecretKey { get; init; }
        public string Issuer { get; init; }
    }
}