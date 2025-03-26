using ProductRating.Data.Configurations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ProductRating.WebApplication.Controllers
{
    public class AuthorizationController : Controller
    {
        private readonly AuthorizationControllerOptions _options;

        public AuthorizationController(IOptions<AuthorizationControllerOptions> options)
        {
            _options = options.Value;
        }

        [HttpGet]
        public IActionResult Authorization()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Authorization(int phone, string password)
        {
            return View();
        }
    }
}