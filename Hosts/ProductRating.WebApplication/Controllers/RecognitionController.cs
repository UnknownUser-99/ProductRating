using System.Text;
using System.Text.Json;
using ProductRating.Data.Configurations;
using ProductRating.Data.Entities.WebAPI.Requests;
using ProductRating.Data.Entities.WebAPI.Results;
using ProductRating.WebApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ProductRating.WebApplication.Controllers
{
    public class RecognitionController : Controller
    {
        private readonly RecognitionControllerOptions _options;
        private readonly IHttpClientFactory _httpClientFactory;

        public RecognitionController(IOptions<RecognitionControllerOptions> options, IHttpClientFactory httpClientFactory)
        {
            _options = options.Value;
            _httpClientFactory = httpClientFactory;
        }

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

                ProductRecognitionRequest requestBody = new ProductRecognitionRequest
                {
                    ImageBase64 = imageBase64
                };

                StringContent jsonContent = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

                HttpClient client = _httpClientFactory.CreateClient();

                HttpResponseMessage response = await client.PostAsync(_options.Url, jsonContent);

                if (!response.IsSuccessStatusCode)
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

                string resultString = await response.Content.ReadAsStringAsync();

                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                ProductRecognitionResult result = JsonSerializer.Deserialize<ProductRecognitionResult>(resultString, options);

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