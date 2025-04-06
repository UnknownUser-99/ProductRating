using ProductRating.Contracts.HttpRequest;
using ProductRating.Data.Configurations;
using ProductRating.WebApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ProductRating.WebApplication.Controllers
{
    public class RecognitionController : Controller
    {
        private readonly RecognitionControllerOptions _options;
        private readonly IProductRecognitionRequestService _productRecognitionRequestService;

        public RecognitionController(IOptions<RecognitionControllerOptions> options, IProductRecognitionRequestService productRecognitionRequestService)
        {
            _options = options.Value;
            _productRecognitionRequestService = productRecognitionRequestService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> Recognize(IFormFile imageFile)
        {
            if (imageFile == null || imageFile.Length == 0)
            {
                return PartialView(_options.View, new RecognitionModel
                {
                    Product = "",
                    Brand = "",
                    Type = "",
                    OverallRating = "",
                    YearlyRating = "",
                    MonthlyRating = "",
                    Confidence = ""
                });
            }

            using (MemoryStream memoryStream = new MemoryStream())
            {
                await imageFile.CopyToAsync(memoryStream);

                byte[] imageBytes = memoryStream.ToArray();
                string imageBase64 = Convert.ToBase64String(imageBytes);

                var result = await _productRecognitionRequestService.RecognizeAsync(imageBase64);

                if (result == null)
                {
                    return PartialView(_options.View, new RecognitionModel
                    {
                        Product = "",
                        Brand = "",
                        Type = "",
                        OverallRating = "",
                        YearlyRating = "",
                        MonthlyRating = "",
                        Confidence = "Изображение не распознано"
                    });
                }               

                return PartialView(_options.View, new RecognitionModel
                {
                    Product = result.Product.Product,
                    Brand = result.Product.Brand,
                    Type = result.Product.Type,
                    Image = result.Product.Image,
                    OverallRating = result.Product.OverallRating.ToString(),
                    YearlyRating = result.Product.YearlyRating.ToString(),
                    MonthlyRating = result.Product.MonthlyRating.ToString(),
                    Confidence = result.Confidence
                });
            }
        }
    }
}