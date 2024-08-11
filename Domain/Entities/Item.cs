namespace Domain.Entities
{
    public class Item
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public decimal UnityPrice { get; set; }
        public decimal TotalPrice{ get; set; }
    }
}
