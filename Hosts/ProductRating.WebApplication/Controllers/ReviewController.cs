using ProductRating.Contracts.HttpRequest;
using ProductRating.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace ProductRating.WebApplication.Controllers
{
    public class ReviewController : Controller
    {
        private readonly IReviewRequestService _reviewRequestService;

        public ReviewController(IReviewRequestService reviewRequestService)
        {
            _reviewRequestService = reviewRequestService;
        }

        [HttpGet]
        public IActionResult Review()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Review(AddReviewModel model)
        {
            var result = await _reviewRequestService.AddReviewAsync(model.Product, model.Rating, model.Description);

            return PartialView("~/Views/Main/Recognition.cshtml");
        }
    }
}