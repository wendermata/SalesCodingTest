using Application.UseCases.Product.ListProducts.Outputs;
using Domain.Repository.Shared.SearchableRepository;
using DomainProduct = Domain.Entities.Product;


namespace Application.UseCases.Product.ListProducts.Mapping
{
    public static class ListProductsOutputMapper
    {
        public static ListProductsOutput MapToOutput(this SearchOutput<DomainProduct> search)
        {
            if (search is null || search.Items.Count == 0)
                return new ListProductsOutput();

            return new ListProductsOutput(search.CurrentPage,
                search.PageSize,
                search.Total,
                search.Items
                    .Select(x => x.MapToItemOutput())
                    .ToList()
            );
        }

        public static ProductOutput MapToItemOutput(this DomainProduct product)
        {
            if (product is null)
                return null;

            return new ProductOutput()
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                StockQuantity = product.StockQuantity,
            };
        }
    }
}
