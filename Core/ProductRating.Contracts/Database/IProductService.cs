using ProductRating.Data.Entities.Database;
using ProductRating.Data.Entities.WebAPI.Results;

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