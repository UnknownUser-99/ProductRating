﻿using ProductRating.Contracts.Database;
using ProductRating.Data.WebAPI.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace ProductRating.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductRatingController : Controller
    {
        private readonly IProductRatingService _productRatingService;

        public ProductRatingController(IProductRatingService productRatingService)
        {
            _productRatingService = productRatingService;
        }

        [HttpPut("UpdateInitialProductRatings")]
        public async Task<IActionResult> UpdateInitialProductRatings([FromBody] UpdateProductRatingsRequest request)
        {
            foreach (var rating in request.Ratings)
            {
                await _productRatingService.UpdateAllProductRatingAsync(rating.Product, rating.Rating);
            }

            return NoContent();
        }

        [HttpPut("UpdateOverallProductRatings")]
        public async Task<IActionResult> UpdateOverallProductRatings([FromBody] UpdateProductRatingsRequest request)
        {
            foreach (var rating in request.Ratings)
            {
                await _productRatingService.UpdateOverallProductRatingAsync(rating.Product, rating.Rating);
            }

            return NoContent();
        }

        [HttpPut("UpdateYearlyProductRatings")]
        public async Task<IActionResult> UpdateYearlyProductRatings([FromBody] UpdateProductRatingsRequest request)
        {
            foreach (var rating in request.Ratings)
            {
                await _productRatingService.UpdateYearlyProductRatingAsync(rating.Product, rating.Rating);
            }

            return NoContent();
        }

        [HttpPut("UpdateMonthlyProductRatings")]
        public async Task<IActionResult> UpdateMonthlyProductRatings([FromBody] UpdateProductRatingsRequest request)
        {
            foreach (var rating in request.Ratings)
            {
                await _productRatingService.UpdateMonthlyProductRatingAsync(rating.Product, rating.Rating);
            }

            return NoContent();
        }
    }
}