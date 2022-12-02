using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2022.Day2;
using NUnit.Framework;

namespace AdventOfCode2022;

public class AdventTests
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
    
    [Test]
    public void Day2_Part1()
    {
        //https://adventofcode.com/2022/day/2
        // The first column is what your opponent is going to play:
        // A for Rock, B for Paper, and C for Scissors
        // The second column, you reason, must be what you should play in response:
        // X for Rock, Y for Paper, and Z for Scissors
        
        // The score for a single round is the score for the shape you selected
        // 1 for Rock, 2 for Paper, and 3 for Scissors
        // plus the score for the outcome of the round
        // 0 if you lost, 3 if the round was a draw, and 6 if you won.
        var puzzleInput = new Day2RockPaperScissors();

        var totalScore = 0;

        foreach (var move in puzzleInput.Moves)
        {
            
            if (move.Opponent.IsRock)
            {
                if (move.Your.IsRock) 
                {
                    // Shape Score Modifier
                    totalScore += 1;
                    // Draw
                    totalScore += 3;
                }

                if (move.Your.IsPaper) 
                {
                    // Shape Score Modifier
                    totalScore += 2;
                    // Win
                    totalScore += 6;
                }
                
                if (move.Your.IsScissors) 
                {
                    // Shape Score Modifier
                    totalScore += 3;
                    // Lose
                    totalScore += 0;
                }
            }
            if (move.Opponent.IsPaper)
            {
                if (move.Your.IsRock) 
                {
                    // Shape Score Modifier
                    totalScore += 1;
                    // Lose
                    totalScore += 0;
                }
                
                if (move.Your.IsPaper) 
                {
                    // Shape Score Modifier
                    totalScore += 2;
                    // Draw
                    totalScore += 3;
                }

                if (move.Your.IsScissors) 
                {
                    // Shape Score Modifier
                    totalScore += 3;
                    // Win
                    totalScore += 6;
                }
            }
            if (move.Opponent.IsScissors)
            {
                if (move.Your.IsRock) 
                {
                    // Shape Score Modifier
                    totalScore += 1;
                    // Win
                    totalScore += 6;
                }
                
                if (move.Your.IsPaper) 
                {
                    // Shape Score Modifier
                    totalScore += 2;
                    // Lose
                    totalScore += 0;
                }
                
                if (move.Your.IsScissors) 
                {
                    // Shape Score Modifier
                    totalScore += 3;
                    // Draw
                    totalScore += 3;
                }
            }
        }
        
        Console.WriteLine("TotalScore : " + totalScore);
        Assert.That(totalScore.Equals(69));
    }
}