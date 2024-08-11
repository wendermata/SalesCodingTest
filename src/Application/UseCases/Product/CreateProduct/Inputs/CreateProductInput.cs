using Application.Common;
using MediatR;

namespace Application.UseCases.Product.CreateProduct.Inputs
{
    public class CreateProductInput : IRequest<Output>
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
    }
}
