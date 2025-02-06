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
        // Normalize the words: remove spaces and convert to lowercase
        word1 = word1.Replace(" ", "").ToLower();
        word2 = word2.Replace(" ", "").ToLower();

        // If the lengths are different, they can't be anagrams
        if (word1.Length != word2.Length)
        {
            return false;
        }

        // Create dictionaries to count the frequency of each letter
        var dict1 = new Dictionary<char, int>();
        var dict2 = new Dictionary<char, int>();

        // Count the frequency of each letter in word1
        foreach (var c in word1)
        {
            if (dict1.ContainsKey(c))
            {
                dict1[c]++;
            }
            else
            {
                dict1[c] = 1;
            }
        }

        // Count the frequency of each letter in word2
        foreach (var c in word2)
        {
            if (dict2.ContainsKey(c))
            {
                dict2[c]++;
            }
            else
            {
                dict2[c] = 1;
            }
        }

        // Compare the two dictionaries
        return dict1.SequenceEqual(dict2);
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
