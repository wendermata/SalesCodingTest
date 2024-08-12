using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities
{
    public class Item
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnityPrice { get; private set; }
        public decimal TotalPrice{ get; private set; }

        public Item(Guid id, int quantity, decimal unityPrice, decimal totalPrice)
        {
            Id = id;
            Quantity = quantity;
            UnityPrice = unityPrice;
            TotalPrice = totalPrice;
        }
    }
}
