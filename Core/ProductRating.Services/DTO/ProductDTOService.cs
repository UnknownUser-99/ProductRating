using ProductRating.Contracts.DTO;
using ProductRating.Data.WebAPI.Results;

namespace ProductRating.Services.DTO
{
    public class ProductDTOService : IProductDTOService
    {
        public ProductCardsResult CreateProductCardsResult(ProductCardResult[] productCards)
        {
            return new ProductCardsResult
            {
                ProductCards = productCards
            };
        }
    }
}