using Application.UseCases.Product.ListProducts.Inputs;
using Application.UseCases.Product.ListProducts.Outputs;
using MediatR;

namespace Application.UseCases.Product.ListProducts
{
    public interface IListProducts : IRequestHandler<ListProductsInput, ListProductsOutput> { }
}
