namespace ProductRating.Data.Entities.WebAPI.Requests
{
    public record AuthorizationRequest
    {
        public int Phone { get; init; }
        public string Password { get; init; }
    }
}