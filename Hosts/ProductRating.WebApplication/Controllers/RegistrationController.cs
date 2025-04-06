using ProductRating.Contracts.HttpRequest;
using ProductRating.WebApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace ProductRating.WebApplication.Controllers
{
    [Route("registration")]
    public class RegistrationController : Controller
    {
        private readonly IAuthRequestService _userRequestService;

        public RegistrationController(IAuthRequestService userRequestService)
        {
            _userRequestService = userRequestService;
        }

        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registration(RegistrationModel model)
        {
            var result = await _userRequestService.RegisterUserAsync(model.Phone, model.Name, model.Password);

            if (result == false)
            {
                ModelState.AddModelError("", "Ошибка при регистрации пользователя");

                return View(model);
            }

            return RedirectToAction("Main", "Main");
        }
    }
}