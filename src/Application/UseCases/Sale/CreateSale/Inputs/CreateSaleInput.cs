using Application.Common;
using MediatR;

namespace Application.UseCases.Sale.CreateSale.Inputs
{
    public class CreateSaleInput : IRequest<Output>
    {
        public string ZipCode { get; set; }
        public List<CreateItemOutput> Items { get; set; }
    }
}
