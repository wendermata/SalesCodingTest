using Application.Common;
using Application.UseCases.Sale.CancelSale.Inputs;
using MediatR;

namespace Application.UseCases.Sale.CancelSale
{
    public interface ICancelSaleUseCase : IRequestHandler<CancelSaleInput, Output>
    {
    }
}
