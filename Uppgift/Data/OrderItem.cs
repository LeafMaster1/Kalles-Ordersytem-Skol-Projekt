namespace Uppgift.Data
{
    public class OrderItem
    {
        public required Product Product { get; set; }
        public int Quantity { get; set; } = 1;
        public decimal TotalPrice => Product.UnitPrice * Quantity;
    }
}
