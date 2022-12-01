using System.Collections.Generic;
using System.IO;

namespace AdventOfCode2022;

public class Day1CalorieCounting
{
    public List<string> Calories;

    public Day1CalorieCounting()
    {
        Calories = GetCalorieList();
    }

    private List<string> GetCalorieList()
    {
        var fileLines = File.ReadAllLines(System.Environment.CurrentDirectory + @"\PuzzleInput.txt");

        Calories = new List<string>(fileLines);
        
        return Calories;
    }
}