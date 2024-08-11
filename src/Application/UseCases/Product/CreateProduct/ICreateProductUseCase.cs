using Application.Common;
using Application.UseCases.Product.CreateProduct.Inputs;
using MediatR;

namespace Application.UseCases.Product.CreateProduct
{
    public interface ICreateProductUseCase : IRequestHandler<CreateProductInput, Output> { }
}
