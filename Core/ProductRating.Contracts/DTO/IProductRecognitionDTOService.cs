using ProductRating.Data.Entities.ProductRecognition;
using ProductRating.Data.Entities.WebAPI.Results;

namespace ProductRating.Contracts.DTO
{
    public interface IProductRecognitionDTOService
    {
        ProductRecognitionResult CreateProductRecognitionResult(RecognitionConfidenceType confidence);
        ProductRecognitionResult CreateProductRecognitionResult(ProductWithFullInfoResult product, RecognitionConfidenceType confidence);
    }
}