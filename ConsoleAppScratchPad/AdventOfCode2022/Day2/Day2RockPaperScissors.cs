using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2022.Day2;

public class Day2RockPaperScissors
{
    public record Move(Opponent Opponent, Your Your);

    public record Opponent(string Move)
    {
        public bool IsRock => (Move == "A");
        public bool IsPaper => (Move == "B");
        public bool IsScissors => (Move == "C");
    }    
    public record Your(string Move)
    {
        public bool IsRock => (Move == "X");
        public bool IsPaper => (Move == "Y");
        public bool IsScissors => (Move == "Z");
    }
    
    public List<Move> Moves;
    
    public Day2RockPaperScissors()
    {
        Moves = GetMoveList();
    }

    private List<Move> GetMoveList()
    {
        var fileLines = File.ReadAllLines(System.Environment.CurrentDirectory + @"\Day2\PuzzleInput.txt");

        return fileLines
            .Select(move => move.Split(" "))
            .Select(splitMove => new Move(new Opponent(splitMove[0]), new Your(splitMove[1])))
            .ToList();
    }
}