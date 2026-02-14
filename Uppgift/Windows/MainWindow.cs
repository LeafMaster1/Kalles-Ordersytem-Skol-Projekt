using Uppgift.Data;
using Uppgift.UI;

namespace Uppgift.Windows
{
    public class MainWindow
    {
        public static void Show(Registers registers)
        {
            var mainMenu = new MenuRenderer();
            mainMenu.Add(new Title("Kalle Ankas Ordersystem"));
            mainMenu.Add(new MenuButton(
                "Skapa ny order",
                "Ange alla uppgifter för kunden och beställningen",
                () => CreateOrderWindow.Show(registers)
            ));
            mainMenu.Add(new MenuButton(
                "Produktregister",
                "Visa, sök, lägg till, ändra eller ta bort",
                () => ProductRegisterWindow.Show(registers)
            ));
            mainMenu.Add(new MenuButton(
                "Orderregister",
                "Visa, sök, ändra status eller makulera ordrar",
                () => OrderRegisterWindow.Show(registers)
            ));
            mainMenu.Add(new MenuButton(
                "Kundregister",
                "Visa, sök, skapa, redigera eller ta bort kunduppgifter",
                () => CustomerRegisterWindow.Show(registers)
            ));
            mainMenu.Print();
        }
    }
}
