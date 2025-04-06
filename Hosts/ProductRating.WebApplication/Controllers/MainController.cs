using ProductRating.Contracts.HttpRequest;
using ProductRating.Data.Configurations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ProductRating.WebApplication.Controllers
{
    public class MainController : Controller
    {
        private readonly MainControllerOptions _options;
        private readonly IAuthRequestService _authRequestService;

        public MainController(IOptions<MainControllerOptions> options, IAuthRequestService authRequestService)
        {
            _options = options.Value;
            _authRequestService = authRequestService;
        }

        [HttpGet]
        public async Task<ActionResult> Main()
        {
            string token = Request.Cookies["AuthToken"];
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
            return PartialView("Profile");
        }

        [HttpGet]
        public ActionResult Recognition()
        {
            return PartialView(_options.RecognitionView);
        }

        [HttpGet]
        public ActionResult Reviews()
        {
            return PartialView("Reviews");
        }

        [HttpGet]
        public ActionResult Products()
        {
            return PartialView(_options.ProductsView);
        }
    }
}