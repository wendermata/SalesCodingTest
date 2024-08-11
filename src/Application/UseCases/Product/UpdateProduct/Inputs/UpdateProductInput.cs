using Application.Common;
using MediatR;

namespace Application.UseCases.Product.UpdateProduct.Inputs
{
    public class UpdateProductInput : IRequest<Output>
    {
        public Guid Id { get; set; }
        public string NewName { get; set; }
        public decimal NewPrice { get; set; }
        public int NewStockQuantity { get; set; }
    }
}
