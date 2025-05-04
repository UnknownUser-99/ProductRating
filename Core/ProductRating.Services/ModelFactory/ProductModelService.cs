using System.Globalization;
using ProductRating.Contracts.ModelFactory;
using ProductRating.Data.Models;
using ProductRating.Data.WebAPI.Results;

namespace ProductRating.Services.ModelFactory
{
    public class ProductModelService : IProductModelService
    {
        public ProductModel[] CreateProductModels(ProductCardsResult productCards)
        {
            if (productCards == null)
            {
                return Array.Empty<ProductModel>();
            }

            ProductModel[] models = new ProductModel[productCards.ProductCards.Length];

            for (int i = 0; i < productCards.ProductCards.Length; i++)
            {
                models[i] = new ProductModel
                {
                    Name = productCards.ProductCards[i].Name,
                    Image = productCards.ProductCards[i].Image,
                    Rating = decimal.Parse(productCards.ProductCards[i].Rating, CultureInfo.InvariantCulture)
                };
            }

            return models;
        }
    }
}