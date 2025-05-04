using System.Security.Claims;
using ProductRating.Contracts.Authorization;
using ProductRating.Data.Configurations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication;

namespace ProductRating.Services.Authorization
{
    public class CookieService : ICookieService
    {
        private readonly CookieServiceOptions _options;

        public CookieService(IOptions<CookieServiceOptions> options)
        {
            _options = options.Value;
        }

        public void CreateTokenCookie(HttpContext httpContext, string token)
        {
            CookieOptions cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                Expires = DateTime.UtcNow.Add(TimeSpan.FromDays(_options.AuthCookieTime)),
                SameSite = SameSiteMode.Strict
            };

            httpContext.Response.Cookies.Append(_options.AuthCookieName, token, cookieOptions);
        }

        public void CreateAuthCookie(HttpContext httpContext, string id, string role)
        {
            CookieOptions cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
            };

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, id),
                new Claim(ClaimTypes.Role, role)
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            httpContext.Response.Cookies.Append("AuthCookie", "Auth", cookieOptions);

            httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);
        }
    }
}