using Application.UseCases.Sale.ListSales.Inputs;
using Application.UseCases.Sale.ListSales.Outputs;
using MediatR;

namespace Application.UseCases.Sale.ListSales
{
    public interface IListSalesUseCase : IRequestHandler<ListSalesInput, ListSalesOutput>
    {
    }
}
