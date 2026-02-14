using Uppgift.Data;
using Uppgift.UI;

namespace Uppgift.Windows
{
    public class OrderRegisterWindow
    {
        public static void Show(Registers registers)
        {
            var orderRegisterMenu = new MenuRenderer();
            orderRegisterMenu.Add(new Title("Orderregister"));
            orderRegisterMenu.Add(new MenuButton("Skapa ny order", null, () => CreateOrderWindow.Show(registers)));
            orderRegisterMenu.Add(new MenuButton("Visa alla ordrar", null, () => OrderListWindow.Show(registers))
            {
                Status = () => registers.Orders.Count == 0 ? MenuEntryStatus.Disabled : MenuEntryStatus.Normal,
            });
            orderRegisterMenu.Add(new MenuButton("Sök order", null, () => OrderSearchWindow.Show(registers))
            {
                Status = () => registers.Orders.Count == 0 ? MenuEntryStatus.Disabled : MenuEntryStatus.Normal,
            });
            orderRegisterMenu.Print();
        }
    }
}
