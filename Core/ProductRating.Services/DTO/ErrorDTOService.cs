using ProductRating.Contracts.DTO;
using ProductRating.Data.WebAPI.Results;

namespace ProductRating.Services.DTO
{
    public class ErrorDTOService : IErrorDTOService
    {
        public ErrorsResult CreateErrorsResult(List<string> errors)
        {
            return new ErrorsResult
            {
                Errors = errors
            };
        }
    }
}