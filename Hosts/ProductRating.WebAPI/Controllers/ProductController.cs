using ProductRating.Contracts.Database;
using ProductRating.Data.Entities.WebAPI.Requests;
using ProductRating.Data.Entities.WebAPI.Results;
using Microsoft.AspNetCore.Mvc;

namespace ProductRating.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            try
            {
                var result = await _productService.GetProducts();

                if (result == null || result.Length == 0)
                {
                    return NotFound("Товары не найдены");
                }

                return Ok(result);
            }
            catch (Exception)
            {
                return Problem(title: "Ошибка при работе с базой данных.", statusCode: 500);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            try
            {
                var result = await _productService.GetProductById(id);

                if (result == null)
                {
                    return NotFound("Товар не найден");
                }

                return Ok(result);
            }
            catch (Exception)
            {
                return Problem(title: "Ошибка при работе с базой данных.", statusCode: 500);
            }
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetProductByName(string name)
        {
            try
            {
                var result = await _productService.GetProductsByName(name);

                if (result == null || result.Length == 0)
                {
                    return NotFound("Товары не найдены");
                }

                return Ok(result);
            }
            catch (Exception)
            {
                return Problem(title: "Ошибка при работе с базой данных.", statusCode: 500);
            }
        }
    }
}