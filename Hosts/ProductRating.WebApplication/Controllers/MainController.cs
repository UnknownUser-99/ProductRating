using Microsoft.AspNetCore.Mvc;

namespace ProductRating.WebApplication.Controllers
{
    public class MainController : Controller
    {
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
            return PartialView("Recognition");
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