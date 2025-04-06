using ProductRating.Data.Database;
using ProductRating.Data.WebAPI.Results;

namespace ProductRating.Contracts.Database
{
    public interface IProductService
    {
        Task<Product[]> GetProducts();
        Task<Product> GetProductById(int id);
        Task<Product[]> GetProductsByName(string name);
        Task<ProductWithFullInfoResult> GetProductWithRating(int id);
    }
}