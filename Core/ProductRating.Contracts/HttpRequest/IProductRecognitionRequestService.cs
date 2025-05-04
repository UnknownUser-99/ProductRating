using ProductRating.Data.WebAPI.Results;

namespace ProductRating.Contracts.HttpRequest
{
    public interface IProductRecognitionRequestService
    {
        Task<ProductRecognitionResult> RecognizeAsync(string imageBase64);
    }
}