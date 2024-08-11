namespace Application.UseCases.Product.ListProducts.Outputs
{
    public class ProductOutput
    {
        public Guid Id { get;  set; }
        public string Name { get;  set; }
        public decimal Price { get;  set; }
        public int StockQuantity { get;  set; }

    }
}
