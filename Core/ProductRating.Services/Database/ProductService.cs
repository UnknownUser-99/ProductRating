using ProductRating.Contracts.Database;
using ProductRating.Data.Entities.Database;
using ProductRating.Data.Entities.WebAPI.Results;
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
            if (id < 1)
            {
                throw new ArgumentException("Id меньше 1.", nameof(id));
            }

            return await _context.Products.FindAsync(id);
        }

        //TODO: Попробовать в бд COLLATE NOCASE
        public async Task<Product[]> GetProductsByName(string name)
        {
            return await _context.Products.Where(p => EF.Functions.Like(p.Name.ToLower(), $"%{name.ToLower()}%")).ToArrayAsync();
        }

        public async Task<ProductWithFullInfoResult> GetProductWithRating(int id)
        {
            if (id < 1)
            {
                throw new ArgumentException("Id меньше 1.", nameof(id));
            }

            var result = await (
                from product in _context.Products
                where product.Id == id
                join brand in _context.ProductBrands on product.Brand equals brand.Id
                join type in _context.ProductTypes on product.Type equals type.Id
                join rating in _context.ProductRatings on product.Id equals rating.Product
                select new ProductWithFullInfoResult
                {
                    Product = product.Name,
                    Brand = brand.Name,
                    Type = type.Name,
                    Image = product.Image,
                    OverallRating = rating.OverallRating,
                    YearlyRating = rating.YearlyRating,
                    MonthlyRating = rating.MonthlyRating
                }).FirstOrDefaultAsync();

            return result;
        }
    }
}