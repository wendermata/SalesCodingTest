using Application.Common;
using Application.UseCases.Sale.CreateSale.Inputs;
using MediatR;

namespace Application.UseCases.Sale.CreateSale
{
    public interface ICreateSaleUseCase : IRequestHandler<CreateSaleInput, Output> { }
}
