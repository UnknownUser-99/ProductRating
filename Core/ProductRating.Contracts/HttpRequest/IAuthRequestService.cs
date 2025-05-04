using ProductRating.Data.WebAPI.Results;

namespace ProductRating.Contracts.HttpRequest
{
    public interface IAuthRequestService
    {
        Task<bool> RegisterAsync(string phone, string name, string password);
        Task<AuthorizationResult> AuthorizeAsync(string phone, string password);
        Task<VerificationResult> VerifyAsync(string token);
    }
}