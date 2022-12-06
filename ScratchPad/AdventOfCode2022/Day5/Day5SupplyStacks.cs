using System.Collections.Generic;
using System.IO;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode2022.Day5;

public class Day5SupplyStacks
{
    public record Procedure(int NumberToMove, string SourceColumn, string DestinationColumn);


    public Day5SupplyStacks()
    {
        CrateMatrix = GetCrateMatrix();
        Procedures = GetCrateProcedure();
    }

    public readonly Dictionary<string, Stack<string>> CrateMatrix;

    public readonly List<Procedure> Procedures;

    private static Dictionary<string, Stack<string>> GetCrateMatrix()
    {
        var fileLines = File.ReadAllLines(System.Environment.CurrentDirectory + @"\Day5\PuzzleInput.txt");

        var columnNumbers = fileLines
            .FirstOrDefault(x => !x.Contains('['))!
            .Split(" ")
            .Where(x => !string.IsNullOrEmpty(x))
            .ToList();

        var crateRows = fileLines
            .TakeWhile(x => x.Contains('['))
            .Select(x => x.Chunk(4))
            .Select(x => x.Select(character => string.Join("", character).Trim('[', ']', ' ')))
            .ToArray();

        var crateColumns = new Dictionary<string, Stack<string>>();
        for (var index = 0; index < columnNumbers.Count; index++)
        {
            var columnCrates = new List<string>();
            var column = columnNumbers[index];
            for (var i = crateRows.Length - 1; i >= 0; i--)
            {
                var cleanRow = crateRows[i];
                var row = cleanRow.ToArray();
                if (!string.IsNullOrWhiteSpace(row[index]))
                    columnCrates.Add(row[index]);
            }

            crateColumns.Add(column, new Stack<string>(columnCrates));
        }

        return crateColumns;
    }

    private static List<Procedure> GetCrateProcedure()
    {
        var fileLines = File.ReadAllLines(System.Environment.CurrentDirectory + @"\Day5\PuzzleInput.txt");

        var procedures = fileLines
            .SkipWhile(x => !x.Contains("move"))
            .Select(x => x.Split(" "))
            .Select(x => x.ToArray())
            .Select(x => new Procedure(int.Parse(x[1]), x[3], x[5]))
            .ToList();


        return procedures;
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
        // After the rearrangement procedure completes, what crate ends up on top of each stack?

        var puzzleInput = new Day5SupplyStacks();
        var topOfStacksString = "";

        var crateWarehouse = puzzleInput.CrateMatrix;
        foreach (var procedure in puzzleInput.Procedures)
        {
            for (var i = 0; i < procedure.NumberToMove; i++)
            {
                var movedCrate = crateWarehouse[procedure.SourceColumn].Pop();
                crateWarehouse[procedure.DestinationColumn].Push(movedCrate);
            }
        }

        foreach (var column in crateWarehouse)
        {
            topOfStacksString += column.Value.FirstOrDefault();
        }

        topOfStacksString.Should().Be("FRDSQRRCD");
    }

    [Test]
    public void Day5_Part2()
    {
        //https://adventofcode.com/2022/day/5
        /* The input is a "crate matrix"
         *     [D]     
         * [N] [C]     
         * [Z] [M] [P] 
         *  1   2   3 
         */
        // Now the crane has the ability to pick up and move multiple crates at once.
        // After the rearrangement procedure completes, what crate ends up on top of each stack?

        var puzzleInput = new Day5SupplyStacks();
        var topOfStacksString = "";

        var crateWarehouse = puzzleInput.CrateMatrix;
        foreach (var procedure in puzzleInput.Procedures)
        {
            var tempStack = new Stack<string>();
            for (var i = 0; i < procedure.NumberToMove; i++)
            {
                var movedCrate = crateWarehouse[procedure.SourceColumn].Pop();
                tempStack.Push(movedCrate);
            }

            while (tempStack.Count > 0)
            {
                var movedCrate = tempStack.Pop();
                crateWarehouse[procedure.DestinationColumn].Push(movedCrate);
            }
        }

        foreach (var column in crateWarehouse)
        {
            topOfStacksString += column.Value.FirstOrDefault();
        }

        topOfStacksString.Should().Be("HRFTQVWNN");
    }
}