namespace Domain.Entities
{
    public class Product
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public decimal Price { get; private set; }
        public int StockQuantity { get; private set; }
        public bool IsActive { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public Product() { }
        public Product(Guid id, string name, decimal price, int stockQuantity, bool isActive = true, DateTime? createdAt = null )
        {
            Id = id;
            Name = name;
            Price = price;
            StockQuantity = stockQuantity;
            IsActive = isActive;
            CreatedAt = createdAt ?? DateTime.Now;
        }

        public void Update(string name, decimal price, int stockQuantity, bool isActive)
        {
            Name = name;
            Price = price;
            StockQuantity = stockQuantity;
            IsActive = isActive;
        }
    }
}
