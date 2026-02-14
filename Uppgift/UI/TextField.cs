namespace Uppgift.UI
{
    public class TextField : MenuEntry
    {
        public Func<string, bool> Validate { get; set; } = _ => true;
        public bool AllowedToBeEmpty { get; set; } = true;
        public string Value { get; private set; }

        public string Label { get; private set; }
        private int position = -1;


        public TextField(string label, string defaultValue = "")
        {
            Label = label;
            Value = defaultValue;
            Status = DefaultStatus;
        }

        private MenuEntryStatus DefaultStatus()
        {
            if (!AllowedToBeEmpty && string.IsNullOrWhiteSpace(Value))
            {
                return MenuEntryStatus.Invalid;
            }

            if (!Validate(Value))
            {
                return MenuEntryStatus.Invalid;
            }

            return MenuEntryStatus.Normal;
        }

        public override void Print(bool selected, int width)
        {
            var availableInputWidth = width - (Label.Length + 4);
            var value = Value.Length >= availableInputWidth
                ? (Value.Substring(0, availableInputWidth - 3) + "...")
                : Value.PadRight(availableInputWidth);

            var status = Status?.Invoke() ?? MenuEntryStatus.Normal;

            var textColor = status switch
            {
                MenuEntryStatus.Disabled => ConsoleColor.DarkGray,
                MenuEntryStatus.Invalid => selected ? ConsoleColor.Red : ConsoleColor.DarkRed,
                _ => selected ? ConsoleColor.White : ConsoleColor.Gray,
            };

            Console.ForegroundColor = status switch
            {
                MenuEntryStatus.Disabled => ConsoleColor.DarkGray,
                MenuEntryStatus.Invalid => selected ? ConsoleColor.Red : ConsoleColor.DarkRed,
                _ => selected ? ConsoleColor.Yellow : ConsoleColor.Gray,
            };
            Console.Write($"  {Label}");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(": ");

            if (selected)
            {
                if (position < 0) position = Value.Length;
                var top = Console.CursorTop;
                var left = Console.CursorLeft;

                Console.ForegroundColor = textColor;
                Console.Write(value);

                Console.CursorLeft = left + position;
                Console.CursorVisible = true;
            }
            else
            {
                Console.ForegroundColor = textColor;
                Console.WriteLine(value);
                Console.ResetColor();
                Console.WriteLine();
            }
        }

        public override KeyEvent OnKey(ConsoleKeyInfo key)
        {
            if (key.Key == ConsoleKey.Backspace)
            {
                Value = Backspace(Value, position);
                position = Math.Max(0, position - 1);
                return KeyEvent.Ignore;
            }
            else if (key.Key == ConsoleKey.LeftArrow)
            {
                position = Math.Max(0, position - 1);
                return KeyEvent.Ignore;
            }
            else if (key.Key == ConsoleKey.RightArrow)
            {
                position = Math.Min(Value.Length, position + 1);
                return KeyEvent.Ignore;
            }
            else if (key.Key == ConsoleKey.Enter)
            {
                return KeyEvent.MoveDown;
            }
            else if (key.Key == ConsoleKey.Escape)
            {
                return KeyEvent.GoBack;
            }
            else if (key.KeyChar != '\0')
            {
                if (position >= Value.Length) Value += key.KeyChar;
                else if (position <= 0) Value = key.KeyChar + Value;
                else
                {
                    var left = Value.Substring(0, position);
                    var right = Value.Substring(position);
                    Value = left + key.KeyChar + right;
                }
                position++;
                return KeyEvent.Ignore;
            }

            return base.OnKey(key);
        }

        public static string Backspace(string value, int position)
        {
            if (position <= 0 || string.IsNullOrEmpty(value))
            {
                return value;
            }
            return value.Remove(position -1,  1);
        }
    }
}
