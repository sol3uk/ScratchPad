using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2022.Day3;

public class Day3Rucksacks
{
    private enum CharacterPriority
    {
        a, b, c, d, e, f, g, h, i, j, k, l, m, n, o, p, q, r, s, t, u, v, w, x, y, z,
        A, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P, Q, R, S, T, U, V, W, X, Y, Z
    }

    public static int GetCharacterPriority(string character)
    {
        var characterEnum = Enum.Parse(typeof(CharacterPriority), character);
        return ((int) characterEnum + 1);
    }
    
    public IEnumerable<IEnumerable<Rucksack>> GetGroups()
    {
        return Rucksacks.Select((x, idx) => new { x, idx })
            .GroupBy(x => x.idx / 3)
            .Select(g => g.Select(a => a.x));
    }
    
    public record Rucksack(string FirstPouch, string SecondPouch, string WholeBag);
    
    public List<Rucksack> Rucksacks;
    
    public Day3Rucksacks()
    {
        Rucksacks = GetRucksacks();
    }

    private List<Rucksack> GetRucksacks()
    {
        var fileLines = File.ReadAllLines(System.Environment.CurrentDirectory + @"\Day3\PuzzleInput.txt");

        return fileLines
            .Select(rucksack => new string[] { rucksack.Substring(0, rucksack.Length / 2) , rucksack.Substring((rucksack.Length / 2))} )
            .Select(rucksack => new Rucksack(rucksack[0], rucksack[1], rucksack[0] + rucksack[1]))
            .ToList();
    }
}


