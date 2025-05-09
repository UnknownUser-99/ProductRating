using ProductRating.Contracts.Database;
using ProductRating.Data.Database;
using Microsoft.EntityFrameworkCore;

namespace ProductRating.Services.Database
{
    public class ProductRatingService : IProductRatingService
    {
        private readonly PRDbContext _context;

        public ProductRatingService(PRDbContext context)
        {
            _context = context;
        }

        public async Task<bool> UpdateAllProductRatingAsync(int product, decimal rating)
        {
            var productRating = await _context.ProductRatings.FirstOrDefaultAsync(pr => pr.Product == product);

            if (productRating == null)
            {
                return false;
            }

            productRating.OverallRating = rating;
            productRating.YearlyRating = rating;
            productRating.MonthlyRating = rating;

            _context.Update(productRating);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}