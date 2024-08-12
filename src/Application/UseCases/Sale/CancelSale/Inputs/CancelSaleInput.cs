using Application.Common;
using MediatR;

namespace Application.UseCases.Sale.CancelSale.Inputs
{
    public class CancelSaleInput : IRequest<Output>
    {
        public Guid SaleId { get; set; }
    }
}
