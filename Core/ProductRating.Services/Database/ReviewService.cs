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