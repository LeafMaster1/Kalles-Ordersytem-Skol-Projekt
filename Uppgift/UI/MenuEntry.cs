namespace Uppgift.UI
{
    public abstract class MenuEntry
    {
        public Func<MenuEntryStatus>? Status { get; set; }

        public abstract void Print(bool selected, int width);

        public virtual KeyEvent OnKey(ConsoleKeyInfo key)
        {
            if (key.Key == ConsoleKey.UpArrow) return KeyEvent.MoveUp;
            if (key.Key == ConsoleKey.DownArrow) return KeyEvent.MoveDown;
            if (key.Key == ConsoleKey.Backspace || key.Key == ConsoleKey.Escape) return KeyEvent.GoBack;

            return KeyEvent.Ignore;
        }

        public virtual bool CanBeSelected => Status?.Invoke() != MenuEntryStatus.Disabled;
    }
}
