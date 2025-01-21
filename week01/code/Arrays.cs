public static class Arrays
{
    /// <summary>
    /// This function will produce an array of size 'length' starting with 'number' followed by multiples of 'number'.  For 
    /// example, MultiplesOf(7, 5) will result in: {7, 14, 21, 28, 35}.  Assume that length is a positive
    /// integer greater than 0.
    /// </summary>
    
    public static double[] MultiplesOf(double number, int length)
    {
        // step 1: initialize an array of size 'length' to store the multiples.
        // we'll create an array of doubles, as we are working with floating-point values.
        double[] multiples = new double[length];

        // step 2: use a loop to generate multiples of the given number.
        // we will iterate from 0 to 'length - 1' to fill each slot in the array.
        for (int i = 0; i < length; i++)
        {
            // step 3: calculate each multiple of 'number' using (i + 1) because multiples start at 'number * 1'.
            multiples[i] = number * (i + 1);
        }

        // step 4: after the loop, return the array of the multiples.
        return multiples; 
    }

    /// <summary>
    /// rotate the 'data' to the right by the 'amount'. For example, if the data is 
    ///List<int>{1,2,3,4,5,6,7,8,9} and an amount is 3 , then the list after the function runs should be 
    /// List<int>{7,8,9,1,2,3,4,5,6}.
    /// </summary>
    
    public static void RotateListRight(List<int> data, int amount)
    {
        // step 1: handle edge cases where the list is empty or the rotation amount is zero.
        // if the List is empty, or if the amount is zero, no rotation is needed, so we return immediately.
        if (data.Count == 0 || amount == 0)
        {
            return; // no need to rotate, return the list as it is.
        }

        // step 2: use the modulo operation to handle large rotation amounts.
        // if the rotation amount exceeds the lenght of the list, we can use modulo to ensure the amount 
        // is within valid bounds. For example, rotating a list of the size 5 by 7 is equivalent to rotating it by 2.
        amount = amount % data.Count; // this ensures that the rotation amount is within the bounds of the List size.

        // step 3: split the list into 2 parts.
        // we will split the list into:
        // - the last 'amount' elements (these will move to the front of the list)
        // - the remaining elements from the beginning of the list (these will shift to the right).
        List<int> part1 = data.GetRange(data.Count - amount, amount); // last 'amount' elements.
        List<int> part2 = data.GetRange(0, data.Count - amount); // remaining elements.

        // step 4: concatenate the 2 parts to form the rotated list.
        // we add 'part1' (which holds the last 'amount' elements) to the front of 'part2'.
        part1.AddRange(part2);

        // step 5: modify the original list to reflect the new rotated order.
        // we first clear the original list, then add the rotated list back into it.
        data.Clear();   // clear the exixting list to replace it with the rotated list.
        data.AddRange(part1);   // add the rotated elements back into the list.
        
    }
}