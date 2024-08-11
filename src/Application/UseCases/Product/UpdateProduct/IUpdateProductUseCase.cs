using Application.Common;
using Application.UseCases.Product.UpdateProduct.Inputs;
using MediatR;

namespace Application.UseCases.Product.UpdateProduct
{
    public interface IUpdateProductUseCase : IRequestHandler<UpdateProductInput, Output> { }
}
