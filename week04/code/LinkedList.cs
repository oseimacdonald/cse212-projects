using System.Collections;

public class LinkedList : IEnumerable<int>
{
    private Node? _head; // Head of the list, initially null
    private Node? _tail; // Tail of the list, initially null

    // Node class represents each element in the linked list
    private class Node
    {
        public int Data; // Holds the data of the node
        public Node? Next; // Points to the next node in the list
        public Node? Prev; // Points to the previous node in the list

        // Constructor to initialize the node with data
        public Node(int data)
        {
            Data = data;
        }
    }

    /// <summary>
    /// Insert a new node at the front (head) of the linked list.
    /// </summary>
    public void InsertHead(int value)
    {
        Node newNode = new(value); // Create a new node with the provided value

        // If the list is empty, set both head and tail to the new node
        if (_head is null)
        {
            _head = newNode;
            _tail = newNode;
        }
        else
        {
            // If the list is not empty, insert the new node at the head
            newNode.Next = _head; // Link the new node to the current head
            _head.Prev = newNode; // Link the current head's prev to the new node
            _head = newNode; // Move the head pointer to the new node
        }
    }

    /// <summary>
    /// Insert a new node at the back (tail) of the linked list.
    /// </summary>
    public void InsertTail(int value)
    {
        Node newNode = new(value); // Create a new node with the provided value

        // If the list is empty, both head and tail should point to the new node
        if (_tail is null)
        {
            _head = newNode;
            _tail = newNode;
        }
        else
        {
            // If the list is not empty, insert the new node at the tail
            _tail.Next = newNode; // Link the current tail to the new node
            newNode.Prev = _tail; // Link the new node's prev to the current tail
            _tail = newNode; // Move the tail pointer to the new node
        }
    }

    /// <summary>
    /// Remove the first node (head) of the linked list.
    /// </summary>
    public void RemoveHead()
    {
        // If the list has only one item or is empty, set both head and tail to null
        if (_head == _tail)
        {
            _head = null;
            _tail = null;
        }
        else if (_head is not null)
        {
            // If the list has more than one item, remove the head node
            if (_head.Next != null)
            {
                _head.Next.Prev = null; // Disconnect the second node from the first node
            }
            _head = _head.Next; // Move the head pointer to the second node
        }
    }

    /// <summary>
    /// Remove the last node (tail) of the linked list.
    /// </summary>
    public void RemoveTail()
    {
        // If the list is empty, do nothing
        if (_tail is null)
        {
            return;
        }

        // If the list has only one node, remove both head and tail
        if (_head == _tail)
        {
            _head = null;
            _tail = null;
        }
        else
        {
            // If the list has more than one node, remove the tail node
            if (_tail.Prev != null)
            {
                _tail = _tail.Prev; // Move the tail pointer to the previous node
                _tail.Next = null;   // Set the new tail's next node to null
            }
        }
    }

    /// <summary>
    /// Insert a new node with 'newValue' after the first occurrence of 'value' in the list.
    /// </summary>
    public void InsertAfter(int value, int newValue)
    {
        Node? curr = _head; // Start from the head of the list

        // Search for the node that contains the specified value
        while (curr is not null)
        {
            // If the value is found, insert the new node after it
            if (curr.Data == value)
            {
                // If the node is the tail, simply insert at the tail
                if (curr == _tail)
                {
                    InsertTail(newValue);
                }
                else
                {
                    // Otherwise, create a new node and link it appropriately
                    Node newNode = new(newValue);
                    newNode.Prev = curr; // Link new node to the current node (prev)
                    newNode.Next = curr.Next; // Link new node to the next node
                    if (curr.Next != null)
                    {
                        curr.Next.Prev = newNode; // Link the next node's prev to the new node
                    }
                    curr.Next = newNode; // Link the current node to the new node
                }

                return; // Exit once the node is inserted
            }

            curr = curr.Next; // Move to the next node in the list
        }
    }

    /// <summary>
    /// Remove the first node that contains the specified 'value'.
    /// </summary>
    public void Remove(int value)
    {
        Node? curr = _head; // Start from the head of the list

        // Search for the node that contains the specified value
        while (curr is not null)
        {
            if (curr.Data == value)
            {
                // If the node to be removed is the head
                if (curr == _head)
                {
                    RemoveHead();
                }
                // If the node to be removed is the tail
                else if (curr == _tail)
                {
                    RemoveTail();
                }
                else
                {
                    // If the node is somewhere in the middle, relink the surrounding nodes
                    if (curr.Prev != null)
                    {
                        curr.Prev.Next = curr.Next;
                    }

                    if (curr.Next != null)
                    {
                        curr.Next.Prev = curr.Prev;
                    }
                }

                return; // Exit after the node is removed
            }

            curr = curr.Next; // Move to the next node in the list
        }
    }

    /// <summary>
    /// Replace all occurrences of 'oldValue' with 'newValue' in the linked list.
    /// </summary>
    public void Replace(int oldValue, int newValue)
    {
        Node? curr = _head; // Start from the head of the list

        // Traverse the list to find all occurrences of 'oldValue'
        while (curr != null)
        {
            if (curr.Data == oldValue)
            {
                curr.Data = newValue; // Replace the value
            }

            curr = curr.Next; // Move to the next node
        }
    }

    // IEnumerable interface method to support non-generic iteration (needed for collection access)
    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }

    /// <summary>
    /// Iterates forward through the linked list and yields each value.
    /// </summary>
    public IEnumerator<int> GetEnumerator()
    {
        var curr = _head; // Start at the head of the list

        while (curr is not null)
        {
            yield return curr.Data; // Yield the current node's data
            curr = curr.Next; // Move to the next node
        }
    }

    /// <summary>
    /// Iterates backward through the linked list and yields each value.
    /// </summary>
    public IEnumerable Reverse()
    {
        Node? curr = _tail; // Start at the tail of the list

        while (curr is not null)
        {
            yield return curr.Data; // Yield the current node's data
            curr = curr.Prev; // Move to the previous node
        }
    }

    /// <summary>
    /// Returns a string representation of the linked list.
    /// </summary>
    public override string ToString()
    {
        return "<LinkedList>{" + string.Join(", ", this) + "}";
    }

    // For testing purposes, checks if both head and tail are null
    public Boolean HeadAndTailAreNull()
    {
        return _head is null && _tail is null;
    }

    // For testing purposes, checks if both head and tail are not null
    public Boolean HeadAndTailAreNotNull()
    {
        return _head is not null && _tail is not null;
    }
}

public static class IntArrayExtensionMethods
{
    // Extension method to convert an IEnumerable to a formatted string
    public static string AsString(this IEnumerable array)
    {
        return "<IEnumerable>{" + string.Join(", ", array.Cast<int>()) + "}";
    }
}
