using System.Text.Json;

namespace Uppgift.Data
{
    public class ProductRegister : Register<Product>
    {
        // TODO: Ta bort Count, All, products, GetByNumber, Add eftersom de ska in i basklassen
        public ProductRegister(string path) : base(path){}

        private static readonly JsonSerializerOptions Json = new () { WriteIndented = true }; 
        // TODO: Returnera null om produkten inte fanns
        protected override int GetKey(Product item) => item.ProductNumber;
        

        protected override void Deserialize(string data)
        {
            items.Clear();
            if (string.IsNullOrWhiteSpace(data))
            {
                return;
            }

            var jsonDeser = JsonSerializer.Deserialize<List<Product>>(data) ?? [];
            foreach (var i in jsonDeser)
            {
                items[GetKey(i)] = i;
            }
        }

        protected override string Serialize() =>
            JsonSerializer.Serialize(items.Values, Json);
    }
}
