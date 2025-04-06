using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace ProductRating.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AdminController : Controller
    {
        /*
        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProduct([FromBody] RegistrationRequest request)
        {

        }
        */
    }
}