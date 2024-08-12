using Application.UseCases.Sale.ListSales.Inputs;
using Domain.Repository.Shared.SearchableRepository;

namespace Application.UseCases.Sale.ListSales.Mapping
{
    public static class ListSalesInputMapper
    {
        public static SearchInput MapToSearchInput(this ListSalesInput input)
        {
            if (input is null) 
                return null;

            return new SearchInput(input.Page, input.PageSize, input.Search, input.Sort, input.Dir);
        }
    }
}
