using Uppgift.Data;
using Uppgift.UI;

namespace Uppgift.Windows
{
    public class OrderSearchWindow
    {
        public static void Show(Registers registers)
        {
            var searchMenu = new MenuRenderer();
            searchMenu.Add(new Title("Sök Order"));
            searchMenu.Add(new TextField("Sökterm") { AllowedToBeEmpty = false });
            
            searchMenu.OnComplete("Sök", "Sök på ordernummer eller kundnummer", values =>
            {
                var term = values["Sökterm"].ToLower();
                var results = registers.Orders.All.Where(o => 
                    o.OrderNumber.ToString().Contains(term) || 
                    o.CustomerNumber.ToString().Contains(term)
                ).ToArray();

                ShowResults(registers, results);
            });

            searchMenu.Print();
        }

        private static void ShowResults(Registers registers, Order[] orders)
        {
            var resultMenu = new MenuRenderer();
            resultMenu.Add(new Title("Sökresultat - Ordrar"));

            if (orders.Length == 0)
            {
                resultMenu.Add(new MenuText("Inga ordrar matchade din sökning."));
            }
            else
            {
                foreach (var order in orders)
                {
                    var customer = registers.Customers.GetByNumber(order.CustomerNumber);
                    resultMenu.Add(new MenuText($"Order: {order.OrderNumber} - Kund: {customer?.FullName ?? order.CustomerNumber.ToString()} ({order.Status})"));
                }
            }

            resultMenu.Print();
        }
    }
}
