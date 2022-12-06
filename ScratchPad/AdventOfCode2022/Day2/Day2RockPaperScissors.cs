using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;

namespace AdventOfCode2022.Day2;

public class Day2RockPaperScissors
{
    public enum ShapeScore
    {
        Rock = 1,
        Paper = 2,
        Scissors = 3
    }

    public enum ResultScore
    {
        Loss = 0,
        Draw = 3,
        Win = 6
    }

    public record Move(Opponent Opponent, Your Your);

    public record Opponent(string Move)
    {
        public bool IsRock => (Move == "A");
        public bool IsPaper => (Move == "B");
        public bool IsScissors => (Move == "C");
    }

    public record Your(string Move)
    {
        public bool IsRock => (Move == "X");
        public bool IsPaper => (Move == "Y");

        public bool IsScissors => (Move == "Z");

        // Pt 2
        public bool ShouldLose => (Move == "X");
        public bool ShouldDraw => (Move == "Y");
        public bool ShouldWin => (Move == "Z");
    }

    public List<Move> Moves;

    public Day2RockPaperScissors()
    {
        Moves = GetMoveList();
    }

    private List<Move> GetMoveList()
    {
        var fileLines = File.ReadAllLines(System.Environment.CurrentDirectory + @"\Day2\PuzzleInput.txt");

        return fileLines
            .Select(move => move.Split(" "))
            .Select(splitMove => new Move(new Opponent(splitMove[0]), new Your(splitMove[1])))
            .ToList();
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