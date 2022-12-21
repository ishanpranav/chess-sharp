namespace ChessSharp;

internal static class Program
{
    private static void Main()
    {
        UciReaderWriter urw = new UciReaderWriter(Console.In, Console.Out);

        urw.Start();
    }
}
