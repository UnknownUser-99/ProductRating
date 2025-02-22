using ProductRating.Contracts.Authorization;
using ProductRating.Contracts.Database;
using ProductRating.Data.Configurations;
using ProductRating.Data.Entities.WebAPI.Requests;
using ProductRating.Data.Entities.WebAPI.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ProductRating.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly AuthControllerOptions _options;
        private readonly IHashService _hashService;
        private readonly IJWTService _jwtService;
        private readonly IUserService _userService;

        public AuthController(IOptions<AuthControllerOptions> options, IHashService hashService, IJWTService jwtService, IUserService userService)
        {
            _options = options.Value;
            _hashService = hashService;
            _jwtService = jwtService;
            _userService = userService;
        }

        /*
        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUser([FromBody] RegistrationRequest request)
        {
            if (request == null)
            {
                return BadRequest("Запрос равен null.");
            }


        }
        */
    }
}