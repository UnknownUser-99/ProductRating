using ProductRating.Contracts.Database;
using ProductRating.Data.Entities.Database;
using Microsoft.EntityFrameworkCore;

namespace ProductRating.Services.Database
{
    public class ProductService : IProductService 
    {
        private readonly PRDbContext _context;

        public ProductService(PRDbContext context)
        {
            _context = context;
        }

        public async Task<Product[]> GetProducts()
        {
            return await _context.Products.ToArrayAsync();
        }

        public async Task<Product> GetProductById(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        //TODO: Попробовать в бд COLLATE NOCASE
        public async Task<Product[]> GetProductsByName(string name)
        {
            return await _context.Products.Where(p => EF.Functions.Like(p.Name.ToLower(), $"%{name.ToLower()}%")).ToArrayAsync();
        }

        /*
        public async Task GetProductByIdWithReviews(int id)
        {

        }
        */
    }
}