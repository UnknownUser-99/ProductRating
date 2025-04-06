namespace ProductRating.Data.Configurations
{
    public record CookieServiceOptions
    {
        public string AuthCookieName { get; init; }
        public int AuthCookieTime { get; init; }
    }
}