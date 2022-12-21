using System;
using System.Collections;
using System.Collections.Generic;

namespace ChessSharp;

public class TrieNode<T> : IEnumerable<TrieNode<T>>
{
    private readonly Dictionary<char, TrieNode<T>> _children = new Dictionary<char, TrieNode<T>>();

    private T? _value;

    public TrieNode() { }

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

    public TrieNode<T>? this[char symbol]
    {
        get
        {
            _children.TryGetValue(symbol, out TrieNode<T>? result);

            return result;
        }
    }

    public void Add(char symbol, TrieNode<T> child)
    {
        _children.Add(symbol, child);
    }

    public void Clear()
    {
        _children.Clear();
    }

    public IEnumerator<TrieNode<T>> GetEnumerator()
    {
        return _children.Values.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return _children.Values.GetEnumerator();
    }
}
