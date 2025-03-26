using ProductRating.WebApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace ProductRating.WebApplication.Controllers
{
    [Route("registration")]
    public class RegistrationController : Controller
    {
        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registration(RegistrationModel model)
        {
            Console.WriteLine($"{model.Name} {model.Phone} {model.Password}");
            return View(model);
        }
    }
}