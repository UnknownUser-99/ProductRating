using System.Security.Claims;
using ProductRating.Data.WebAPI.Results;

namespace ProductRating.Contracts.DTO
{
    public interface IAuthDTOService
    {
        AuthorizationResult CreateAuthorizationResult(string token);
        VerificationResult CreateVerificationResult(ClaimsPrincipal claims);
    }
}