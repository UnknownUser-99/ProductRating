using System.Net.Http.Headers;
using ProductRating.Data.Configurations;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace ProductRating.Services.HttpRequest
{
    public class AuthTokenHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly CookieServiceOptions _options;

        public AuthTokenHandler(IHttpContextAccessor httpContextAccessor, IOptions<CookieServiceOptions> options)
        {
            _httpContextAccessor = httpContextAccessor;
            _options = options.Value;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var httpContext = _httpContextAccessor.HttpContext;

            if (httpContext != null && httpContext.Request.Cookies.TryGetValue(_options.AuthCookieName, out var token))
            {
                if (!string.IsNullOrWhiteSpace(token))
                {
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}