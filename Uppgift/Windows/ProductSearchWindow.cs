using Uppgift.Data;
using Uppgift.UI;

namespace Uppgift.Windows
{
    public class ProductSearchWindow
    {
        public static void Show(Registers registers)
        {
            var searchMenu = new MenuRenderer();
            searchMenu.Add(new Title("Sök Produkt"));
            searchMenu.Add(new TextField("Sökterm") { AllowedToBeEmpty = false });
            
            searchMenu.OnComplete("Sök", "Sök efter namn eller produktnummer", values =>
            {
                var term = values["Sökterm"].ToLower();
                var results = registers.Products.All.Where(p => 
                    p.Name.ToLower().Contains(term) || 
                    p.ProductNumber.ToString().Contains(term)
                ).ToArray();

                ShowResults(results);
            });

            searchMenu.Print();
        }

        private static void ShowResults(Product[] products)
        {
            var resultMenu = new MenuRenderer();
            resultMenu.Add(new Title("Sökresultat"));

            if (products.Length == 0)
            {
                resultMenu.Add(new MenuText("Inga produkter matchade din sökning."));
            }
            else
            {
                foreach (var product in products)
                {
                    resultMenu.Add(new MenuText($"{product.ProductNumber} - {product.Name} ({product.UnitPrice:C})"));
                }
            }

            resultMenu.Print();
        }
    }
}
