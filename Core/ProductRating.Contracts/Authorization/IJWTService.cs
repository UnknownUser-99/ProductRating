using System.Security.Claims;

namespace ProductRating.Contracts.Authorization
{
    public interface IJWTService
    {
        string GenerateToken(int id, TimeSpan time);
        ClaimsPrincipal VerifyToken(string token);
    }
}