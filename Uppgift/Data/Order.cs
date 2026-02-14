namespace Uppgift.Data
{
    public class Order
    {
        public required int OrderNumber { get; set; }
        public required int CustomerNumber { get; set; }
        public List<OrderItem> Items { get; set; } = new List<OrderItem>();
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
    }
}
