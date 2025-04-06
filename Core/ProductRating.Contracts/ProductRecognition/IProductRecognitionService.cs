using ProductRating.Data.ProductRecognition;

namespace ProductRating.Contracts.ProductRecognition
{
    public interface IProductRecognitionService
    {
        RecognitionResult Recognize(string imageBase64);
    }
}