using System.Collections;
using System.Collections.Generic;
using System.Linq;

public static class Recursion
{
    /// <summary>
    /// #############
    /// # Problem 1 #
    /// #############
    /// Using recursion, find the sum of 1^2 + 2^2 + 3^2 + ... + n^2
    /// and return it.  Remember to both express the solution 
    /// in terms of recursive call on a smaller problem and 
    /// to identify a base case (terminating case).  If the value of
    /// n <= 0, just return 0.   A loop should not be used.
    /// </summary>
    public static int SumSquaresRecursive(int n)
    {
        // Base case: if n is less than or equal to 0, return 0
        if (n <= 0)
            return 0;
    
        // Recursive case: sum of squares of n and all numbers less than n
        return n * n + SumSquaresRecursive(n - 1);
    }

    /// <summary>
    /// #############
    /// # Problem 2 #
    /// #############
    /// Using recursion, insert permutations of length
    /// 'size' from a list of 'letters' into the results list.  
    /// This function should assume that each letter is unique (i.e. the 
    /// function does not need to find unique permutations).
    /// </summary>
    public static void PermutationsChoose(List<string> results, string letters, int size, string word = "")
    {
        // Base case: if the word has reached the required size, add it to the results
        if (word.Length == size)
        {
            results.Add(word);
            return;
        }
    
        // Recursive case: loop through each letter and generate the next permutation
        for (int i = 0; i < letters.Length; i++)
        {
            // Exclude the letter already used and generate the next permutation
            string remainingLetters = letters.Substring(0, i) + letters.Substring(i + 1);
            PermutationsChoose(results, remainingLetters, size, word + letters[i]);
        }
    }

    /// <summary>
    /// #############
    /// # Problem 3 #
    /// #############
    /// Imagine that there was a staircase with 's' stairs.  
    /// We want to count how many ways there are to climb 
    /// the stairs, where you can take 1, 2, or 3 steps at a time.  
    /// </summary>
    public static decimal CountWaysToClimb(int s, Dictionary<int, decimal>? remember = null)
    {
        // Initialize the dictionary if it's null
        if (remember == null)
            remember = new Dictionary<int, decimal>();

        // Base Cases
        if (s == 0)
            return 0;
        if (s == 1)
            return 1;
        if (s == 2)
            return 2;
        if (s == 3)
            return 4;

        // Check if the result is already memoized
        if (remember.ContainsKey(s))
            return remember[s];

        // Recursive case: the number of ways to reach step s is the sum of the ways
        // to reach step s-1, step s-2, and step s-3
        decimal ways = CountWaysToClimb(s - 1, remember) + CountWaysToClimb(s - 2, remember) + CountWaysToClimb(s - 3, remember);

        // Memoize the result
        remember[s] = ways;
    
        return ways;
    }

    /// <summary>
    /// #############
    /// # Problem 4 #
    /// #############
    /// A binary string is a string consisting of just 1's and 0's.  
    /// A wildcard '*' represents multiple binary string possibilities.
    /// This function generates all binary strings that match the pattern.
    /// </summary>
    public static void WildcardBinary(string pattern, List<string> results)
    {
        // Base case: If there's no '*' in the pattern, add it to results
        if (!pattern.Contains("*"))
        {
            results.Add(pattern);
            return;
        }
        
        // Recursive case: Find the first '*' in the pattern
        int starIndex = pattern.IndexOf("*");
        
        // Two recursive calls: one replacing '*' with '0' and the other with '1'
        string patternWithZero = pattern.Substring(0, starIndex) + '0' + pattern.Substring(starIndex + 1);
        string patternWithOne = pattern.Substring(0, starIndex) + '1' + pattern.Substring(starIndex + 1);
        
        // Recursively generate patterns for both modified strings
        WildcardBinary(patternWithZero, results);
        WildcardBinary(patternWithOne, results);
    }

    /// <summary>
    /// #############
    /// # Problem 5 #
    /// #############
    /// Using recursion to insert all paths from (0,0) to the 'end' square 
    /// of the maze into the results list.
    /// </summary>
    public static void SolveMaze(List<string> results, Maze maze, int x = 0, int y = 0, List<ValueTuple<int, int>>? currPath = null)
    {
        // If this is the first time running the function, then we need
        // to initialize the currPath list.
        if (currPath == null) {
            currPath = new List<ValueTuple<int, int>>();
        }
        
        // currPath.Add((1,2)); // Use this syntax to add to the current path

        // TODO Start Problem 5
        // ADD CODE HERE

        // results.Add(currPath.AsString()); // Use this to add your path to the results array keeping track of complete maze solutions when you find the solution.
    }
}

