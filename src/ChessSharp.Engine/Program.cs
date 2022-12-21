namespace ChessSharp;
using Trie;

internal static class Program
{
    private static void Main()
    {
        Trie.Trie<int> test = new Trie.Trie<int>();
        test.Insert("Tri", 1);
        test.Insert("tri", 3);
        test.Insert("Tris", 4);
        Console.WriteLine(test.Find("tri"));
    }
}
