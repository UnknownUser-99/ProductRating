using System.Drawing;
using System.Globalization;
using ProductRating.Contracts.ModelFactory;
using ProductRating.Data.Models;
using ProductRating.Data.ProductRecognition;
using ProductRating.Data.WebAPI.Results;

namespace ProductRating.Services.ModelFactory
{
    public class RecognitionModelService : IRecognitionModelService
    {
        public RecognitionModel CreateRecognitionModel(ProductRecognitionResult productRecognition, ReviewModel[] reviewModel)
        {
            if (productRecognition == null)
            {
                return new RecognitionModel();
            }

            RecognitionConfidenceType confidenceType = Enum.Parse<RecognitionConfidenceType>(productRecognition.Confidence);

            var (confidence, confidenceColor) = confidenceType switch
            {
                RecognitionConfidenceType.High => ("Высокая уверенность распознавания", Color.Green),
                RecognitionConfidenceType.Medium => ("Средняя уверенность распознавания", Color.Blue),
                RecognitionConfidenceType.Low => ("Низкая уверенность распознавания", Color.Red)
            };

            return new RecognitionModel
            {
                Id = productRecognition.Product.Id,
                Product = productRecognition.Product.Product,
                Brand = productRecognition.Product.Brand,
                Type = productRecognition.Product.Type,
                Image = productRecognition.Product.Image,
                OverallRating = decimal.Parse(productRecognition.Product.OverallRating, CultureInfo.InvariantCulture),
                YearlyRating = decimal.Parse(productRecognition.Product.YearlyRating, CultureInfo.InvariantCulture),
                MonthlyRating = decimal.Parse(productRecognition.Product.MonthlyRating, CultureInfo.InvariantCulture),
                Confidence = confidence,
                ConfidenceColor = confidenceColor.Name.ToLower(),
                Reviews = reviewModel
            };
        }

        public RecognitionModel CreateRecognitionModel()
        {
            return new RecognitionModel();
        }
    }
}