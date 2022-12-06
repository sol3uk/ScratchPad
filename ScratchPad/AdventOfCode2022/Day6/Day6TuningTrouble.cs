using System.Collections.Generic;
using System.IO;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

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

[TestFixture]
public class Day6
{
    [Test]
    public void Day6_Part1()
    {
        //https://adventofcode.com/2022/day/6
        // The signal is a series of seemingly-random characters that the device receives one at a time.
        // You need to add a subroutine to the device that detects a start-of-packet marker in the datastream.
        // The start of a packet is indicated by a sequence of four characters that are all different.

        var puzzleInput = new Day6TuningTrouble();
        var charactersProcessedBeforeMarker = 0;
        var processedChars = new List<string>();
        foreach (var c in puzzleInput.DataStreamBuffer.ToCharArray())
        {
            var character = c.ToString();

            processedChars.Add(character);
            charactersProcessedBeforeMarker++;

            if (processedChars.Count < 4) continue;

            if (processedChars.Distinct().Count() == processedChars.Count)
            {
                break;
            }

            processedChars.RemoveAt(0);
        }

        charactersProcessedBeforeMarker.Should().Be(1909);
    }

    [Test]
    public void Day6_Part2()
    {
        //https://adventofcode.com/2022/day/6
        // A start-of-message marker is just like a start-of-packet marker, except it consists of 14 distinct characters rather than 4.

        var puzzleInput = new Day6TuningTrouble();
        var charactersProcessedBeforeMarker = 0;
        var processedChars = new List<string>();
        foreach (var c in puzzleInput.DataStreamBuffer.ToCharArray())
        {
            var character = c.ToString();

            processedChars.Add(character);
            charactersProcessedBeforeMarker++;

            if (processedChars.Count < 14) continue;

            if (processedChars.Distinct().Count() == processedChars.Count)
            {
                break;
            }

            processedChars.RemoveAt(0);
        }

        charactersProcessedBeforeMarker.Should().Be(3380);
    }
}