namespace ProductRating.Data.WebAPI.Requests
{
    public record RegistrationRequest
    {
        public string Phone { get; init; }
        public string Name { get; init; }
        public string Password { get; init; }
    }
}