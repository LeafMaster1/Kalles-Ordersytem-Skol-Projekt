namespace Uppgift.UI
{
    public class MenuButton : MenuEntry
    {
        private string name;
        private string? description;
        private Action render;


        public MenuButton(string name, string? description = null, Action? render = null)
        {
            this.name = name;
            this.description = description;
            this.render = render ?? (() => { });
        }

        public override void Print(bool selected, int width)
        {
            var status = Status?.Invoke() ?? MenuEntryStatus.Normal;

            var nameColor = status switch
            {
                MenuEntryStatus.Disabled => ConsoleColor.DarkGray,
                MenuEntryStatus.Invalid => ConsoleColor.Red,
                _ => selected ? ConsoleColor.Yellow : ConsoleColor.Gray,
            };
            var descriptionColor = status switch
            {
                MenuEntryStatus.Disabled => ConsoleColor.DarkGray,
                MenuEntryStatus.Invalid => ConsoleColor.DarkRed,
                _ => selected ? ConsoleColor.Gray : ConsoleColor.DarkGray,
            };
            var outlineColor = status switch
            {
                MenuEntryStatus.Disabled => ConsoleColor.DarkGray,
                MenuEntryStatus.Invalid => selected ? ConsoleColor.Red : ConsoleColor.DarkRed,
                _ => selected ? ConsoleColor.Yellow : ConsoleColor.DarkGray,
            };

            var vertical = selected ? '│' : '┊';
            var horizontal = new string(selected ? '─' : '┄', width - 2);

            Console.ForegroundColor = outlineColor;
            Console.WriteLine($"╭{horizontal}╮");

            Console.ForegroundColor = outlineColor;
            Console.Write($"{vertical} ");

            Console.ForegroundColor = nameColor;
            Console.Write(name.PadRight(width - 4));

            Console.ForegroundColor = outlineColor;
            Console.WriteLine($" {vertical}");

            if (!string.IsNullOrWhiteSpace(this.description))
            {
                Console.Write($"{vertical} ");

                Console.ForegroundColor = descriptionColor;
                Console.Write(description.PadRight(width - 4));

                Console.ForegroundColor = outlineColor;
                Console.WriteLine($" {vertical}");
            }

            Console.ForegroundColor = outlineColor;
            Console.WriteLine($"╰{horizontal}╯");


            Console.ResetColor();
            Console.WriteLine();
        }

        public override KeyEvent OnKey(ConsoleKeyInfo key)
        {
            if (key.Key == ConsoleKey.Spacebar || key.Key == ConsoleKey.Enter)
            {
                var status = Status?.Invoke() ?? MenuEntryStatus.Normal;
                if (status == MenuEntryStatus.Disabled || status == MenuEntryStatus.Invalid)
                {
                    return KeyEvent.Ignore;
                }

                Console.Clear();
                render();
                Console.Clear();
                return KeyEvent.Ignore;
            }

            return base.OnKey(key);
        }
    }
}
