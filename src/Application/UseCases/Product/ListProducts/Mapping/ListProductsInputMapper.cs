using Application.UseCases.Product.ListProducts.Inputs;
using Domain.Repository.Shared.SearchableRepository;

namespace Application.UseCases.Product.ListProducts.Mapping
{
    public static class ListProductsInputMapper
    {
        public static SearchInput MapToSearchInput(this ListProductsInput input)
        {
            if (input == null)
                return null;

            return new SearchInput(input.Page,
                input.PageSize,
                input.Search,
                input.Sort,
                input.Dir);
        }
    }
}
