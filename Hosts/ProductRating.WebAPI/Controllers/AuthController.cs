using ProductRating.Contracts.Authorization;
using ProductRating.Contracts.Database;
using ProductRating.Contracts.DTO;
using ProductRating.Data.Configurations;
using ProductRating.Data.WebAPI.Requests;
using ProductRating.WebAPI.Filters;
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
        private readonly IAuthDTOService _userDTOService;

        public AuthController(IOptions<AuthControllerOptions> options, IHashService hashService, IJWTService jwtService, IUserService userService, IAuthDTOService userDTOService)
        {
            _options = options.Value;
            _hashService = hashService;
            _jwtService = jwtService;
            _userService = userService;
            _userDTOService = userDTOService;
        }

        [HttpPost("Registration")]
        [ServiceFilter(typeof(RegistrationFilter))]
        public async Task<IActionResult> Registration([FromBody] RegistrationRequest request)
        {
            var password = _hashService.HashPassword(request.Password);

            var result = await _userService.AddUserAsync(request.Phone, request.Name, password);          

            return Created("", null);
        }

        [HttpPost("Authorization")]
        [ServiceFilter(typeof(AuthorizationFilter))]
        public async Task<IActionResult> Authorization([FromBody] AuthorizationRequest request)
        {
            var user = await _userService.GetUserByPhoneAsync(request.Phone);

            if (user == null || !_hashService.VerifyPassword(request.Password, user.Password))
            {
                return Unauthorized("Неверный Phone или Password.");
            }

            var token = _jwtService.GenerateToken(user.Id, user.Role);

            return Ok(_userDTOService.CreateAuthorizationResult(token));
        }

        [HttpPost("Verification")]
        [ServiceFilter(typeof(VerificationFilter))]
        public IActionResult Verification([FromBody] VerificationRequest request)
        {
            var result = _jwtService.VerifyToken(request.Token);

            if (result == null)
            {
                return Unauthorized("Неверный Token.");
            }

            return Ok(_userDTOService.CreateVerificationResult(result));
        }
    }
}