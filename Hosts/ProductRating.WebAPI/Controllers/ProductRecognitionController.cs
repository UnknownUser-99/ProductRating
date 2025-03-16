using ProductRating.Contracts.Database;
using ProductRating.Contracts.DTO;
using ProductRating.Contracts.ProductRecognition;
using ProductRating.Data.Entities.ProductRecognition;
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
        private readonly IProductService _productService;
        private readonly IProductRecognitionDTOService _productRecognitionDTOService;

        public ProductRecognitionController(IProductRecognitionService productRecognitionService,IProductService productService, IProductRecognitionDTOService productRecognitionDTOService)
        {
            _productRecognitionService = productRecognitionService;
            _productService = productService;
            _productRecognitionDTOService = productRecognitionDTOService;
        }

        [HttpPost]
        public async Task<IActionResult> Recognize([FromBody] ProductRecognitionRequest request)
        {
            byte[] imageBytes = Convert.FromBase64String(request.ImageBase64);

            var recognitionResult = _productRecognitionService.Recognize(imageBytes);

            if (recognitionResult.Confidence == RecognitionConfidenceType.NotRecognized)
            {
                return NotFound();
            }

            var productResult = await _productService.GetProductWithRating(recognitionResult.Product);

            if (productResult == null)
            {
                return NotFound();
            }

            return Ok(_productRecognitionDTOService.CreateProductRecognitionResult(productResult, recognitionResult.Confidence));
        }
    }
}