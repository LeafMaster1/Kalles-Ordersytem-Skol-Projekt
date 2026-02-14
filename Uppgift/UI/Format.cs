namespace Uppgift.UI
{
    public class Format
    {
        public static string AsTitle(string text) =>
            string.Join(" ", text.Select(s => s.ToString()));
    }
}