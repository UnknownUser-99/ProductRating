using ProductRating.Contracts.Database;
using ProductRating.Data.Database;
using ProductRating.Data.ProductRecognition;

namespace ProductRating.Services.Database
{
    public class RecognitionHistoryService : IRecognitionHistoryService
    {
        private readonly PRDbContext _context;

        public RecognitionHistoryService(PRDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddRecognitionHistoryAsync(int product, int user, RecognitionConfidenceType confidence)
        {
            if (product < 1)
            {
                throw new ArgumentException("Product меньше 1.", nameof(product));
            }

            if (user < 1)
            {
                throw new ArgumentException("User меньше 1.", nameof(user));
            }

            RecognitionHistory history = new RecognitionHistory()
            {
                Product = product,
                User = user,
                Confidence = (int)confidence
            };

            _context.RecognitionHistory.Add(history);
            await _context.SaveChangesAsync();

            return history.Id;
        }
    }
}