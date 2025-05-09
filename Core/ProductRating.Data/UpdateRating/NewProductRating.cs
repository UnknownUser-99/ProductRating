namespace ProductRating.Data.UpdateRating
{
    public record NewProductRating
    {
        public int Product { get; init; }
        public decimal Rating { get; init; }
    }
}