namespace CodeGym;

public class AnagramKata
{
    public static string WordInput = "shipping";

    public static List<string> WordsInput = new()
    {
        "sing",
        "pong",
        "ship",
        "ping",
    };

    public static void GetWordsOutput(string wordInput, List<string> actualWordsOutput)
    {
        var wordInputChars = wordInput.ToCharArray();

        foreach (var possibleWord in WordsInput)
        {
            var possibleWordChars = possibleWord.ToCharArray();
            var inputBuffer = wordInput;
            if (!possibleWordChars.Any(x => wordInputChars.Contains(x)))
            {
                continue;
            }

            actualWordsOutput.Add(possibleWord);
            // Remove characters from inputBuffer
        }
    }
}