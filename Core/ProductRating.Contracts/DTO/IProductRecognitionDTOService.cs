using ProductRating.Data.ProductRecognition;
using ProductRating.Data.WebAPI.Results;

namespace ProductRating.Contracts.DTO
{
    public interface IProductRecognitionDTOService
    {
        ProductRecognitionResult CreateProductRecognitionResult(RecognitionConfidenceType confidence);
        ProductRecognitionResult CreateProductRecognitionResult(ProductWithFullInfoResult product, RecognitionConfidenceType confidence);
    }
}