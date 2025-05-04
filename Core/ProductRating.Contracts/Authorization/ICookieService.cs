using Microsoft.AspNetCore.Http;

namespace ProductRating.Contracts.Authorization
{
    public interface ICookieService
    {
        void CreateTokenCookie(HttpContext httpContext, string token);
    }
}