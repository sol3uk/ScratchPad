using System.Collections.Generic;
using System.IO;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode2022.Day4;

public class Day4CampCleanup
{
    public record SectionAssignment(Assignment FirstElf, Assignment SecondElf)
    {
        public SectionAssignment(string firstElf, string secondElf)
            : this(new Assignment(firstElf), new Assignment(secondElf))
        {
        }
    }

    public class Assignment
    {
        private readonly string _value;

        public Assignment(string value)
        {
            _value = value;
        }

        public int FirstNumber()
        {
            return int.Parse(_value.Split("-")[0]);
        }

        public int LastNumber()
        {
            return int.Parse(_value.Split("-")[1]);
        }
    }

    public Day4CampCleanup()
    {
        SectionAssignments = GetSectionAssignments();
    }

    public readonly List<SectionAssignment> SectionAssignments;

    private static List<SectionAssignment> GetSectionAssignments()
    {
        var fileLines = File.ReadAllLines(System.Environment.CurrentDirectory + @"\Day4\PuzzleInput.txt");

        return fileLines
            .Select(x => x.Split(","))
            .Select(splitSections => new SectionAssignment(splitSections[0], splitSections[1]))
            .ToList();
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