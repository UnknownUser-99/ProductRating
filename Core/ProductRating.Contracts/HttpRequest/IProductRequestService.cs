using ProductRating.Data.WebAPI.Results;

namespace ProductRating.Contracts.HttpRequest
{
    public interface IProductRequestService
    {
        Task<ProductCardsResult> GetProductCardsAsync();
    }
}