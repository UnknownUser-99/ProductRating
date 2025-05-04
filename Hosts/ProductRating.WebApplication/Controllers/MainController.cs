using ProductRating.Contracts.HttpRequest;
using ProductRating.Contracts.ModelFactory;
using Microsoft.AspNetCore.Mvc;

namespace ProductRating.WebApplication.Controllers
{
    public class MainController : Controller
    {
        private readonly IAuthRequestService _authRequestService;
        private readonly IProductRequestService _productRequestService;
        private readonly IProductModelService _productModelService;

        public MainController(IAuthRequestService authRequestService, IProductRequestService productRequestService, IProductModelService productModelFactory)
        {
            _authRequestService = authRequestService;
            _productRequestService = productRequestService;
            _productModelService = productModelFactory;
        }

        [HttpGet]
        public async Task<ActionResult> Main()
        {
            string token = Request.Cookies["AuthToken"];

            if (string.IsNullOrWhiteSpace(token))
            {
                return RedirectToAction("Authorization", "Authorization");
            }

            var result = await _authRequestService.VerifyAsync(token);

            if (string.IsNullOrWhiteSpace(result.Id) || string.IsNullOrWhiteSpace(result.Role))
            {
                return RedirectToAction("Authorization", "Authorization");
            }

            return View();
        }

        [HttpGet]
        public ActionResult Profile()
        {
            return PartialView();
        }

        [HttpGet]
        public ActionResult Recognition()
        {
            return PartialView();
        }

        [HttpGet]
        public ActionResult Reviews()
        {
            return PartialView();
        }

        [HttpGet]
        public async Task<ActionResult> Products()
        {
            var result = await _productRequestService.GetProductCardsAsync();       

            return PartialView(_productModelService.CreateProductModels(result));
        }
    }
}