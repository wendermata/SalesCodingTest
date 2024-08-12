using Application.Common;

namespace Application.UseCases.Sale.ListSales.Outputs
{
    public class ListSalesOutput : PaginatedListOutput<SaleOutput>
    {
        public ListSalesOutput(int page,
            int pageSize,
            int total,
            IReadOnlyList<SaleOutput> items) : base(page, pageSize, total, items) { }

        public ListSalesOutput() { }
    }
}
