using ProductRating.Data.Models;
using ProductRating.Data.WebAPI.Results;

namespace ProductRating.Contracts.ModelFactory
{
    public interface IRecognitionModelService
    {
        RecognitionModel CreateRecognitionModel(ProductRecognitionResult productRecognition, ReviewModel[] reviewModel);
        RecognitionModel CreateRecognitionModel();
    }
}