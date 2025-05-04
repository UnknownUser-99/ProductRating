using ProductRating.Contracts.DTO;
using ProductRating.Data.ProductRecognition;
using ProductRating.Data.WebAPI.Results;

namespace ProductRating.Services.DTO
{
    public class ProductRecognitionDTOService : IProductRecognitionDTOService
    {
        public ProductRecognitionResult CreateProductRecognitionResult(RecognitionConfidenceType confidence)
        {
            return new ProductRecognitionResult
            {
                Confidence = confidence.ToString()
            };
        }

        public ProductRecognitionResult CreateProductRecognitionResult(ProductWithFullInfoResult product, RecognitionConfidenceType confidence)
        {
            return new ProductRecognitionResult
            {
                Product = product,
                Confidence = confidence.ToString()
            };
        }
    }
}