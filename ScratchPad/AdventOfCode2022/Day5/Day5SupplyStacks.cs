using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode2022.Day4;

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

        var cleanRows = fileLines
            .TakeWhile(x => x.Contains('['))
            .Select(x => x.Chunk(4))
            .Select(x => x.Select(character => string.Join("", character).Trim('[',']',' ')))
            .ToArray();

        var crateColumns = new Dictionary<string, Stack<string>>();
        for (var index = 0; index < columnNumbers.Count; index++)
        {
            var columnCrates = new List<string>();
            var column = columnNumbers[index];
            for (var i = cleanRows.Length - 1; i >= 0; i--)
            {
                var cleanRow = cleanRows[i];
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
            .Select(x =>  new Procedure(int.Parse(x[1]),x[3], x[5]))
            .ToList();


        return procedures;
    }
}






