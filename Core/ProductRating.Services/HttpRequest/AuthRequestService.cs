using System.Net.Http.Json;
using ProductRating.Contracts.HttpRequest;
using ProductRating.Data.WebAPI.Requests;
using ProductRating.Data.WebAPI.Results;

namespace ProductRating.Services.HttpRequest
{
    public class AuthRequestService : IAuthRequestService
    {
        private readonly HttpClient _httpClient;

        public AuthRequestService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> RegisterAsync(string phone, string name, string password)
        {
            RegistrationRequest request = new RegistrationRequest
            {
                Phone = phone,
                Name = name,
                Password = password
            };

            var response = await _httpClient.PostAsJsonAsync("Registration", request);

            return response.IsSuccessStatusCode;
        }

        public async Task<AuthorizationResult> AuthorizeAsync(string phone, string password)
        {
            AuthorizationRequest request = new AuthorizationRequest
            {
                Phone = phone,
                Password = password
            };

            var response = await _httpClient.PostAsJsonAsync("Authorization", request);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var result = await response.Content.ReadFromJsonConfiguredAsync<AuthorizationResult>();

            return result;
        }

        public async Task<VerificationResult> VerifyAsync(string token)
        {
            VerificationRequest request = new VerificationRequest
            {
                Token = token
            };

            var response = await _httpClient.PostAsJsonAsync("Verification", request);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var result = await response.Content.ReadFromJsonConfiguredAsync<VerificationResult>();

            return result;
        }
    }
}