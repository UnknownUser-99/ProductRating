namespace ProductRating.Data.WebAPI.Requests
{
    public record AuthorizationRequest
    {
        public string Phone { get; init; }
        public string Password { get; init; }
    }
}