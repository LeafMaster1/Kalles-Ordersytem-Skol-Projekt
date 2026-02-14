namespace Uppgift.UI
{
    public class MenuRenderer
    {
        public int Width { get; set; } = 0;

        private List<MenuEntry> menu = new();
        private MenuButton? completeButton = null;
        private bool shouldClose = false;

        public void Add(MenuEntry entry) => menu.Add(entry);

        public void OnComplete(string name, string? description, Action<Dictionary<string, string>> action)
        {
            if (completeButton != null)
            {
                throw new NotSupportedException("OnComplete kan ändast sättas en gång");
            }
            Add(completeButton = new MenuButton(name, description, () =>
            {
                var values = new Dictionary<string, string>();
                foreach (var entry in menu)
                {
                    if (entry is TextField textField)
                    {
                        values.Add(textField.Label, textField.Value);
                    }
                }
                action(values);
                shouldClose = true;
            })
            {
                Status = () =>
                {
                    foreach (var item in menu)
                    {
                        if (item == completeButton) continue;

                        var status = item.Status?.Invoke();
                        if (status == MenuEntryStatus.Invalid)
                        {
                            return MenuEntryStatus.Invalid;
                        }
                    }
                    return MenuEntryStatus.Normal;
                }
            });
        }

        public static int CalculateWidthOf(IEnumerable<MenuEntry> entries) => 60; // TODO: Ersätt med en beräknad bredd, ta bredaste texten bland valen

        public static int GetFirstSelectableEntry(IEnumerable<MenuEntry> entries)
        {
            var list = entries.ToList();
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].CanBeSelected) return i;
            }
            return 0;
        }

        public static int GetPreviousValidEntry(IEnumerable<MenuEntry> entries, int index)
        {
            var list = entries.ToList();
            for (int i = index - 1; i >= 0; i--)
            {
                if (list[i].CanBeSelected) return i;
            }
            return index;
        }

        public static int GetNextValidEntry(IEnumerable<MenuEntry> entries, int index)
        {
            var list = entries.ToList();
            for (int i = index + 1; i < list.Count; i++)
            {
                if (list[i].CanBeSelected) return i;
            }
            return index;
        }

        public void Print()
        {
            shouldClose = false;
            var width = Width;
            if (width <= 0)
            {
                width = CalculateWidthOf(menu);
            }

            Console.Clear();
            int choice = GetFirstSelectableEntry(menu);
            int previousWidth = Console.WindowWidth;
            int previousHeight = Console.WindowHeight;
            while (!shouldClose)
            {
                // Återställ consollen
                Console.CursorTop = 0;
                Console.CursorLeft = 0;
                Console.CursorVisible = false;

                if (previousWidth != Console.WindowWidth || previousHeight != Console.WindowHeight)
                {
                    Console.Clear();
                    previousWidth = Console.WindowWidth;
                    previousHeight = Console.WindowHeight;
                }

                // Rendera alla menyval
                int y = 0;
                for (int i = 0; i < menu.Count; i++)
                {
                    if (choice == i)
                    {
                        y = Console.CursorTop;
                    }
                    menu[i].Print(false, width);
                }

                // Avsluta med att rendera den valda igen så att den koden kan sätta muspekarpositionen
                Console.CursorTop = y;
                menu[choice].Print(true, width);


                // Hantera tangentbordet
                var key = Console.ReadKey(true);
                var eventType = menu[choice].OnKey(key);

                if (eventType == KeyEvent.MoveUp)
                {
                    choice = GetPreviousValidEntry(menu, choice);
                }
                else if (eventType == KeyEvent.MoveDown)
                {
                    choice = GetNextValidEntry(menu, choice);
                }
                else if (eventType == KeyEvent.GoBack)
                {
                    Console.Clear();
                    return;
                }
            }
        }
    }
}
