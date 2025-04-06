using System.Text.RegularExpressions;
using ProductRating.Contracts.DTO;
using ProductRating.Data.Configurations;
using ProductRating.Data.WebAPI.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;

namespace ProductRating.WebAPI.Filters
{
    public class AuthorizationFilter : IAsyncActionFilter
    {
        private readonly AuthorizationFilterOptions _options;
        private readonly IErrorDTOService _errorDTOService;

        public AuthorizationFilter(IOptions<AuthorizationFilterOptions> options, IErrorDTOService errorDTOService)
        {
            _options = options.Value;
            _errorDTOService = errorDTOService;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ActionArguments.TryGetValue("request", out var argument) || argument is not AuthorizationRequest request)
            {
                context.Result = new BadRequestObjectResult("Неверный формат данных.");

                return;
            }

            List<string> errors = Validate(request);

            if (errors.Count > 0)
            {
                context.Result = new BadRequestObjectResult(_errorDTOService.CreateErrorsResult(errors));

                return;
            }

            await next();
        }

        private List<string> Validate(AuthorizationRequest request)
        {
            List<string> errors = new List<string>();

            if (string.IsNullOrWhiteSpace(request.Phone))
            {
                errors.Add("Phone пустой или null.");
            }
            else if (request.Phone.Length < _options.PhoneMinLength || request.Phone.Length > _options.PhoneMaxLength)
            {
                errors.Add("Недопустимая длина Phone.");
            }
            else if (!Regex.IsMatch(request.Phone, _options.PhoneRegularExpression))
            {
                errors.Add("Недопустимые символы в Phone");
            }

            if (string.IsNullOrWhiteSpace(request.Password))
            {
                errors.Add("Password пустой или null.");
            }
            else if (request.Password.Length < _options.PasswordMinLength || request.Password.Length > _options.PasswordMaxLength)
            {
                errors.Add("Недопустимая длина Password.");
            }

            return errors;
        }
    }
}