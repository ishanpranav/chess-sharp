using System;
using System.Collections.Generic;

namespace ChessSharp;

public class TrieNode<T>
{
    private readonly Dictionary<char, TrieNode<T>> _children = new Dictionary<char, TrieNode<T>>();

    private T? _value;

    public TrieNode() { }

    public IReadOnlyDictionary<char, TrieNode<T>> Children
    {
        get
        {
            return _children;
        }
    }

    public T Value
    {
        get
        {
            if (!IsTerminal)
            {
                throw new InvalidOperationException();
            }

            return _value!;
        }
        internal set
        {
            _value = value;
            IsTerminal = true;
        }
    }

    public bool IsTerminal { get; private set; }

    internal void Add(char symbol, TrieNode<T> child)
    {
        _children.Add(symbol, child);
    }

    internal void Clear()
    {
        _children.Clear();
    }
}
