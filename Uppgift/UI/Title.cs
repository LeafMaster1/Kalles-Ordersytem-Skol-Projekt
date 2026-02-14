namespace Uppgift.UI
{
    public class Title : MenuEntry
    {
        private string title;

        public Title(string title) => this.title = Format.AsTitle(title);

        public override bool CanBeSelected => false;

        public override void Print(bool selected, int width)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(title);
            Console.ResetColor();
            Console.WriteLine();
        }
    }
}
