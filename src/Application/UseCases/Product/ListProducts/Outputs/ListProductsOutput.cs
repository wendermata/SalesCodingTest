using Application.Common;

namespace Application.UseCases.Product.ListProducts.Outputs
{
    public class ListProductsOutput : PaginatedListOutput<ProductOutput>
    {
        public ListProductsOutput(int page,
            int pageSize,
            int total,
            IReadOnlyList<ProductOutput> items) : base(page, pageSize, total, items){ }

        public ListProductsOutput() { }
    }
}
