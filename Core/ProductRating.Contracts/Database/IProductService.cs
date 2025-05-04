using ProductRating.Data.Database;
using ProductRating.Data.WebAPI.Results;

namespace ProductRating.Contracts.Database
{
    public interface IProductService
    {
        Task<Product[]> GetProductsAsync();
        Task<Product> GetProductByIdAsync(int id);
        Task<Product[]> GetProductsByNameAsync(string name);
        Task<ProductWithFullInfoResult> GetProductWithRatingAsync(int id);
        Task<ProductCardResult[]> GetProductCardsAsync();
    }
}