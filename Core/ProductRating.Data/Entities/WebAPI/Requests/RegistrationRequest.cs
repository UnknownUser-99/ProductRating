namespace ProductRating.Data.Entities.WebAPI.Requests
{
    public record RegistrationRequest
    {
        public int Phone { get; init; }
        public string Name { get; init; }
        public string Password { get; init; }
    }
}