using Application.Common;
using Application.UseCases.Sale.ListSales.Outputs;
using Domain.Repository.Shared.SearchableRepository;
using MediatR;

namespace Application.UseCases.Sale.ListSales.Inputs
{
    public class ListSalesInput : PaginatedListInput, IRequest<ListSalesOutput>
    {
        public ListSalesInput(
            int page = 1,
            int pageSize = 15,
            string search = "",
            string sort = "",
            SearchOrder dir = SearchOrder.Asc) : base(page, pageSize, search, sort, dir)
        { }

        public ListSalesInput() : base(1, 15, "", "", SearchOrder.Asc)
        { }
    }
}
