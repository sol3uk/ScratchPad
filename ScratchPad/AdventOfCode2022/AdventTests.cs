using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2022.Day2;
using AdventOfCode2022.Day3;
using AdventOfCode2022.Day4;
using AdventOfCode2022.Day5;
using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode2022;

[TestFixture]
public class AdventTests
{
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

    [TestFixture]
    public class Day2
    {
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
            Assert.That(totalScore.Equals(12794));
        }

        [Test]
        public void Day2_Part2()
        {
            //https://adventofcode.com/2022/day/2
            // The first column is what your opponent is going to play:
            // A for Rock, B for Paper, and C for Scissors
            // The second column says how the round needs to end:
            // X means you need to lose, Y means you need to end the round in a draw, and Z means you need to win.

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
                    if (move.Your.ShouldLose)
                    {
                        totalScore += (int) Day2RockPaperScissors.ShapeScore.Scissors;
                        totalScore += (int) Day2RockPaperScissors.ResultScore.Loss;
                    }

                    if (move.Your.ShouldDraw)
                    {
                        totalScore += (int) Day2RockPaperScissors.ShapeScore.Rock;
                        totalScore += (int) Day2RockPaperScissors.ResultScore.Draw;
                    }

                    if (move.Your.ShouldWin)
                    {
                        totalScore += (int) Day2RockPaperScissors.ShapeScore.Paper;
                        totalScore += (int) Day2RockPaperScissors.ResultScore.Win;
                    }
                }

                if (move.Opponent.IsPaper)
                {
                    if (move.Your.ShouldLose)
                    {
                        totalScore += (int) Day2RockPaperScissors.ShapeScore.Rock;
                        totalScore += (int) Day2RockPaperScissors.ResultScore.Loss;
                    }

                    if (move.Your.ShouldDraw)
                    {
                        totalScore += (int) Day2RockPaperScissors.ShapeScore.Paper;
                        totalScore += (int) Day2RockPaperScissors.ResultScore.Draw;
                    }

                    if (move.Your.ShouldWin)
                    {
                        totalScore += (int) Day2RockPaperScissors.ShapeScore.Scissors;
                        totalScore += (int) Day2RockPaperScissors.ResultScore.Win;
                    }
                }

