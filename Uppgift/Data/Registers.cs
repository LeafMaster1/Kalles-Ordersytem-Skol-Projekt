namespace Uppgift.Data
{
    public class Registers
    {
        public string Path { get; private set; }

        public CustomerRegister Customers { get; private set; }
        public OrderRegister Orders { get; private set; }
        public ProductRegister Products { get; private set; }

        public const string customersFileName = "customers.register";
        public const string ordersFileName = "orders.register";
        public const string productsFileName = "products.register";

        public Registers(string directory, CustomerRegister customers, OrderRegister orders, ProductRegister products)
        {
            Path = directory;
            Customers = customers;
            Orders = orders;
            Products = products;
        }

        public static Registers Load(string directory)
        {
            var customers = new CustomerRegister(System.IO.Path.Combine(directory, customersFileName));
            customers.Load();

            var orders = new OrderRegister(System.IO.Path.Combine(directory, ordersFileName));
            orders.Load();

            var product = new ProductRegister(System.IO.Path.Combine(directory, productsFileName));
            product.Load();

            return new Registers(directory, customers, orders, product);
        }

        public void Save()
        {
            Customers.Save();
            Orders.Save();
            Products.Save();
        }
    }
}
