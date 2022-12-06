using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;

namespace AdventOfCode2022.Day1;

public class Day1CalorieCounting
{
    public List<string> Calories;

    public Day1CalorieCounting()
    {
        Calories = GetCalorieList();
    }

    private List<string> GetCalorieList()
    {
        var fileLines = File.ReadAllLines(System.Environment.CurrentDirectory + @"\Day1\PuzzleInput.txt");

        Calories = new List<string>(fileLines);

        return Calories;
    }
}

[TestFixture]
public class Day1
{
    [Test]
    public void Day1_Part1()
    {
        //https://adventofcode.com/2022/day/1
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
        Assert.That(highestElfCalories.Equals(69310));
    }

    [Test]
    public void Day1_Part2()
    {
        //https://adventofcode.com/2022/day/1
        var puzzleInput = new Day1CalorieCounting();
        var allElfCalories = new List<int>();
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
                allElfCalories.Add(currentElfCalories);

                currentElfCalories = 0;
            }
        }


        allElfCalories.OrderByDescending(x => x)
            .Take(3).ToList().ForEach(i => Console.Write("{0}\t", i));

        var top3ElfCalorieTotal = allElfCalories.OrderByDescending(x => x)
            .Take(3)
            .Sum();
        Console.WriteLine("HighestElfCalories : " + top3ElfCalorieTotal);
        Assert.That(top3ElfCalorieTotal.Equals(206104));
    }
}