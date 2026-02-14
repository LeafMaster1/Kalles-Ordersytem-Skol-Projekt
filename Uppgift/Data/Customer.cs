namespace Uppgift.Data
{
    public class Customer
    {
        public required int CustomerNumber { get; set; }
        public required string FullName { get; set; }
        public string Email { get; set; } = "";
        public string Address { get; set; } = "";
    }
}
