using ProductRating.Contracts.Authorization;
using ProductRating.Contracts.HttpRequest;
using ProductRating.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace ProductRating.WebApplication.Controllers
{
    [Route("authorization")]
    public class AuthorizationController : Controller
    {
        private readonly IAuthRequestService _authRequestService;
        private readonly ICookieService _cookieService;

        public AuthorizationController(IAuthRequestService userRequestService, ICookieService cookieService)
        {
            _authRequestService = userRequestService;
            _cookieService = cookieService;
        }

        [HttpGet]
        public IActionResult Authorization()
        {
            return View("~/Views/Auth/Authorization.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> Authorization(AuthorizationModel model)
        {
            var result = await _authRequestService.AuthorizeAsync(model.Phone, model.Password);

            if (result == null)
            {
                ModelState.AddModelError("", "Ошибка при авторизации пользователя");

                return View(model);
            }

            _cookieService.CreateTokenCookie(HttpContext, result.Token);

            return RedirectToAction("Main", "Main");
        }
    }
}