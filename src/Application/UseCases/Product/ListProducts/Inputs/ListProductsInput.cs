using Application.Common;
using Application.UseCases.Product.ListProducts.Outputs;
using Domain.Repository.Shared.SearchableRepository;
using MediatR;

namespace Application.UseCases.Product.ListProducts.Inputs
{
    public class ListProductsInput : PaginatedListInput, IRequest<ListProductsOutput>
    {
        public ListProductsInput(
            int page = 1,
            int pageSize = 15,
            string search = "",
            string sort = "",
            SearchOrder dir = SearchOrder.Asc) : base(page, pageSize, search, sort, dir)
        { }

        public ListProductsInput() : base(1, 15, "", "", SearchOrder.Asc)
        { }
    }
}
