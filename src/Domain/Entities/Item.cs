using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities
{
    public class Item
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public decimal UnityPrice { get; set; }
        public decimal TotalPrice{ get; set; }
    }
}
