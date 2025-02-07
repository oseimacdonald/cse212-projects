using System.Text.Json;

public static class SetsAndMaps
{
    /// <summary>
    /// The words parameter contains a list of two character 
    /// words (lower case, no duplicates). Using sets, find an O(n) 
    /// solution for returning all symmetric pairs of words.  
    ///
    /// For example, if words was: [am, at, ma, if, fi], we would return :
    ///
    /// ["am & ma", "if & fi"]
    ///
    /// The order of the array does not matter, nor does the order of the specific words in each string in the array.
    /// at would not be returned because ta is not in the list of words.
    ///
    /// As a special case, if the letters are the same (example: 'aa') then
    /// it would not match anything else (remember the assumption above
    /// that there were no duplicates) and therefore should not be returned.
    /// </summary>
    /// <param name="words">An array of 2-character words (lowercase, no duplicates)</param>
    public static string[] FindPairs(string[] words)
    {
        var seenWords = new HashSet<string>(); // Set to store words we have seen
        var pairs = new List<string>(); // List to store the pairs found

        foreach (var word in words)
        {
            // Reverse the current word
            var reversedWord = new string(word.Reverse().ToArray());

            // Check if the reversed word is in the set
            if (seenWords.Contains(reversedWord))
            {
                // If it's a symmetric pair, add to result
                pairs.Add($"{word} & {reversedWord}");
                // Remove both words from the set to avoid duplicates
                seenWords.Remove(reversedWord);
            }
            else
            {
                // Otherwise, add the current word to the set
                seenWords.Add(word);
            }
        }

        // Return the pairs as an array
        return pairs.ToArray();
    }

    /// <summary>
    /// Read a census file and summarize the degrees (education)
    /// earned by those contained in the file.  The summary
    /// should be stored in a dictionary where the key is the
    /// degree earned and the value is the number of people that 
    /// have earned that degree.  The degree information is in
    /// the 4th column of the file.  There is no header row in the
    /// file.
    /// </summary>
    /// <param name="filename">The name of the file to read</param>
    /// <returns>A dictionary where keys are degree types and values are the count of people having that degree</returns>
    public static Dictionary<string, int> SummarizeDegrees(string filename)
    {
        var degrees = new Dictionary<string, int>();

        try
        {
            // Read each line from the census file
            foreach (var line in File.ReadLines(filename))
            {
                var fields = line.Split(",");

                // Make sure the line has enough columns to extract the degree (4th column)
                if (fields.Length >= 4)
                {
                    // Get the degree information (4th column is at index 3)
                    var degree = fields[3].Trim();

                    // Update the dictionary with the degree count
                    if (degrees.ContainsKey(degree))
                    {
                        degrees[degree]++;
                    }
                    else
                    {
                        degrees[degree] = 1;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Handle file read errors (e.g., file not found)
            Console.WriteLine($"An error occurred while reading the file: {ex.Message}");
        }

        // Return the dictionary of degree counts
        return degrees;
    }

    /// <summary>
    /// Determine if 'word1' and 'word2' are anagrams.  An anagram
    /// is when the same letters in a word are re-organized into a 
    /// new word.  A dictionary is used to solve the problem.
    /// </summary>
    /// <param name="word1">First word</param>
    /// <param name="word2">Second word</param>
    /// <returns>True if the words are anagrams, false otherwise</returns>
public static bool IsAnagram(string word1, string word2)
{
    // Remove spaces and convert to lowercase for case insensitivity
    word1 = word1.Replace(" ", "").ToLower();
    word2 = word2.Replace(" ", "").ToLower();

    // If the lengths are different, the words cannot be anagrams
    if (word1.Length != word2.Length)
    {
        return false;
    }

    // Create an array to count the frequency of each character (assuming lowercase English letters)
    int[] count = new int[26]; // One index for each letter a-z

    // Iterate through both words
    for (int i = 0; i < word1.Length; i++)
    {
        // Ensure that the characters are between 'a' and 'z' before processing
        if (word1[i] >= 'a' && word1[i] <= 'z') 
        {
            count[word1[i] - 'a']++; // Increment count for the character in word1
        }

        if (word2[i] >= 'a' && word2[i] <= 'z') 
        {
            count[word2[i] - 'a']--; // Decrement count for the character in word2
        }
    }

    // If all counts are zero, the words are anagrams
    foreach (int c in count)
    {
        if (c != 0)
        {
            return false; // If any count is not zero, they are not anagrams
        }
    }

    return true; // The words are anagrams
}


    /// <summary>
    /// This function will read JSON (Javascript Object Notation) data from the 
    /// United States Geological Service (USGS) consisting of earthquake data.
    /// The data will include all earthquakes in the current day.
    /// </summary>
    /// <returns>Array of strings representing earthquake places and magnitudes</returns>
    public static string[] EarthquakeDailySummary()
    {
        const string uri = "https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_day.geojson";
        using var client = new HttpClient();
        using var getRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
        using var jsonStream = client.Send(getRequestMessage).Content.ReadAsStream();
        using var reader = new StreamReader(jsonStream);
        var json = reader.ReadToEnd();

        // Deserialize the JSON into our FeatureCollection object
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var featureCollection = JsonSerializer.Deserialize<FeatureCollection>(json, options);

        // Create an array of formatted strings for each earthquake's place and magnitude
        return featureCollection.Features
            .Where(f => f.Properties != null)  // Ensure Properties is not null
            .Select(f => $"{f.Properties.Place} - Mag {f.Properties.Magnitude:F2}")
            .ToArray();
    }

    // Classes for deserialization
    public class FeatureCollection
    {
        public List<Feature> Features { get; set; }
    }

    public class Feature
    {
        public Properties Properties { get; set; }
    }

    public class Properties
    {
        public string Place { get; set; }
        public double Magnitude { get; set; }
    }
}
