using ProductRating.Contracts.HttpRequest;
using Microsoft.AspNetCore.Http;

namespace ProductRating.Services.Authorization
{
    public class AuthService
    {
        private readonly IAuthRequestService _authRequestService;

        public AuthService(IAuthRequestService authRequestService)
        {
            _authRequestService = authRequestService;
        }

        //TODO: Доделать
        public async Task<bool> AuthWeb(HttpContext httpContext)
        {
            var token = httpContext.Request.Cookies["AuthToken"];

            if (string.IsNullOrWhiteSpace(token))
            {
                return false;
            }

            var result = await _authRequestService.VerifyAsync(token);

            if (result == null)
            {
                return false;
            }

            return true;
        }
    }
}