using Application.Common;
using Application.UseCases.Product.RemoveProduct.Inputs;
using MediatR;

namespace Application.UseCases.Product.RemoveProduct
{
    public interface IRemoveProductUseCase : IRequestHandler<RemoveProductInput, Output> { }
}
