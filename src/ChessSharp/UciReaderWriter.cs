using System;
using System.Collections.Generic;
using System.IO;

namespace ChessSharp;

public class UciReaderWriter : IDisposable
{
    private readonly TextReader _input;
    private readonly TextWriter _output;

    private bool _disposed;

    public UciReaderWriter(TextReader input, TextWriter output)
    {
        _input = input;
        _output = output;
    }

    public void Start()
    {
        int read;
        bool keepReading = true;
        Trie<int> commands = new Trie<int>()
        {
            { "uci", 1 },
            { "isready", 2 }
        };
        TrieNode<int> current = commands.Root;
        Queue<int> instructions = new Queue<int>();

        while ((read = _input.Read()) is not -1)
        {
            char symbol = (char)read;

            if (char.IsWhiteSpace(symbol))
            {
                enqueue();

                current = commands.Root;
                keepReading = true;
            }
            else if (keepReading)
            {
                TrieNode<int>? child = current.Children[symbol];

                if (child is null)
                {
                    keepReading = false;
                    current = commands.Root;
                }
                else
                {
                    current = child;
                }
            }
        }

        enqueue();

        void enqueue()
        {
            if (current.IsTerminal)
            {
                Console.WriteLine(current.Value);

                instructions.Enqueue(current.Value);
            }
        }
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _input.Dispose();
                _output.Dispose();
            }

            _disposed = true;
        }
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
