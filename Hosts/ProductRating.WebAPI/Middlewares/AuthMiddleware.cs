using ProductRating.Contracts.Authorization;

namespace ProductRating.WebAPI.Middlewares
{
    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path.StartsWithSegments("/swagger", StringComparison.OrdinalIgnoreCase) || context.Request.Path.StartsWithSegments("/api-docs", StringComparison.OrdinalIgnoreCase) ||context.Request.Path.StartsWithSegments("/api/auth", StringComparison.OrdinalIgnoreCase))
            {
                await _next(context);

                return;
            }

            if (!Auth(context))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;

                await context.Response.WriteAsync("Token недействительный.");

                return;
            }

            await _next(context);
        }

        private bool Auth(HttpContext context)
        {
            var jwtService = context.RequestServices.GetRequiredService<IJWTService>();

            var authHead = context.Request.Headers["Authorization"].FirstOrDefault();

            if (string.IsNullOrWhiteSpace(authHead) || !authHead.StartsWith("Bearer ", StringComparison.InvariantCultureIgnoreCase))
            {
                return false;
            }

            var token = authHead["Bearer ".Length..].Trim();

            var principal = jwtService.VerifyToken(token);

            if (principal != null)
            {
                context.User = principal;

                return true;
            }

            return false;
        }
    }
}