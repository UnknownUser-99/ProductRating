using ProductRating.Data.WebAPI.Results;

namespace ProductRating.Contracts.DTO
{
    public interface IErrorDTOService
    {
        ErrorsResult CreateErrorsResult(List<string> errors);
    }
}