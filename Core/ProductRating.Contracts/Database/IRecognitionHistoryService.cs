using ProductRating.Data.ProductRecognition;

namespace ProductRating.Contracts.Database
{
    public interface IRecognitionHistoryService
    {
        Task<int> AddRecognitionHistoryAsync(int product, int user, RecognitionConfidenceType confidence);
    }
}