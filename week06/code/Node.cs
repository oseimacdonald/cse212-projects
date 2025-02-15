public class Node
{
    public int Data { get; set; }
    public Node? Right { get; private set; }
    public Node? Left { get; private set; }

    public Node(int data)
    {
        this.Data = data;
    }

    public void Insert(int value)
{
    if (value == Data) 
    {
        // If the value is equal to the current node's data, don't insert it (no duplicates).
        return;
    }
    
    if (value < Data)
    {
        // Insert to the left
        if (Left is null)
            Left = new Node(value);
        else
            Left.Insert(value);
    }
    else
    {
        // Insert to the right
        if (Right is null)
            Right = new Node(value);
        else
            Right.Insert(value);
    }
}


    public bool Contains(int value)
    {
    if (value == Data)
    {
        return true;
    }
    
    if (value < Data && Left != null)
    {
        return Left.Contains(value);
    }
    
    if (value > Data && Right != null)
    {
        return Right.Contains(value);
    }
    
    return false; // If we reach here, value is not found
}


    public int GetHeight()
    {
    // Base case: if the node is null, the height is 0
    if (this == null) return 0;
    
    int leftHeight = Left?.GetHeight() ?? 0;  // If Left is null, treat it as height 0
    int rightHeight = Right?.GetHeight() ?? 0;  // If Right is null, treat it as height 0
    
    return 1 + Math.Max(leftHeight, rightHeight); // 1 for the current node plus the max height of subtrees
}

}