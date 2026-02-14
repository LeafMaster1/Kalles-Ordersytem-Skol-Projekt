using System.Security.Cryptography;
using Uppgift.Data;
using Uppgift.UI;

namespace Uppgift.Windows
{
    public class CreateCustomerWindow
    {
        public static void Show(Registers registers)
        {
            var createCustomerMenu = new MenuRenderer();
            createCustomerMenu.Add(new Title("Skapa en ny kund"));
            createCustomerMenu.Add(new TextField("Nummer")
            {
                AllowedToBeEmpty = false,
                Validate = input => ValidateCustomerNumber(registers, input),
            });
            createCustomerMenu.Add(new TextField("Namn")
            {
                AllowedToBeEmpty = false
            });
            createCustomerMenu.Add(new TextField("Epost"));
            createCustomerMenu.Add(new TextField("Adress"));
            createCustomerMenu.OnComplete("Skapa", null, values =>
            {
                var number = int.Parse(values["Nummer"]);
                var name = values["Namn"];
                var email = values["Epost"];
                var address = values["Adress"];
                registers.Customers.Add(new Customer
                {
                    CustomerNumber = number,
                    FullName = name,
                    Address = address,
                    Email = email,
                });
                registers.Save();
            });
            createCustomerMenu.Print();
        }

        
        public static bool ValidateCustomerNumber(Registers registers, string input)
        {
            if (!int.TryParse(input, out var number))
                return false;

            if (number <= 0)
                return false;
         
            return registers.Customers.GetByNumber(number) == null;
        }
    }
}
