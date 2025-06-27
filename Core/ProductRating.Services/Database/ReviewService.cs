using ProductRating.Contracts.Database;
using ProductRating.Data.Database;
using ProductRating.Data.WebAPI.Results;
using Microsoft.EntityFrameworkCore;

namespace ProductRating.Services.Database
{
    public class ReviewService : IReviewService
    {
        private readonly PRDbContext _context;

        public ReviewService(PRDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddReviewAsync(int user, int product, int rating, string description)
        {
            //var checkResult = await _context.Reviews.AnyAsync(r => r.User == user && r.Product == product);

            Review review = new Review
            {
                User = user,
                Product = product,
                Rating = rating,
                Description = description
            };

            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();

            return review.Id;
        }

        public async Task<ReviewRatingResult[]> GetReviewsForUpdateRatingAsync()
        {
            var result = await _context.Reviews
                .Select(r => new ReviewRatingResult
                {
                    Product = r.Product,
                    Rating = r.Rating
                })
                .ToArrayAsync();

            return result;
        }

        public async Task<ReviewRatingResult[]> GetReviewsForUpdateOverallRatingAsync()
        {
            var yesterday = DateTime.UtcNow.AddDays(-1).Date;

            var result = await _context.Reviews
                .Where(r => _context.Reviews
                    .Where(r2 => r2.Date >= yesterday && r2.Date < yesterday.AddDays(1))
                    .Select(r2 => r2.Product)
                    .Distinct()
                    .Contains(r.Product)
                )
                .Select(r => new ReviewRatingResult
                 {
                     Product = r.Product,
                     Rating = r.Rating
                 })
                .ToArrayAsync();

            return result;
        }

        public async Task<ReviewRatingResult[]> GetReviewsForUpdateYearlyRatingAsync()
        {
            var today = DateTime.UtcNow;
            var firstDayOfLastYear = new DateTime(today.Year - 1, 1, 1);
            var lastDayOfLastYear = new DateTime(today.Year - 1, 12, 31);

            var result = await _context.Reviews
                .Where(r => _context.Reviews
                    .Where(r2 => r2.Date >= firstDayOfLastYear && r2.Date <= lastDayOfLastYear)
                    .Select(r2 => r2.Product)
                    .Distinct()
                    .Contains(r.Product)
                )
                .Select(r => new ReviewRatingResult
                {
                    Product = r.Product,
                    Rating = r.Rating
                })
                .ToArrayAsync();

            return result;
        }

        public async Task<ReviewRatingResult[]> GetReviewsForUpdateMonthlyRatingAsync()
        {
            var today = DateTime.UtcNow;
            var firstDayOfLastMonth = new DateTime(today.Year, today.Month, 1).AddMonths(-1);
            var lastDayOfLastMonth = firstDayOfLastMonth.AddMonths(1).AddDays(-1);

            var result = await _context.Reviews
                .Where(r => _context.Reviews
                    .Where(r2 => r2.Date >= firstDayOfLastMonth && r2.Date <= lastDayOfLastMonth)
                    .Select(r2 => r2.Product)
                    .Distinct()
                    .Contains(r.Product)
                )
                .Select(r => new ReviewRatingResult
                {
                    Product = r.Product,
                    Rating = r.Rating
                })
                .ToArrayAsync();

            return result;
        }

        public async Task<ReviewForRecognitionResult[]> GetReviewsForRecognitionAsync(int product, int count = 5)
        {
            var result = await (
                from review in _context.Reviews
                join user in _context.Users on review.User equals user.Id
                where review.Product == product
                orderby review.Date descending
                select new ReviewForRecognitionResult
                {
                    User = user.Name,
                    Rating = review.Rating,
                    Description = review.Description,
                    Date = DateOnly.FromDateTime(review.Date)
                })
                .Take(count)
                .ToArrayAsync();

            return result;
        }
    }
}