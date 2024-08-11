using Application.UseCases.Product.CreateProduct.Inputs;
using DomainProduct = Domain.Entities.Product;

namespace Application.UseCases.Product.CreateProduct.Mapping
{
    public static class CreateProductInputMapper
    {
        public static DomainProduct MapToDomain(this CreateProductInput input)
        {
            if (input is null) return null;

            return new DomainProduct(
                Guid.NewGuid(),
                input.Name,
                input.Price,
                input.StockQuantity);
        }
    }
}
