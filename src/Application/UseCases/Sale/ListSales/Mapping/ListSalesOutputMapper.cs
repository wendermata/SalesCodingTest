using Application.UseCases.Sale.ListSales.Outputs;
using Domain.Aggregates;
using Domain.Entities;
using Domain.Repository.Shared.SearchableRepository;
using DomainProduct = Domain.Entities.Product;

namespace Application.UseCases.Sale.ListSales.Mapping
{
    public static class ListSalesOutputMapper
    {
        public static ListSalesOutput MapToOutput(this SearchOutput<SaleAggregate> search)
        {
            if (search is null)
                return null;

            return new ListSalesOutput(search.CurrentPage,
                search.PageSize,
                search.Total,
                search.Items
                    .Select(x => x.MapToSaleOutput())
                    .ToList()
            );
        }

        public static SaleOutput MapToSaleOutput(this SaleAggregate sale)
        {
            if (sale is null)
                return null;

            return new SaleOutput()
            {
                Id = sale.Id,
                ZipCode = sale.ZipCode,
                ShipmentValue = sale.ShipmentValue,
                TotalValue = sale.TotalValue,
                CreatedAt = sale.CreatedAt,
                IsCancelled = sale.IsCancelled,
                CancelledAt = sale.CancelledAt,
                Items = sale.Items.Select(x => x.MapToItemOutput()).ToList()
            };
        }

        public static ItemOutput MapToItemOutput(this Item item)
        {
            if (item is null)
                return null;

            return new ItemOutput()
            {
                Id = item.Id,
                UnityPrice = item.UnityPrice,
                Quantity = item.Quantity,
                TotalPrice = item.TotalPrice
            };
        }

    }
}
