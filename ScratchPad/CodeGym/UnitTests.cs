using NUnit.Framework;

namespace CodeGym;

public class Tests
{
    [Test]
    public void AnagramKataTests()
    {
        // https://codingdojo.org/kata/Anagram/
        // Write a program that generates all two-word anagrams of the string “documenting”. 
        // It must not use the same letters across the two words
        var expectedWordsOutput = new List<string>()
        {
            "ship", "ping"
        };

        var actualWordsOutput = new List<string>()
        {
        };

        AnagramKata.GetWordsOutput(AnagramKata.WordInput, actualWordsOutput);
        
        Assert.AreEqual(expectedWordsOutput, actualWordsOutput);
    }
}