using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trie
{
    public class Trie<E>
    {
        private class Node
        {
            private char key;
            private E value;
            private Dictionary<char, Node> children;
            private bool isTerminal;

            public Node(char key)
            {
                this.key = key;
                this.children = new Dictionary<char, Node>();
                this.isTerminal = false;
            }

            public Node(E value)
            {
                this.value = value;
                this.isTerminal = true;
                this.children = null;
            }

            public bool IsTerminal()
            {
                return isTerminal;
            }

            public Dictionary<char, Node> GetChildren()
            {
                return children;
            }

            public E GetValue()
            {
                return value;
            }

            public String ToString()
            {
                if (children == null)
                {
                    return key + ": " + value;
                }
                return "(" + key + ")";
            }
        }

        private Node root;
        private int size;

        public Trie()
        {
            root = new Node(default(char));
        }

        public Trie(string[] entries, E[] values)
        {
            root = new Node(default(char));
        }

        public void Insert(string key, E value)
        {
            Node curr = root;

            foreach (char c in key)
            {
                if (!curr.GetChildren().ContainsKey(c))
                {
                    curr.GetChildren().Add(c, new Node(c));
                    curr = curr.GetChildren()[c];
                }
                else
                {
                    curr = curr.GetChildren()[c];
                }
            }

            curr.GetChildren().Add(default(char), new Node(value));
        }

        public E Find(string key)
        {
            Node curr = root;

            foreach (char c in key)
            {
                curr = curr.GetChildren()[c];
            }

            curr = curr.GetChildren()[default(char)];
            return curr.GetValue();
        }

        public string ToString()
        {
            StringBuilder sb = new StringBuilder();

            Stack<KeyValuePair<Node, int>> stack = new Stack<KeyValuePair<Node, int>>();
            stack.Push(new KeyValuePair<Node, int>(root, 0));

            while (stack.Count > 0)
            {
                KeyValuePair<Node, int> curr = stack.Pop();

                for (int x = 0; x < curr.Value - 1; x++)
                {
                    sb.Append("   ");
                }
                if (curr.Value >= 1)
                {
                    sb.Append("-->");
                }
                sb.Append(curr.Key.ToString());

                if (curr.Key.GetChildren() == null)
                {
                    sb.Append("\n");
                    continue;
                }

                foreach (KeyValuePair<char, Node> pair in curr.Key.GetChildren())
                {
                    stack.Push(new KeyValuePair<Node, int>(pair.Value, curr.Value + 1));
                }
                sb.Append("\n");
            }

            return sb.ToString();
        }

        public int getSize()
        {
            return size;
        }
    }
}
