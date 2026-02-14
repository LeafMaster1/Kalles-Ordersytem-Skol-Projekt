using System.Linq;
using Uppgift.Data;
using Uppgift.UI;

namespace Uppgift.Windows
{
    public class CreateOrderWindow
    {
        public static void Show(Registers registers)
        {
            var createOrderMenu = new MenuRenderer();
            createOrderMenu.Add(new Title("Skapa en ny order"));
            createOrderMenu.Add(new TextField("Ordernummer")
            {
                AllowedToBeEmpty = false,
                Validate = input => ValidateOrderNumber(registers, input),
            });
            createOrderMenu.Add(new TextField("Kund (Namn)")
            {
                AllowedToBeEmpty = false,
                Validate = input => ValidateCustomer(registers, input),
            });
            createOrderMenu.Add(new TextField("Beställning") { AllowedToBeEmpty = false });
            createOrderMenu.OnComplete("Skapa", "Spara den nya ordern", values =>
            {
                var customer = GetCustomerByInput(registers, values["Kund (Namn)"]);
                var order = new Order
                {
                    OrderNumber = int.Parse(values["Ordernummer"]),
                    CustomerNumber = customer!.CustomerNumber,
                    Items = new List<OrderItem>(),
                    OrderDate = DateTime.Now,
                    Status = OrderStatus.Pending
                };

                // Enkel hantering av beställningstexten
                var itemText = values["Beställning"];
                var product = registers.Products.All.FirstOrDefault(p => p.Name.Equals(itemText, StringComparison.OrdinalIgnoreCase));
                
                if (product != null)
                {
                    order.Items.Add(new OrderItem { Product = product, Quantity = 1 });
                }
                else
                {
                    // Om produkten inte finns, skapa en tillfällig produkt för att visa texten
                    order.Items.Add(new OrderItem { 
                        Product = new Product { ProductNumber = 0, Name = itemText, UnitPrice = 0 }, 
                        Quantity = 1 
                    });
                }

                if (registers.Orders.Add(order))
                {
                    registers.Save();
                }
            });
            createOrderMenu.Print();
        }


        public static bool ValidateOrderNumber(Registers registers, string input)
        {
            input = input.Trim();
            if (!int.TryParse(input, out var number))
                return false;

            if (number < 0)
                return false;

            return registers.Orders.GetByNumber(number) == null;
        }


        public static bool ValidateCustomer(Registers registers, string input)
        {
            return GetCustomerByInput(registers, input) != null;
        }

        private static Customer? GetCustomerByInput(Registers registers, string input)
        {
            input = input.Trim();
            var allCustomers = registers.Customers.All;
            
            // Försök först hitta via exakt namn (case-insensitive)
            var customer = allCustomers.FirstOrDefault(c => c.FullName.Equals(input, StringComparison.OrdinalIgnoreCase));
            
            // Om inte hittad, försök se om input är ett ID
            if (customer == null && int.TryParse(input, out var id))
            {
                customer = registers.Customers.GetByNumber(id);
            }

            return customer;
        }
    }
}
