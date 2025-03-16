using ProductRating.Contracts.ProductRecognition;
using ProductRating.Data.Configurations;
using ProductRating.Data.Entities.ProductRecognition;
using Microsoft.Extensions.Options;

namespace ProductRating.Services.ProductRecognition
{
    public class ProductRecognitionService: IProductRecognitionService
    {
        private readonly ProductRecognitionServiceOptions _options;

        public ProductRecognitionService(IOptions<ProductRecognitionServiceOptions> options)
        {
            _options = options.Value;
        }

        public RecognitionResult Recognize(byte[] imageBytes)
        {
            MLProductRecognition.ModelInput inputData = new MLProductRecognition.ModelInput()
            {
                ImageSource = imageBytes,
            };

            MLProductRecognition.ModelOutput result = MLProductRecognition.Predict(inputData);

            float maxScore = result.Score.Max();

            RecognitionConfidenceType confidence = maxScore switch
            {
                float score when score >= _options.HighConfidence => RecognitionConfidenceType.High,
                float score when score >= _options.MediumConfidence => RecognitionConfidenceType.Medium,
                float score when score >= _options.LowConfidence => RecognitionConfidenceType.Low,
                _ => RecognitionConfidenceType.NotRecognized
            };

            if (confidence == RecognitionConfidenceType.NotRecognized)
            {
                return new RecognitionResult
                {
                    Confidence = confidence
                };
            }

            return new RecognitionResult
            {
                Product = int.Parse(result.PredictedLabel),
                Confidence = confidence
            };
        }
    }
}