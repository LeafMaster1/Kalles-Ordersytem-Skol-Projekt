using System.Text.Json;
using System.Text.Json.Nodes;
using System.Linq;

namespace Uppgift.Data
{
    public abstract class Register<T> where T : class
    {
        public string Path { get; private set; }

        // -----
        protected readonly Dictionary<int, T> items = new();

        public Register(string path) => Path = path;
        
        public int Count => items.Count;
        //t All poster som array
        public T[] All => items.Values.ToArray();
        
        public T? GetByNumber ( int number) => items.TryGetValue(number, out var value) ? value : null;

        public bool Add(T item)
        {
            var key = GetKey(item);
            if (items.ContainsKey(key))
            {
                return false;
            } 
                items[key] = item;
                return true;
        }

        protected abstract int GetKey(T item);

        protected abstract string Serialize();
        
        protected abstract void Deserialize(string data);

        public void Save()
        {
            var directory = System.IO.Path.GetDirectoryName(Path);
            if (!string.IsNullOrEmpty(directory))
                System.IO.Directory.CreateDirectory(directory);
            
            var data = Serialize();
            System.IO.File.WriteAllText(Path, data);
        }

        public void Load()
        {
            if (!System.IO.File.Exists(Path))
            {
                items.Clear();
                return;
            }
                var data = System.IO.File.ReadAllText(Path);
                Deserialize(data);
        }
    }
}
