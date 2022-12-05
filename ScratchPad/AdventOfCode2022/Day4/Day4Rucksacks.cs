using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2022.Day4;

public class Day4CampCleanup
{
    public record SectionAssignment();
    
    public Day4CampCleanup()
    {
        SectionAssignments = GetSectionAssignments();
    }

    public List<SectionAssignment> SectionAssignments;

    private List<SectionAssignment> GetSectionAssignments()
    {
        var fileLines = File.ReadAllLines(System.Environment.CurrentDirectory + @"\Day4\PuzzleInput.txt");

        return fileLines
            // TODO: Split them up and assign values
            .Select(x => new SectionAssignment())
            .ToList();
    }
}




