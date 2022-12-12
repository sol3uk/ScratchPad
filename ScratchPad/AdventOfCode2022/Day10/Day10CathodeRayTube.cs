using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Coord = AdventOfCode2022.Day9.Day9RopeBridge.Coord;

namespace AdventOfCode2022.Day10;

public class Day10CathodeRayTube
{
    public Day10CathodeRayTube()
    {
        Commands = GetCommands();
    }

    private static List<Command> GetCommands()
    {
        var fileLines = File.ReadAllLines(Environment.CurrentDirectory + @"\Day10\PuzzleInput.txt");

        return fileLines
            .Select(line => line.Split(" ")[0] != "noop" ? new Command(line.Split(" ")[0], int.Parse(line.Split(" ")[1])) : new Command(line))
            .ToList();
    }

    public record Command(string Name, int? Value = null);

    public readonly List<Command> Commands;
}

[TestFixture]
public class Day10
{
    [Test]
    public void Part1()
    {
        //https://adventofcode.com/2022/day/9
        // The CPU has a single register, X, which starts with the value 1. It supports only two instructions:
        //  "addx V" takes two cycles to complete. After two cycles, the X register is increased by the value V. (V can be negative.)
        //  "noop" takes one cycle to complete. It has no other effect.
        // The CPU uses these instructions in a program (your puzzle input) to, somehow, tell the screen what to draw.
        // Find the signal strength during the 20th, 60th, 100th, 140th, 180th, and 220th cycles. What is the sum of these six signal strengths?

        var sumOfSignalStrengths = 0;
        var puzzleInput = new Day10CathodeRayTube();


        puzzleInput.Commands.Should().NotBeNullOrEmpty();
        sumOfSignalStrengths.Should().Be(13140);
    }
}