using System.Security.Claims;

namespace ProductRating.Contracts.Authorization
{
    public interface IJWTService
    {
        string GenerateToken(int id, int role);
        ClaimsPrincipal VerifyToken(string token);
    }
}