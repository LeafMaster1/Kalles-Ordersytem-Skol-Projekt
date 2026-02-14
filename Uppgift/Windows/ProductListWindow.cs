using Uppgift.Data;
using Uppgift.UI;

namespace Uppgift.Windows
{
    public class ProductListWindow
    {
        public static void Show(Registers registers)
        {
            var productListMenu = new MenuRenderer();
            productListMenu.Add(new Title("Produktlista"));
            var allProducts = registers.Products.All;
            if (allProducts.Length == 0)
            {
                productListMenu.Add(new MenuText("Inga produkter finns."));
            }
            else
            {
                foreach (var product in allProducts)
                {
                    productListMenu.Add(new MenuText(
                        $"Produktnummer: {product.ProductNumber}  " +
                        $"Namn: {product.Name.PadRight(20)}  " +
                        $"Pris: {product.UnitPrice:C}"
                    ));
                }
            }
            productListMenu.Print();
        }
    }
}
