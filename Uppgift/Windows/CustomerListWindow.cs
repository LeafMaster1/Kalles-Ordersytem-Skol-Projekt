using Uppgift.Data;
using Uppgift.UI;

namespace Uppgift.Windows
{
    public class CustomerListWindow
    {
        public static void Show(Registers registers)
        {
            var customerListMenu = new MenuRenderer();
            customerListMenu.Add(new Title("Kundlista"));
            var allCustomers = registers.Customers.All;
            var idLength = allCustomers.Max(x => x.CustomerNumber.ToString().Length);
            foreach (var customer in allCustomers)
            {
                customerListMenu.Add(new MenuText($"{customer.CustomerNumber.ToString().PadLeft(idLength)}  {customer.FullName}"));
            }
            customerListMenu.Print();
        }
    }
}
