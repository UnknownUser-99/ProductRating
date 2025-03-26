using ProductRating.Data.Configurations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ProductRating.WebApplication.Controllers
{
    public class MainController : Controller
    {
        private readonly MainControllerOptions _options;

        public MainController(IOptions<MainControllerOptions> options)
        {
            _options = options.Value;
        }

        [HttpGet]
        public ActionResult Main()
        {
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
            return PartialView("Products");
        }
    }
}