using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode2022.Day4;

namespace AdventOfCode2022.Day6;

public class Day6TuningTrouble
{
    public Day6TuningTrouble()
    {
        DataStreamBuffer = GetDataStreamBuffer();
    }

    public readonly string DataStreamBuffer;

    private static string GetDataStreamBuffer()
    {
        return File.ReadAllText(System.Environment.CurrentDirectory + @"\Day6\PuzzleInput.txt");
    }
}