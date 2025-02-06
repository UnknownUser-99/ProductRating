using ProductRating.Data.Entities.ProductRecognition;

namespace ProductRating.Contracts.ProductRecognition
{
    public interface IProductRecognitionService
    {
        RecognitionResult Recognize(byte[] imageBytes);
    }
}