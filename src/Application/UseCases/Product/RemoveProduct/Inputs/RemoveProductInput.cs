using Application.Common;
using MediatR;

namespace Application.UseCases.Product.RemoveProduct.Inputs
{
    public class RemoveProductInput : IRequest<Output>
    {
        public Guid Id { get; set; }
    }
}
