namespace ProductRating.Data.WebAPI.Results
{
    public record ProductCardResult
    {
        public string Name { get; init; }
        public string Image { get; init; }
        public string Rating { get; init; }
    }
}