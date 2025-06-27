using System.Net.Http.Headers;
using ProductRating.Data.Configurations;
using Microsoft.Extensions.Options;

namespace ProductRating.Services.HttpRequest
{
    public class AuthTokenSchedulerHandler : DelegatingHandler
    {
        private readonly AuthTokenSchedulerHandlerOptions _options;

        public AuthTokenSchedulerHandler(IOptions<AuthTokenSchedulerHandlerOptions> options)
        {
            _options = options.Value;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _options.AuthToken);

            return await base.SendAsync(request, cancellationToken);
        }
    }
}