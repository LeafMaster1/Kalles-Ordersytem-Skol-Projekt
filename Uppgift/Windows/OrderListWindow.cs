using Uppgift.Data;
using Uppgift.UI;

namespace Uppgift.Windows
{
    public class OrderListWindow
    {
        public static void Show(Registers registers)
        {
            var orderListMenu = new MenuRenderer();
            orderListMenu.Add(new Title("Orderlista"));
            var allOrders = registers.Orders.All;
            if (allOrders.Length == 0)
            {
                orderListMenu.Add(new MenuText("Inga ordrar finns."));
            }
            else
            {
                foreach (var order in allOrders)
                {
                    var customer = registers.Customers.GetByNumber(order.CustomerNumber);
                    var customerName = customer?.FullName ?? "Okänd kund";
                    
                    orderListMenu.Add(new MenuText(
                        $"Ordernummer: {order.OrderNumber}  " +
                        $"Kundnummer: {order.CustomerNumber} ({customerName})  " +
                        $"Status: {order.Status}"
                    ));

                    if (order.Items != null && order.Items.Any())
                    {
                        foreach (var item in order.Items)
                        {
                            orderListMenu.Add(new MenuText($"    - {item.Product.Name} x{item.Quantity}"));
                        }
                    }
                    orderListMenu.Add(new MenuText("")); // Tom rad för avstånd
                }
            }
            orderListMenu.Print();
        }
    }
}
