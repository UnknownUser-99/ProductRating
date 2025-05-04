using ProductRating.Contracts.Database;
using ProductRating.Contracts.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace ProductRating.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IProductDTOService _productDTOService;

        public ProductController(IProductService productService, IProductDTOService productDTOService)
        {
            _productService = productService;
            _productDTOService = productDTOService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var result = await _productService.GetProductsAsync();

            if (result == null || result.Length == 0)
            {
                return NotFound("Товары не найдены");
            }

            return Ok(result);
        }

        [HttpGet("ProductCards")]
        public async Task<IActionResult> GetProductCards()
        {
            var result = await _productService.GetProductCardsAsync();

            if (result == null || result.Length == 0)
            {
                return NotFound("Товары не найдены");
            }

            return Ok(_productDTOService.CreateProductCardsResult(result));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var result = await _productService.GetProductByIdAsync(id);

            if (result == null)
            {
                return NotFound("Товар не найден");
            }

            return Ok(result);
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetProductByName(string name)
        {
            var result = await _productService.GetProductsByNameAsync(name);

            if (result == null || result.Length == 0)
            {
                return NotFound("Товары не найдены");
            }

            return Ok(result);
        }
    }
}