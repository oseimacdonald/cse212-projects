using System.Collections;

public class LinkedList : IEnumerable<int>
{
    private Node? _head;
    private Node? _tail;

    private class Node
    {
        public int Data;
        public Node? Next;
        public Node? Prev;

        public Node(int data)
        {
            Data = data;
        }
    }

    public void InsertHead(int value)
    {
        Node newNode = new(value);

        if (_head is null)
        {
            _head = newNode;
            _tail = newNode;
        }
        else
        {
            newNode.Next = _head;
            _head.Prev = newNode;
            _head = newNode;
        }
    }

    public void InsertTail(int value)
    {
        Node newNode = new(value);

        if (_tail is null)
        {
            _head = newNode;
            _tail = newNode;
        }
        else
        {
            _tail.Next = newNode;
            newNode.Prev = _tail;
            _tail = newNode;
        }
    }

    public void RemoveHead()
    {
        if (_head == _tail)
        {
            _head = null;
            _tail = null;
        }
        else if (_head is not null)
        {
            // Check that _head.Next is not null before dereferencing
            if (_head.Next != null)
            {
                _head.Next.Prev = null;
            }
            _head = _head.Next;
        }
    }

    public void RemoveTail()
    {
        if (_tail is null)
        {
            return;
        }

        if (_head == _tail)
        {
            _head = null;
            _tail = null;
        }
        else
        {
            // Check that _tail.Prev is not null before dereferencing
            if (_tail.Prev != null)
            {
                _tail = _tail.Prev;
                _tail.Next = null;
            }
        }
    }

    public void InsertAfter(int value, int newValue)
    {
        Node? curr = _head;
        while (curr is not null)
        {
            if (curr.Data == value)
            {
                if (curr == _tail)
                {
                    InsertTail(newValue);
                }
                else
                {
                    Node newNode = new(newValue);
                    newNode.Prev = curr;
                    newNode.Next = curr.Next;

                    if (curr.Next != null)
                    {
                        curr.Next.Prev = newNode;
                    }

                    curr.Next = newNode;
                }

                return;
            }

            curr = curr.Next;
        }
    }

    public void Remove(int value)
    {
        Node? curr = _head;

        while (curr is not null)
        {
            if (curr.Data == value)
            {
                if (curr == _head)
                {
                    RemoveHead();
                }
                else if (curr == _tail)
                {
                    RemoveTail();
                }
                else
                {
                    if (curr.Prev != null)
                    {
                        curr.Prev.Next = curr.Next;
                    }

                    if (curr.Next != null)
                    {
                        curr.Next.Prev = curr.Prev;
                    }
                }

                return;
            }

            curr = curr.Next;
        }
    }

    public void Replace(int oldValue, int newValue)
    {
        Node? curr = _head;

        while (curr != null)
        {
            if (curr.Data == oldValue)
            {
                curr.Data = newValue;
            }

            curr = curr.Next;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }

    public IEnumerator<int> GetEnumerator()
    {
        var curr = _head;
        while (curr is not null)
        {
            yield return curr.Data;
            curr = curr.Next;
        }
    }

    public IEnumerable Reverse()
    {
        Node? curr = _tail;
        while (curr is not null)
        {
            yield return curr.Data;
            curr = curr.Prev;
        }
    }

    public override string ToString()
    {
        return "<LinkedList>{" + string.Join(", ", this) + "}";
    }

    // Just for testing.
    public Boolean HeadAndTailAreNull()
    {
        return _head is null && _tail is null;
    }

    // Just for testing.
    public Boolean HeadAndTailAreNotNull()
    {
        return _head is not null && _tail is not null;
    }
}

public static class IntArrayExtensionMethods
{
    public static string AsString(this IEnumerable array)
    {
        return "<IEnumerable>{" + string.Join(", ", array.Cast<int>()) + "}";
    }
}

