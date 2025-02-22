using ProductRating.Data.Entities.Database;

namespace ProductRating.Contracts.Database
{
    public interface IProductService
    {
        Task<Product[]> GetProducts();
        Task<Product> GetProductById(int id);
        Task<Product[]> GetProductsByName(string name);
    }
}