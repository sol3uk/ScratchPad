using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Coord = AdventOfCode2022.Day9.Day9RopeBridge.Coord;

namespace AdventOfCode2022.Day9;

public class Day9RopeBridge
{
    public record Coord(int X, int Y)
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Coord GoUp(int distance)
        {
            Y += distance;
            return this;
        }

        public Coord GoDown(int distance)
        {
            Y -= distance;
            return this;
        }

        public Coord GoRight(int distance)
        {
            X += distance;
            return this;
        }

        public Coord GoLeft(int distance)
        {
            X -= distance;
            return this;
        }
    };
    public record Motion(string Direction, int Distance);

    public Day9RopeBridge()
    {
        RopeMotions = GetMotions();
    }

    private List<Motion> GetMotions()
    {
        var fileLines = File.ReadAllLines(Environment.CurrentDirectory + @"\Day9\PuzzleInput.txt");

        return fileLines
            .Select(line =>
                new Motion(line.Split(" ")[0], int.Parse(line.Split(" ")[1])))
            .ToList();
    }

    public static List<Motion> RopeMotions;
}

[TestFixture]
public class Day9
{
    [Test]
    public void Part1()
    {
        //https://adventofcode.com/2022/day/9
        // The input is a list of commands to move rope around
        // Consider a rope with a knot at each end; these knots mark the head and the tail of the rope.
        // If the head moves far enough away from the tail, the tail is pulled toward the head.
        // the head (H) and tail (T) must always be touching (diagonally adjacent and even overlapping both count as touching):
        // Assume the head and the tail both start at the same position, overlapping.
        // How many positions does the tail of the rope visit at least once?
        var numberOfPositionsTailMoved = 0;
        var puzzleInput = Day9RopeBridge.RopeMotions;
        var headLocation = new Coord(0, 0);
        var tailLocation = new Coord(0, 0);
        foreach (var (direction, distance) in puzzleInput)
        {
            
        }

        numberOfPositionsTailMoved.Should().Be(13);
    }
}

