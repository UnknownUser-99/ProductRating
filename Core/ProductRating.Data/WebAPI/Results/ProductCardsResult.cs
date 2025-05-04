namespace ProductRating.Data.WebAPI.Results
{
    public record ProductCardsResult
    {
        public ProductCardResult[] ProductCards { get; init; }
    }
}