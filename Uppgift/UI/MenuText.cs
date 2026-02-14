namespace Uppgift.UI
{
    public class MenuText : MenuEntry
    {
        private string text;


        public MenuText(string text)
        {
            this.text = text;
        }

        public override bool CanBeSelected => false;

        public override void Print(bool selected, int width)
        {
            var status = Status?.Invoke() ?? MenuEntryStatus.Normal;
            Console.ForegroundColor = status switch
            {
                MenuEntryStatus.Disabled => ConsoleColor.DarkGray,
                MenuEntryStatus.Invalid => ConsoleColor.Red,
                _ => ConsoleColor.Gray,
            };
            Console.WriteLine(text);
            Console.ResetColor();
        }
    }
}
