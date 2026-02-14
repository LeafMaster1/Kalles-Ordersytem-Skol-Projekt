using Uppgift.Data;
using Uppgift.UI;

namespace Uppgift.Windows
{
    public class ProductRegisterWindow
    {
        public static void Show(Registers registers)
        {
            var productRegisterMenu = new MenuRenderer();
            productRegisterMenu.Add(new Title("Produktregister"));
            productRegisterMenu.Add(new MenuButton("Visa alla produkter", null, () => ProductListWindow.Show(registers))
            {
                Status = () => registers.Products.Count == 0 ? MenuEntryStatus.Disabled : MenuEntryStatus.Normal,
            });
            productRegisterMenu.Add(new MenuButton("Sök produkt", null, () => ProductSearchWindow.Show(registers))
            {
                Status = () => registers.Products.Count == 0 ? MenuEntryStatus.Disabled : MenuEntryStatus.Normal,
            });
            productRegisterMenu.Print();
        }
    }
}
