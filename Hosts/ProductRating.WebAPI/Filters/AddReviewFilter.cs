using ProductRating.Contracts.DTO;
using ProductRating.Data.Configurations;
using ProductRating.Data.WebAPI.Requests;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc;

namespace ProductRating.WebAPI.Filters
{
    public class AddReviewFilter : IAsyncActionFilter
    {
        private readonly AddReviewFilterOptions _options;
        private readonly IErrorDTOService _errorDTOService;

        public AddReviewFilter(IOptions<AddReviewFilterOptions> options, IErrorDTOService errorDTOService)
        {
            _options = options.Value;
            _errorDTOService = errorDTOService;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ActionArguments.TryGetValue("request", out var argument) || argument is not AddReviewRequest request)
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

        private List<string> Validate(AddReviewRequest request)
        {
            List<string> errors = new List<string>();

            if (request.Product <= 0)
            {
                errors.Add("Product меньше или равен 0.");
            }

            if (request.Rating < _options.RatingMinValue || request.Rating > _options.RatingMaxValue)
            {
                errors.Add($"Rating меньше {_options.RatingMinValue} или больше {_options.RatingMaxValue}.");
            }

            if (request.Description.Length > _options.DescriptionMaxLength)
            {
                errors.Add($"Description больше {_options.DescriptionMaxLength}.");
            }

            return errors;
        }
    }
}