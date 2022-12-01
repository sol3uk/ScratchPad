using System;
using NUnit.Framework;

namespace AdventOfCode2022;

public class AdventTests
{

    [Test]
    public void Day1()
    {
        var puzzleInput = new Day1CalorieCounting();
        var elfNumber = 1;
        var highestElfCalories = 0;
        var currentElfCalories = 0;
        var totalLines = puzzleInput.Calories.Count;
        for (var index = 0; index < puzzleInput.Calories.Count; index++)
        {
            var line = puzzleInput.Calories[index];

            if (int.TryParse(line, out var parsedInt))
            {
                currentElfCalories += parsedInt;
            }
            
            if (string.IsNullOrEmpty(line) || index + 1 == totalLines)
            {
                if (currentElfCalories > highestElfCalories)
                {
                    highestElfCalories = currentElfCalories;
                    Console.WriteLine("New Highest Calories");
                }

                Console.WriteLine("Total For Elf #" + elfNumber + " : " + currentElfCalories);
                currentElfCalories = 0;
                elfNumber++;
            }
        }

        Console.WriteLine("HighestElfCalories : " + highestElfCalories);
        Assert.Pass();
    }
}