using Uppgift.Data;
using Uppgift.UI;

namespace Uppgift.Windows
{
    public class CustomerSearchWindow
    {
        public static void Show(Registers registers)
        {
            var searchMenu = new MenuRenderer();
            searchMenu.Add(new Title("Sök Kund"));
            searchMenu.Add(new TextField("Sökterm") { AllowedToBeEmpty = false });
            
            searchMenu.OnComplete("Sök", "Sök på namn, e-post eller kundnummer", values =>
            {
                var term = values["Sökterm"].ToLower();
                var results = registers.Customers.All.Where(c => 
                    c.FullName.ToLower().Contains(term) || 
                    c.Email.ToLower().Contains(term) ||
                    c.CustomerNumber.ToString().Contains(term)
                ).ToArray();

                ShowResults(results);
            });

            searchMenu.Print();
        }

        private static void ShowResults(Customer[] customers)
        {
            var resultMenu = new MenuRenderer();
            resultMenu.Add(new Title("Sökresultat - Kunder"));

            if (customers.Length == 0)
            {
                resultMenu.Add(new MenuText("Inga kunder matchade din sökning."));
            }
            else
            {
                foreach (var customer in customers)
                {
                    resultMenu.Add(new MenuText($"{customer.CustomerNumber} - {customer.FullName} ({customer.Email})"));
                }
            }

            resultMenu.Print();
        }
    }
}
