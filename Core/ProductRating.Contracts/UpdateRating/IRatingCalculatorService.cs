namespace ProductRating.Contracts.UpdateRating
{
    public interface IRatingCalculatorService
    {
        decimal CalculateRating(int[] ratings);
    }
}