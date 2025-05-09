using System.Security.Claims;
using ProductRating.Contracts.Database;
using ProductRating.Contracts.DTO;
using ProductRating.Data.WebAPI.Requests;
using ProductRating.WebAPI.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ProductRating.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ReviewController : Controller
    {
        private readonly IReviewService _reviewService;
        private readonly IReviewDTOService _reviewDTOService;

        public ReviewController(IReviewService reviewService, IReviewDTOService reviewDTOService)
        {
            _reviewService = reviewService;
            _reviewDTOService = reviewDTOService;
        }

        [HttpGet("ReviewsForUpdateRating")]
        public async Task<IActionResult> GetReviewsForUpdateRating()
        {
            var result = await _reviewService.GetReviewsForUpdateRatingAsync();

            if (result.Length == 0)
            {
                return NotFound("Отзывы не найдены.");
            }

            return Ok(_reviewDTOService.CreateReviewsForUpdateRatingResult(result));
        }

        [HttpGet("ReviewsForRecognition")]
        public async Task<IActionResult> GetReviewsForRecognition([FromQuery] int product)
        {
            var result = await _reviewService.GetReviewsForRecognitionAsync(product);

            if (result.Length == 0)
            {
                return NotFound("Отзывы не найдены.");
            }

            return Ok(_reviewDTOService.CreateReviewsForRecognitionResult(result));
        }

        [HttpPost]
        [ServiceFilter(typeof(AddReviewFilter))]
        public async Task<IActionResult> AddReview([FromBody] AddReviewRequest request)
        {
            var user = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var result = await _reviewService.AddReviewAsync(user, request.Product, request.Rating, request.Description);

            return Created($"api/review/{result}", null);
        }
    }
}