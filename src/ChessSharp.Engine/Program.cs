namespace ChessSharp;

internal static class Program
{
    private static void Main()
    {
        Trie<int> t = new Trie<int>();

        t.Add("hello", 2);
        t.Add("hell", 1);
        t.Add("hail", 10);
        t.Add("amortized", 3);

        Console.WriteLine(t.DebuggerDisplay);

        UciReaderWriter urw = new UciReaderWriter(Console.In, Console.Out);

        urw.Start();
    }
}
