using Application.Common;
using MediatR;

namespace Application.UseCases.Sale.CreateSale.Inputs
{
    public class CreateSaleInput : IRequest<Output>
    {
        public string ZipCode { get; private set; }
        public List<ItemOutput> Items = new();

    }

    public class ItemOutput
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
