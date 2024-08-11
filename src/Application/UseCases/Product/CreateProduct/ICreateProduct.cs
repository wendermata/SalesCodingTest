using Application.Common;
using Application.UseCases.Product.CreateProduct.Inputs;
using MediatR;

namespace Application.UseCases.Product.CreateProduct
{
    public interface ICreateProduct : IRequestHandler<CreateProductInput, Output> { }
}
