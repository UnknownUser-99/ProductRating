using ProductRating.Contracts.Database;
using ProductRating.Data.Database;
using ProductRating.Data.WebAPI.Results;
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

        public async Task<Product[]> GetProductsAsync()
        {
            return await _context.Products.ToArrayAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        //TODO: Попробовать в бд COLLATE NOCASE
        public async Task<Product[]> GetProductsByNameAsync(string name)
        {
            return await _context.Products.Where(p => EF.Functions.Like(p.Name.ToLower(), $"%{name.ToLower()}%")).ToArrayAsync();
        }

        public async Task<ProductWithFullInfoResult> GetProductWithRatingAsync(int id)
        {
            var result = await (
                from product in _context.Products
                where product.Id == id
                join brand in _context.ProductBrands on product.Brand equals brand.Id
                join type in _context.ProductTypes on product.Type equals type.Id
                join rating in _context.ProductRatings on product.Id equals rating.Product
                select new ProductWithFullInfoResult
                {
                    Id = product.Id,
                    Product = product.Name,
                    Brand = brand.Name,
                    Type = type.Name,
                    Image = product.Image,
                    OverallRating = rating.OverallRating.ToString(),
                    YearlyRating = rating.YearlyRating.ToString(),
                    MonthlyRating = rating.MonthlyRating.ToString()
                })
                .FirstOrDefaultAsync();

            return result;
        }

        public async Task<ProductCardResult[]> GetProductCardsAsync()
        {
            var result = await (
                from product in _context.Products
                join rating in _context.ProductRatings on product.Id equals rating.Product
                select new ProductCardResult
                {
                    Name = product.Name,
                    Image = product.Image,
                    Rating = rating.OverallRating.ToString()
                })
                .ToArrayAsync();

            return result;
        }
    }
}