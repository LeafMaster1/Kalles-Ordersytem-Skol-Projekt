using System.Text.Json;

namespace Uppgift.Data
{
    public class OrderRegister : Register<Order>
    {
        private static readonly JsonSerializerOptions Json = new() { WriteIndented = true };
        public OrderRegister(string path) : base(path){}

        // TODO: Returnera null om ordern inte fanns
        protected override int GetKey(Order item) => item.OrderNumber;
        protected override string Serialize()=>
        JsonSerializer.Serialize(items.Values, Json);
        protected override void Deserialize(string data)
        {
            items.Clear();
            if (string.IsNullOrWhiteSpace(data)) return;
            var jsonDeser = JsonSerializer.Deserialize<List<Order>>(data) ?? [];
            foreach (var i in jsonDeser)
            {
                items[GetKey(i)] = i;
            }

        }
    }
}
