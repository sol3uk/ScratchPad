using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

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
        return Rucksacks.Select((x, idx) => new {x, idx})
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
            .Select(rucksack => new string[]
                {rucksack.Substring(0, rucksack.Length / 2), rucksack.Substring((rucksack.Length / 2))})
            .Select(rucksack => new Rucksack(rucksack[0], rucksack[1], rucksack[0] + rucksack[1]))
            .ToList();
    }
}

[TestFixture]
public class Day3
{
    [Test]
    public void Day3_Part1()
    {
        //https://adventofcode.com/2022/day/3
        // each line is a rucksack with the string being split in the middle
        // each side is first and second pouch respectively
        // case SENSITIVE
        // Lowercase item types a through z have priorities 1 through 26.
        // Uppercase item types A through Z have priorities 27 through 52.
        // Find the item type that appears in both compartments of each rucksack
        // What is the sum of the priorities of those item types?

        var puzzleInput = new Day3Rucksacks();
        var sumOfPriorities = 0;

        foreach (var rucksack in puzzleInput.Rucksacks)
        {
            var duplicateItem = rucksack.FirstPouch
                .Intersect(rucksack.SecondPouch)
                .FirstOrDefault().ToString();

            Assert.That(!string.IsNullOrEmpty(duplicateItem));
            sumOfPriorities += Day3Rucksacks.GetCharacterPriority(duplicateItem);
        }

        Assert.That(puzzleInput.Rucksacks != null);
        sumOfPriorities.Should().Be(8123);
    }

    [Test]
    public void Day3_Part2()
    {
        //https://adventofcode.com/2022/day/3
        // each 3 lines are now "groups"
        // find the one item type that is common between all three Elves in each group.
        // Find the item type that corresponds to the badges of each three-Elf group.
        // What is the sum of the priorities of those item types?

        var puzzleInput = new Day3Rucksacks();
        var sumOfPriorities = 0;

        foreach (var rucksackGroup in puzzleInput.GetGroups())
        {
            var group = rucksackGroup.ToList();
            var duplicateItem = group[0].WholeBag
                .Intersect(group[1].WholeBag)
                .Intersect(group[2].WholeBag)
                .FirstOrDefault().ToString();

            Assert.That(!string.IsNullOrEmpty(duplicateItem));
            sumOfPriorities += Day3Rucksacks.GetCharacterPriority(duplicateItem);
        }

        Assert.That(puzzleInput.Rucksacks != null);
        sumOfPriorities.Should().Be(2620);
    }
}