using Uppgift.Data;
using Uppgift.UI;

namespace Uppgift.Windows
{
    public class CustomerRegisterWindow
    {
        public static void Show(Registers registers)
        {
            var customerRegisterMenu = new MenuRenderer();
            customerRegisterMenu.Add(new Title("Kundregister"));
            customerRegisterMenu.Add(new MenuButton("Skapa", null, () => CreateCustomerWindow.Show(registers)));
            customerRegisterMenu.Add(new MenuButton("Visa", null, () => CustomerListWindow.Show(registers))
            {
                Status = () => registers.Customers.Count == 0 ? MenuEntryStatus.Disabled : MenuEntryStatus.Normal,
            });
            customerRegisterMenu.Add(new MenuButton("Sök", null, () => CustomerSearchWindow.Show(registers))
            {
                Status = () => registers.Customers.Count == 0 ? MenuEntryStatus.Disabled : MenuEntryStatus.Normal,
            });
            customerRegisterMenu.Print();
        }
    }
}
