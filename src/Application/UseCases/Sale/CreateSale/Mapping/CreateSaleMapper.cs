using Application.Boundaries.Services.ViaCEP.Client.Response;
using Application.UseCases.Sale.CreateSale.Inputs;
using Domain.Aggregates;
using Domain.Entities;
using DomainProduct = Domain.Entities.Product;

namespace Application.UseCases.Sale.CreateSale.Mapping
{
    public static class CreateSaleMapper
    {

        public static SaleAggregate MapToDomain(this CreateSaleInput input, List<DomainProduct> products, ViaCEPResponse response)
        {
            if (products.Count == 0 || response is null)
                return null;

            var items = new List<Item>();

            foreach (var product in products)
                items.Add(product.MapToDomain(input.Items
                    .FirstOrDefault(x => x.ProductId == product.Id)!));

            var sale = new SaleAggregate(Guid.NewGuid(),
                input.ZipCode,
                GetShipmentValue(response));

            sale.AddItems(items);
            sale.CalculateTotalValue();
            return sale;
        }

        public static Item MapToDomain(this DomainProduct product, CreateItemOutput item)
        {
            if (product is null || item is null) return null;

            return new Item(product.Id, item.Quantity, product.Price, item.Quantity * product.Price);
        }

        public static decimal GetShipmentValue(this ViaCEPResponse response)
        {
            if (response is null) return 0;

            if (response.Localidade.ToUpper().Trim() == "RIO DE JANEIRO")
                return 10;

            if (response.Uf.ToUpper().Trim() == "RJ")
                return 20;

            return 40;
        }
    }
}
