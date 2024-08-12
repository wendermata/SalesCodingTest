using Application.Common;
using Application.UseCases.Sales.CreateSale.Inputs;
using MediatR;

namespace Application.UseCases.Sales.CreateSale
{
    public interface ICreateSaleUseCase : IRequestHandler<CreateSaleInput, Output> { }
}
