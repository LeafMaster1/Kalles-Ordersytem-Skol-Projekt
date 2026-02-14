# Kalles Ordersystem

## **Program beskrivning**
  
- **Lägga till en produkt:** Du kan lägga till nya produkter i systemet, till exempel genom att ange ett namn, pris och id.
- **Visa lista med produkter:** Programmet kan visa alla produkter som finns sparade, så att du kan se och bläddra i dem.
- **Söka efter produkter:** Du kan söka efter en produkt med ett id eller namn. Om produkten inte finns, ska svaret vara `null`.
- **Spara data:** Produkterna sparas ner till en fil (serialisering), så att de inte försvinner när programmet stängs.
- **Ladda data:** När programmet startas igen, läses produkterna tillbaka från filen (deserialisering).






---
## Viktiga filer och klasser
- `Uppgift/Data/Register.cs`  
  - Abstrakt basregister för typ `T`. Hanterar intern lagring i `items : Dictionary<int, T>`.
  - Exponerar: `Path`, `Count`, `All`, `GetByNumber(int)`, `Add(T)`, `Save()`, `Load()`.
  - Kräver att avledda klasser implementerar `GetKey(T)`, `Serialize()` och `Deserialize(string)`.

- `Uppgift/Data/ProductRegister.cs`  
  - Specialisering av `Register<Product>`.
  - Implementerar `GetKey(Product)` (returnerar `ProductNumber`).
  - Implementerar `Serialize()` för att skriva ut `items.Values` som JSON och `Deserialize(string)` för att läsa in produkter från JSON.

- `Uppgift/Data/Product.cs`  
  - Modell för produkt med properties: `ProductNumber`, `Name`, `UnitPrice`.

- `Uppgift/Program.cs`  
  - Programstart som använder registren för att lägga till, hämta, spara och läsa tillbaka data.
---


## Programmet använder sig av olika metoder exempel på några.

- JsonDeserialaizer
- JsonSearialaizer
- Dictionary
- Lamda
- List
- Array
- Linq


## GitHub
https://github.com/EduEdugrade/net25-kurs-1-ordersystem-LeafMaster1

