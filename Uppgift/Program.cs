using System.Text;
using Uppgift.Data;
using Uppgift.Windows;

namespace Uppgift
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;


            // Ladda registerdata
            var dataFolder = "data";
            if (!Directory.Exists(dataFolder))
            {
                Directory.CreateDirectory(dataFolder);
            }
            var registers = Registers.Load(dataFolder);


            // Visa grafiskt gränssnitt
            MainWindow.Show(registers);
        }
    }
}
