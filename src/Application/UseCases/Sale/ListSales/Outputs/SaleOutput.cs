namespace Application.UseCases.Sale.ListSales.Outputs
{
    public class SaleOutput
    {
        public Guid Id { get; set; }
        public string ZipCode { get; set; }
        public decimal ShipmentValue { get; set; }
        public decimal TotalValue { get; set; }
        public bool IsCancelled { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? CancelledAt { get; set; }
        public List<ItemOutput> Items { get; set; } = [];
    }
}
