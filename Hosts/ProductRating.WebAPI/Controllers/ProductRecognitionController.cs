using System.Security.Claims;
using ProductRating.Contracts.Database;
using ProductRating.Contracts.DTO;
using ProductRating.Contracts.ProductRecognition;
using ProductRating.Data.ProductRecognition;
using ProductRating.Data.WebAPI.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace ProductRating.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductRecognitionController : Controller
    {
        private readonly IProductRecognitionService _productRecognitionService;
        private readonly IProductService _productService;
        private readonly IRecognitionHistoryService _recognitionHistoryService;
        private readonly IProductRecognitionDTOService _productRecognitionDTOService;

        public ProductRecognitionController(IProductRecognitionService productRecognitionService,IProductService productService,IRecognitionHistoryService recognitionHistoryService,IProductRecognitionDTOService productRecognitionDTOService)
        {
            _productRecognitionService = productRecognitionService;
            _productService = productService;
            _recognitionHistoryService = recognitionHistoryService;
            _productRecognitionDTOService = productRecognitionDTOService;
        }

        [HttpPost]
        public async Task<IActionResult> Recognize([FromBody] ProductRecognitionRequest request)
        {
            var recognitionResult = _productRecognitionService.Recognize(request.ImageBase64);

            if (recognitionResult.Confidence == RecognitionConfidenceType.NotRecognized)
            {
                return NotFound();
            }

            var productResult = await _productService.GetProductWithRatingAsync(recognitionResult.Product);

            if (productResult == null)
            {
                return NotFound();
            }

            //var historyResult = await _recognitionHistoryService.AddRecognitionHistoryAsync(recognitionResult.Product, int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value), recognitionResult.Confidence);

            return Ok(_productRecognitionDTOService.CreateProductRecognitionResult(productResult, recognitionResult.Confidence));
        }
    }
}