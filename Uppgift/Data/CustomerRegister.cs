using System.Text.Json;

namespace Uppgift.Data
{
    public class CustomerRegister : Register<Customer>
    {

        private static readonly JsonSerializerOptions Json = new() { WriteIndented = true };
        public CustomerRegister(string path) : base(path) {}

        // TODO: Returnera null om kunden inte fanns
        
        protected override int GetKey(Customer item) => item.CustomerNumber;
        protected override string Serialize() =>
        JsonSerializer.Serialize(items.Values, Json);

        protected override void Deserialize(string data)
        {
            items.Clear();
            if (string.IsNullOrWhiteSpace(data)) return;
            var jsonDeser = JsonSerializer.Deserialize<List<Customer>>(data) ?? [];
            foreach (var i in jsonDeser)
            {
                items[GetKey(i)] = i;
            }
        }

        
    }
}
