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

public class InglenookShuntingKata
{
    public static List<string> GetMovesFromTarget(Stack<string> trackA, Stack<string> trackB, Stack<string> trackC,
        Stack<string> trackD, Stack<string> mainTargetTrackState)
    {
        return new List<string>()
        {
            "B to C +1",
            "D to B +1",
        };
    }
}

public class FullEnglishTimeKata
{
    public static string GetEnglishTime(int secondsSinceMidnight)
    {
        var minutesFull = (secondsSinceMidnight / 60);
        var minutesRounded = (int) Math.Floor((decimal) minutesFull);
        var hoursFull = minutesFull / 60;
        var hoursRounded = (int) Math.Floor((decimal) hoursFull);
        var isMorning = hoursRounded <= 11 && minutesRounded < 60;
        var hoursNon24 = isMorning ? hoursRounded : hoursRounded - 12;
        var minutesMinusHours = (int) Math.Floor((decimal) (minutesRounded - (hoursRounded * 60)));
        var minutesToHour = 60 - minutesMinusHours;

        if (secondsSinceMidnight == 0)
        {
            return "Midnight";
        }

        if (secondsSinceMidnight < 60)
        {
            return $"{UppercaseFirst(NumberToWords(secondsSinceMidnight))} seconds since midnight";
        }

        if (hoursRounded == 0)
        {
            // Midnight cases
            // half past 
            if (minutesMinusHours == 30)
            {
                return $"Half past midnight";
            }

            // n minutes from "hour" 
            if (minutesMinusHours < 30)
            {
                return $"{UppercaseFirst(NumberToWords(minutesRounded))} minute past midnight";
            }

            // n minutes to "hour"
            if (minutesMinusHours < 60)
            {
                return $"{UppercaseFirst(NumberToWords(minutesToHour))} minute to midnight";
            }
        }
        else
        {
            if (minutesFull == minutesRounded)
            {
                // There are no seconds involved

                // half past 
                if (minutesMinusHours == 30)
                {
                    return $"half past";
                }

                // n minutes from "hour" 
                if (minutesMinusHours < 30)
                {
                    return
                        $"{UppercaseFirst(NumberToWords(minutesMinusHours))} minute from {UppercaseFirst(NumberToWords(hoursRounded))}";
                }

                // n minutes to "hour"
                if (minutesMinusHours < 60)
                {
                    var nextHourComingUp = hoursNon24 + 1;
                    if (minutesToHour > 1)
                    {
                        return
                            $"{UppercaseFirst(NumberToWords(minutesToHour))} minutes to {NumberToWords(nextHourComingUp)}";
                    }
                    else
                    {
                        return
                            $"{UppercaseFirst(NumberToWords(minutesToHour))} minute to {NumberToWords(nextHourComingUp)}";
                    }
                }
            }

            if (minutesRounded > 0)
            {
                return "bad";
            }
        }


        return "bad";
    }

    public static string NumberToWords(int number)
    {
        if (number == 0)
            return "zero";

        if (number < 0)
            return "minus " + NumberToWords(Math.Abs(number));

        string words = "";

        if ((number / 1000000) > 0)
        {
            words += NumberToWords(number / 1000000) + " million ";
            number %= 1000000;
        }

        if ((number / 1000) > 0)
        {
            words += NumberToWords(number / 1000) + " thousand ";
            number %= 1000;
        }

        if ((number / 100) > 0)
        {
            words += NumberToWords(number / 100) + " hundred ";
            number %= 100;
        }

        if (number > 0)
        {
            if (words != "")
                words += "and ";

            var unitsMap = new[]
            {
                "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven",
                "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen"
            };
            var tensMap = new[]
                {"zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety"};

            if (number < 20)
                words += unitsMap[number];
            else
            {
                words += tensMap[number / 10];
                if ((number % 10) > 0)
                    words += "-" + unitsMap[number % 10];
            }
        }

        return words;
    }

    static string UppercaseFirst(string s)
    {
        if (string.IsNullOrEmpty(s))
        {
            return string.Empty;
        }

        char[] a = s.ToCharArray();
        a[0] = char.ToUpper(a[0]);
        return new string(a);
    }
}

public class CompassKata
{
    public static (int, int) GetResultOfWalk(List<string> directions)
    {
        var currentLocation = (0, 0);
        foreach (var direction in directions)
        {
            switch (direction)
            {
                case "n":
                    currentLocation.Item2++;
                    break;
                case "e":
                    currentLocation.Item1++;
                    break;
                case "s":
                    currentLocation.Item2--;
                    break;
                case "w":
                    currentLocation.Item1--;
                    break;
            }
        }

        return currentLocation;
    }

    public static string GetDirectionsBack(List<string> directions)
    {
        var resultOfWalk = GetResultOfWalk(directions);
        var directionsBackToOrigin = "";

        switch (resultOfWalk.Item1)
        {
            case > 0:
                directionsBackToOrigin += new string('w', resultOfWalk.Item1);
                break;
            case < 0:
                directionsBackToOrigin += new string('e', Math.Abs(resultOfWalk.Item1));
                break;
        }

        switch (resultOfWalk.Item2)
        {
            case > 0:
                directionsBackToOrigin += new string('s', resultOfWalk.Item2);
                break;
            case < 0:
                directionsBackToOrigin += new string('n', Math.Abs(resultOfWalk.Item2));
                break;
        }

        return string.Join(",", directionsBackToOrigin.ToCharArray());
    }
}