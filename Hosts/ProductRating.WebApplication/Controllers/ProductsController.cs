using ProductRating.Contracts.HttpRequest;
using Microsoft.AspNetCore.Mvc;

namespace ProductRating.WebApplication.Controllers
{
    [Route("products")]
    public class ProductsController : Controller
    {
        private readonly IProductRequestService _productRequestService;

        public ProductsController(IProductRequestService productRequestService)
        {
            _productRequestService = productRequestService;
        }

        public IActionResult Products()
        {
            return PartialView();
        }
    }
}