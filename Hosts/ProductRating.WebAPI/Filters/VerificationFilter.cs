using ProductRating.Contracts.DTO;
using ProductRating.Data.WebAPI.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ProductRating.WebAPI.Filters
{
    public class VerificationFilter : IAsyncActionFilter
    {
        private readonly IErrorDTOService _errorDTOService;

        public VerificationFilter(IErrorDTOService errorDTOService)
        {
            _errorDTOService = errorDTOService;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ActionArguments.TryGetValue("request", out var argument) || argument is not VerificationRequest request)
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

        private List<string> Validate(VerificationRequest request)
        {
            List<string> errors = new List<string>();

            if (string.IsNullOrWhiteSpace(request.Token))
            {
                errors.Add("Token пустой или null.");
            }

            return errors;
        }
    }
}