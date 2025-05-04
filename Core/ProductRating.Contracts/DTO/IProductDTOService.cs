using ProductRating.Data.WebAPI.Results;

namespace ProductRating.Contracts.DTO
{
    public interface IProductDTOService
    {
        ProductCardsResult CreateProductCardsResult(ProductCardResult[] productCards);
    }
}