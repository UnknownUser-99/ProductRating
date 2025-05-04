using System.Security.Claims;
using ProductRating.Contracts.DTO;
using ProductRating.Data.WebAPI.Results;

namespace ProductRating.Services.DTO
{
    public class AuthDTOService : IAuthDTOService
    {
        public AuthorizationResult CreateAuthorizationResult(string token)
        {
            return new AuthorizationResult
            {
                Token = token
            };
        }

        public VerificationResult CreateVerificationResult(ClaimsPrincipal claims)
        {
            string id = claims.FindFirst(ClaimTypes.NameIdentifier).Value;
            string role = claims.FindFirst(ClaimTypes.Role).Value;

            if (string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(role))
            {
                throw new ArgumentException("ClaimsPrincipal пустые или null.", nameof(claims));
            }

            return new VerificationResult
            {
                Id = id,
                Role = role
            };
        }
    }
}