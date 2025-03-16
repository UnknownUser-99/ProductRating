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

        public ActionResult Main()
        {
            return View();
        }

        public ActionResult Profile()
        {
            return PartialView("Profile");
        }

        public ActionResult Recognition()
        {
            return PartialView(_options.RecognitionView);
        }

        public ActionResult Reviews()
        {
            return PartialView("Reviews");
        }

        public ActionResult Products()
        {
            return PartialView("Products");
        }
    }
}