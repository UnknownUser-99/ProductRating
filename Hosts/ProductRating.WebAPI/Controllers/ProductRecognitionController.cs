using ProductRating.Contracts.ProductRecognition;
using ProductRating.Data.Entities.WebAPI.Requests;
using ProductRating.Data.Entities.WebAPI.Results;
using Microsoft.AspNetCore.Mvc;

namespace ProductRating.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductRecognitionController : Controller
    {
        private readonly IProductRecognitionService _productRecognitionService;

        public ProductRecognitionController(IProductRecognitionService productRecognitionService)
        {
            _productRecognitionService = productRecognitionService;
        }

        [HttpPost]
        public async Task<IActionResult> Recognize([FromBody] ProductRecognitionRequest request)
        {
            byte[] imageBytes = Convert.FromBase64String(request.ImageBase64);

            var result = _productRecognitionService.Recognize(imageBytes);

            return Ok(result);
        }
    }
}