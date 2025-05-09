using ProductRating.Contracts.UpdateRating;

namespace ProductRating.Services.UpdateRating
{
    public class RatingCalculatorService : IRatingCalculatorService
    {
        public decimal CalculateRating(int[] ratings)
        {
            if (ratings.Length == 0)
            {
                throw new ArgumentException("Передан пустой массив рейтингов.", nameof(ratings));
            }

            if (ratings.Length == 1)
            {
                return decimal.Round(ratings[0], 2);
            }

            int sum = ratings.Sum();
            decimal rating = (decimal)sum / ratings.Length;

            return decimal.Round(rating, 2);
        }
    }
}