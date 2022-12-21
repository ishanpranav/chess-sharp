using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace ChessSharp;

[DebuggerDisplay("{DebuggerDisplay}")]
public class Trie<T> : IDictionary<string, T>
{
    public Trie() { }

    public TrieNode<T> Root { get; } = new TrieNode<T>();

    public int Count { get; private set; }

    public T this[string key]
    {
        get
        {
            TrieNode<T>? current = Root;

            foreach (char symbol in key)
            {
                current = current.Children.GetValueOrDefault(symbol);

                if (current is null)
                {
                    throw new KeyNotFoundException();
                }
            }

            return current.Value;
        }
        set
        {
            Add(key, value);
        }
    }

    public ICollection<string> Keys
    {
        get
        {
            throw new NotImplementedException();
        }
    }

    public ICollection<T> Values
    {
        get
        {
            throw new NotImplementedException();
        }
    }

    public bool IsReadOnly
    {
        get
        {
            return false;
        }
    }

    public void Add(string key, T value)
    {
        TrieNode<T> current = Root;

        foreach (char symbol in key)
        {
            TrieNode<T>? child = current.Children.GetValueOrDefault(symbol);

            if (child is null)
            {
                child = new TrieNode<T>();

                current.Add(symbol, child);
            }

            current = child;
        }

        current.Value = value;
    }

    void ICollection<KeyValuePair<string, T>>.Add(KeyValuePair<string, T> item)
    {
        Add(item.Key, item.Value);
    }

    public bool ContainsKey(string key)
    {
        return TryGetValue(key, out _);
    }

    bool ICollection<KeyValuePair<string, T>>.Contains(KeyValuePair<string, T> item)
    {
        return TryGetValue(item.Key, out _);
    }

    public bool TryGetValue(string key, [MaybeNullWhen(false)] out T value)
    {
        try
        {
            value = this[key];

            return true;
        }
        catch (KeyNotFoundException)
        {
            value = default;

            return false;
        }
    }

    public bool Remove(string key)
    {
        throw new NotImplementedException();
    }

    bool ICollection<KeyValuePair<string, T>>.Remove(KeyValuePair<string, T> item)
    {
        return Remove(item.Key);
    }

    public void Clear()
    {
        Root.Clear();
    }

    public void CopyTo(KeyValuePair<string, T>[] array, int arrayIndex)
    {
        throw new NotImplementedException();
    }

    public string DebuggerDisplay
    {
        get
        {
            StringBuilder result = new StringBuilder();
            Stack<(char, TrieNode<T>, int depth)> stack = new Stack<(char, TrieNode<T>, int)>();

            stack.Push((default, Root, depth: 0));

            while (stack.TryPop(out (char symbol, TrieNode<T> node, int depth) current))
            {
                for (int depth = 0; depth < current.depth - 1; depth++)
                {
                    result.Append(' ', repeatCount: 3);
                }

                if (current.depth >= 1)
                {
                    result.Append("-->");
                }

                result.Append(current.symbol);

                if (current.node.IsTerminal)
                {
                    T value = current.node.Value;

                    if (value is not null)
                    {
                        result
                            .Append(value)
                            .AppendLine();
                    }
                }

                foreach (KeyValuePair<char, TrieNode<T>> child in current.node.Children)
                {
                    stack.Push((child.Key, child.Value, current.depth + 1));
                }

                result.AppendLine();
            }

            return result.ToString();
        }
    }

    public IEnumerator<KeyValuePair<string, T>> GetEnumerator()
    {
        throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        throw new NotImplementedException();
    }
}
