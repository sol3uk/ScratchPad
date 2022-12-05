using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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
        public string Value;

        public Assignment(string value)
        {
            Value = value;
        }

        public int FirstNumber()
        {
            return int.Parse(Value.Split("-")[0]);
        }
        public int LastNumber()
        {
            return int.Parse(Value.Split("-")[1]);
        }
    }
    
    public Day4CampCleanup()
    {
        SectionAssignments = GetSectionAssignments();
    }

    public List<SectionAssignment> SectionAssignments;

    private List<SectionAssignment> GetSectionAssignments()
    {
        var fileLines = File.ReadAllLines(System.Environment.CurrentDirectory + @"\Day4\PuzzleInput.txt");

        return fileLines
            .Select(x => x.Split(","))
            .Select(splitSections => new SectionAssignment(splitSections[0], splitSections[1]))
            .ToList();
    }
}




