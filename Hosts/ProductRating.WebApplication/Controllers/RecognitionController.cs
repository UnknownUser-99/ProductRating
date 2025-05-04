using ProductRating.Contracts.HttpRequest;
using ProductRating.Contracts.ModelFactory;
using ProductRating.Data.Configurations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ProductRating.WebApplication.Controllers
{
    public class RecognitionController : Controller
    {
        private readonly RecognitionControllerOptions _options;
        private readonly IProductRecognitionRequestService _productRecognitionRequestService;
        private readonly IReviewRequestService _reviewRequestService;
        private readonly IRecognitionModelService _recognitionModelService;
        private readonly IReviewModelService _reviewModelService;

        public RecognitionController(IOptions<RecognitionControllerOptions> options, IProductRecognitionRequestService productRecognitionRequestService, IReviewRequestService reviewRequestService, IRecognitionModelService recognitionModelService, IReviewModelService reviewModelService)
        {
            _options = options.Value;
            _productRecognitionRequestService = productRecognitionRequestService;
            _reviewRequestService = reviewRequestService;
            _recognitionModelService = recognitionModelService;
            _reviewModelService = reviewModelService;
        }

        [HttpGet]
        public IActionResult Recognition()
        {
            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> Recognition(IFormFile imageFile)
        {
            if (imageFile == null || imageFile.Length == 0)
            {
                return PartialView(_options.MainView, _recognitionModelService.CreateRecognitionModel());
            }

            using (MemoryStream memoryStream = new MemoryStream())
            {
                await imageFile.CopyToAsync(memoryStream);

                byte[] imageBytes = memoryStream.ToArray();
                string imageBase64 = Convert.ToBase64String(imageBytes);

                var recognitionResult = await _productRecognitionRequestService.RecognizeAsync(imageBase64);

                if (recognitionResult == null)
                {
                    return PartialView(_options.MainView, _recognitionModelService.CreateRecognitionModel());
                }

                var reviewsResult = await _reviewRequestService.GetReviewForRecognition(recognitionResult.Product.Id);

                return PartialView(_options.MainView, _recognitionModelService.CreateRecognitionModel(recognitionResult, _reviewModelService.CreateReviewModels(reviewsResult)));
            }
        }
    }
}