                if (move.Opponent.IsScissors)
                {
                    if (move.Your.ShouldLose)
                    {
                        totalScore += (int) Day2RockPaperScissors.ShapeScore.Paper;
                        totalScore += (int) Day2RockPaperScissors.ResultScore.Loss;
                    }

                    if (move.Your.ShouldDraw)
                    {
                        totalScore += (int) Day2RockPaperScissors.ShapeScore.Scissors;
                        totalScore += (int) Day2RockPaperScissors.ResultScore.Draw;
                    }

                    if (move.Your.ShouldWin)
                    {
                        totalScore += (int) Day2RockPaperScissors.ShapeScore.Rock;
                        totalScore += (int) Day2RockPaperScissors.ResultScore.Win;
                    }
                }
            }

            Console.WriteLine("TotalScore : " + totalScore);
            Assert.That(totalScore.Equals(14979));
        }
    }

    [TestFixture]
    public class Day3
    {
        [Test]
        public void Day3_Part1()
        {
            //https://adventofcode.com/2022/day/3
            // each line is a rucksack with the string being split in the middle
            // each side is first and second pouch respectively
            // case SENSITIVE
            // Lowercase item types a through z have priorities 1 through 26.
            // Uppercase item types A through Z have priorities 27 through 52.
            // Find the item type that appears in both compartments of each rucksack
            // What is the sum of the priorities of those item types?

            var puzzleInput = new Day3Rucksacks();
            var sumOfPriorities = 0;

            foreach (var rucksack in puzzleInput.Rucksacks)
            {
                var duplicateItem = rucksack.FirstPouch
                    .Intersect(rucksack.SecondPouch)
                    .FirstOrDefault().ToString();

                Assert.That(!string.IsNullOrEmpty(duplicateItem));
                sumOfPriorities += Day3Rucksacks.GetCharacterPriority(duplicateItem);
            }

            Assert.That(puzzleInput.Rucksacks != null);
            sumOfPriorities.Should().Be(8123);
        }

        [Test]
        public void Day3_Part2()
        {
            //https://adventofcode.com/2022/day/3
            // each 3 lines are now "groups"
            // find the one item type that is common between all three Elves in each group.
            // Find the item type that corresponds to the badges of each three-Elf group.
            // What is the sum of the priorities of those item types?

            var puzzleInput = new Day3Rucksacks();
            var sumOfPriorities = 0;

            foreach (var rucksackGroup in puzzleInput.GetGroups())
            {
                var group = rucksackGroup.ToList();
                var duplicateItem = group[0].WholeBag
                    .Intersect(group[1].WholeBag)
                    .Intersect(group[2].WholeBag)
                    .FirstOrDefault().ToString();

                Assert.That(!string.IsNullOrEmpty(duplicateItem));
                sumOfPriorities += Day3Rucksacks.GetCharacterPriority(duplicateItem);
            }

            Assert.That(puzzleInput.Rucksacks != null);
            sumOfPriorities.Should().Be(2620);
        }
    }
    
    [TestFixture]
    public class Day4
    {
        [Test]
        public void Day4_Part1()
        {
            //https://adventofcode.com/2022/day/4
            // each line is a section assignment
            // Some of the pairs have noticed that one of their assignments fully contains the other.
            // For example, 2-8 fully contains 3-7, and 6-6 is fully contained by 4-6.
            // In how many assignment pairs does one range fully contain the other?

            var puzzleInput = new Day4CampCleanup();
            var totalFullyContainedPairs = 0;

            foreach (var pair in puzzleInput.SectionAssignments)
            {
                if (pair.SecondElf.FirstNumber() >= pair.FirstElf.FirstNumber() 
                    && pair.SecondElf.LastNumber() <= pair.FirstElf.LastNumber())
                {
                    totalFullyContainedPairs++;
                } 
                else if (pair.FirstElf.FirstNumber() >= pair.SecondElf.FirstNumber() 
                      && pair.FirstElf.LastNumber() <= pair.SecondElf.LastNumber())
                {
                    totalFullyContainedPairs++;
                }
            }

            totalFullyContainedPairs.Should().Be(459);
        }
        
        [Test]
        public void Day4_Part2()
        {
            //https://adventofcode.com/2022/day/4
            // each line is a section assignment
            // the Elves would like to know the number of pairs that overlap at all.

            var puzzleInput = new Day4CampCleanup();
            var totalOverlappingPairs = 0;

            foreach (var pair in puzzleInput.SectionAssignments)
            {
                if (pair.SecondElf.FirstNumber() >= pair.FirstElf.FirstNumber() 
                    && pair.FirstElf.LastNumber() >= pair.SecondElf.FirstNumber())
                {
                    totalOverlappingPairs++;
                } 
                else if (pair.FirstElf.FirstNumber() >= pair.SecondElf.FirstNumber() 
                         && pair.SecondElf.LastNumber() >= pair.FirstElf.FirstNumber())
                {
                    totalOverlappingPairs++;
                }
            }

            totalOverlappingPairs.Should().Be(779);
        }
    }

    [TestFixture]
    public class Day5
    {
        [Test]
        public void Day5_Part1()
        {
            //https://adventofcode.com/2022/day/5
            /* The input is a "crate matrix"
             *     [D]     
             * [N] [C]     
             * [Z] [M] [P] 
             *  1   2   3 
             */ 
            // The lower half contains a procedure for moving the crates around
            // move 1 from 2 to 1
            // move 3 from 1 to 3

            var puzzleInput = new Day5SupplyStacks();
            var topOfStacksString = "";

            var crateWarehouse = puzzleInput.CrateMatrix;
            foreach (var procedure in puzzleInput.Procedures)
            {
                for (int i = 0; i < procedure.NumberToMove; i++)
                {
                    var movedCrate = crateWarehouse[procedure.SourceColumn].Pop();
                    crateWarehouse[procedure.DestinationColumn].Push(movedCrate);
                }
            }

            foreach (var column in crateWarehouse)
            {
                topOfStacksString += column.Value.FirstOrDefault();
            }

            topOfStacksString.Should().Be("CMZ");
        }
    }
}