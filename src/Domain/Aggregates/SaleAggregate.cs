using Domain.Entities;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Domain.Aggregates
{
    public class SaleAggregate
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]

        public Guid Id { get; private set; }
        public string ZipCode { get; private set; }
        public decimal ShipmentValue { get; private set; }
        public decimal TotalValue { get; private set; }
        public bool IsActive { get; private set; }
        public DateTime CreatedAt { get; private set; }


        private List<Item> _items = new List<Item>();
        public IReadOnlyList<Item> Items => _items.AsReadOnly();

        public SaleAggregate(Guid id, string zipCode, decimal shipmentValue, decimal? totalValue = null, bool isActive = true, DateTime? createdAt = null,List<Item>? items = null)
        {
            Id = id;
            ZipCode = zipCode;
            ShipmentValue = shipmentValue;
            IsActive = isActive;
            TotalValue = totalValue ?? 0;
            CreatedAt = createdAt ?? DateTime.Now;
            _items = items ?? new List<Item>();
        }

        public void AddItem(Item item) => _items.Add(item);
        public void AddItems(IEnumerable<Item> items) => _items.AddRange(items);

        public void CalculateTotalValue()
        {
            TotalValue = ShipmentValue;
            foreach (var item in _items)
                TotalValue += item.TotalPrice;
        }

    }
}
